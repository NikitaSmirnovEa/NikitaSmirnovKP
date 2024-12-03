using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO; 

namespace vulrill
{
    public partial class import : Form
    {
        public import()
        {
            InitializeComponent();
            LoadTableNames();
        }

        private void LoadTableNames()
        {
            using (MySqlConnection con = new MySqlConnection(helper.connect))
            {
                con.Open();
                var cmd = new MySqlCommand("SHOW TABLES;", con);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBoxTables.Items.Add(reader[0].ToString());
                    }
                }
            }
        }
       
        private void ImportCsvToDatabase(string filePath, string tableName)
        {
            int importedRecordsCount = 0;

            try
            {
                using (MySqlConnection con = new MySqlConnection(helper.connect))
                {
                    con.Open();

                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                     
                        reader.ReadLine();

                        int columnCount = GetColumnCount(con, tableName);

                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] values = line.Split(';');

                            
                            if (values.Length != columnCount)
                            {
                                MessageBox.Show($"Parameter count mismatch: expected {columnCount}, but got {values.Length}.");
                                return;
                            }

                            var insertCommand = new MySqlCommand($"INSERT INTO {tableName} VALUES ({string.Join(",", values.Select(v => $"'{v}'"))});", con);
                            insertCommand.ExecuteNonQuery();
                            importedRecordsCount++;
                        }
                    }
                }

                MessageBox.Show($"Successfully imported {importedRecordsCount} records.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error importing data: {ex.Message}");
            }
        }
        private int GetColumnCount(MySqlConnection con, string tableName)
        {
            var getColumnCountCommand = new MySqlCommand($"DESCRIBE {tableName}", con);
            int columnCount = 0;
            using (var reader = getColumnCountCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    columnCount++;
                }
            }
            return columnCount;
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(helper.connect))
            {
                con.Open();
                try
                {
                    string createSchemaScript = @"
            DROP TABLE IF EXISTS `client`;
            DROP TABLE IF EXISTS `employee`;
            DROP TABLE IF EXISTS `master`;
            DROP TABLE IF EXISTS `order`;
            DROP TABLE IF EXISTS `role`;
            DROP TABLE IF EXISTS `sketch`;

            CREATE TABLE `role` (
                `id_role` int(11) NOT NULL AUTO_INCREMENT,
                `name` varchar(45) NOT NULL,
                PRIMARY KEY (`id_role`)
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

            CREATE TABLE `sketch` (
                `id_sketch` int(11) NOT NULL AUTO_INCREMENT,
                `name` varchar(45) NOT NULL,
                `cost` int(11) NOT NULL,
                `image` varchar(45) NOT NULL,
                PRIMARY KEY (`id_sketch`)
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

            CREATE TABLE `client` (
                `id_client` int(11) NOT NULL AUTO_INCREMENT,
                `surname` varchar(45) NOT NULL,
                `name` varchar(45) NOT NULL,
                `patronymic` varchar(45) DEFAULT NULL,
                `phone_number` bigint(20) NOT NULL,
                `age` int(11) NOT NULL,
                PRIMARY KEY (`id_client`)
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

            CREATE TABLE `employee` (
                `id_employee` int(11) NOT NULL AUTO_INCREMENT,
                `login` varchar(45) NOT NULL,
                `password` varchar(100) NOT NULL,
                `role_id` int(11) NOT NULL,
                `surname` varchar(45) NOT NULL,
                `name` varchar(45) NOT NULL,
                `patronymic` varchar(45) DEFAULT NULL,
                `phone_number` bigint(20) NOT NULL,
                PRIMARY KEY (`id_employee`),
                FOREIGN KEY (`role_id`) REFERENCES `role` (`id_role`)
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

            CREATE TABLE `master` (
                `id_master` int(11) NOT NULL AUTO_INCREMENT,
                `surname` varchar(45) NOT NULL,
                `name` varchar(45) NOT NULL,
                `patronymic` varchar(45) DEFAULT NULL,
                `experience` int(11) NOT NULL,
                `phone_number` bigint(20) NOT NULL,
                PRIMARY KEY (`id_master`)
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

            CREATE TABLE `order` (
                `id_order` int(11) NOT NULL AUTO_INCREMENT,
                `sketch_id` int(11) NOT NULL,
                `master_id` int(11) NOT NULL,
                `client_id` int(11) NOT NULL,
                `employee_id` int(11) NOT NULL,
                `date` date NOT NULL,
                PRIMARY KEY (`id_order`),
                FOREIGN KEY (`client_id`) REFERENCES `client` (`id_client`),
                FOREIGN KEY (`employee_id`) REFERENCES `employee` (`id_employee`),
                FOREIGN KEY (`master_id`) REFERENCES `master` (`id_master`),
                FOREIGN KEY (`sketch_id`) REFERENCES `sketch` (`id_sketch`)

            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
        ";

                    using (MySqlCommand command = new MySqlCommand(createSchemaScript, con))
                    {
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Структура базы данных восстановлена успешно.");
                    LoadTableNames(); // Assuming this method loads table names to a UI component
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка восстановления структуры базы данных: {ex.Message}");
                }
            }
        }
      //
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (comboBoxTables.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите таблицу из списка.");
                return;
            }

            string selectedTable = comboBoxTables.SelectedItem.ToString();

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV files (*.csv)|*.csv";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    ImportCsvToDatabase(filePath, selectedTable);
                }
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
