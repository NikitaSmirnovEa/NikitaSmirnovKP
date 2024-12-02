using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using vulrill.Клиенты;
using Word = Microsoft.Office.Interop.Word;

namespace vulrill
{
    public partial class Заказы : Form
    {
        public Заказы()
        {
            InitializeComponent();
        }

        private void order_Load(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(helper.connect))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT name, cost, image FROM sketch WHERE name = '{helper.orderSketch}';", con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                label2.Text += dt.Rows[0][0].ToString();
                label1.Text += dt.Rows[0][1].ToString();
                if (dt.Rows[0][2].ToString() != "") { pictureBox1.ImageLocation = helper.path + dt.Rows[0][2].ToString(); }
                else { pictureBox1.ImageLocation = helper.path + "picture.png"; }

                comboBoxClientRefresh();
            }

            using (MySqlConnection con = new MySqlConnection(helper.connect))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT surname, name, patronymic FROM master;", con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    comboBox2.Items.Add(row["surname"].ToString() + " " + row["name"].ToString() + " " + row["patronymic"].ToString());
                }

                comboBox2.SelectedIndex = 0;
            }
        }

        private void comboBoxClientRefresh()
        {
            comboBox1.Items.Clear();

            using (MySqlConnection con = new MySqlConnection(helper.connect))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT surname, name, patronymic FROM client;", con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach(DataRow row in dt.Rows)
                {
                    comboBox1.Items.Add(row["surname"].ToString() + " " + row["name"].ToString() + " " + row["patronymic"].ToString());
                }
            }

            comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Заказы_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            client_add client_Add = new client_add();
            client_Add.ShowDialog();
            this.Show();
            comboBoxClientRefresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string master_id; string client_id; string employee_id; string sketch_id;
                string[] clientFIO = comboBox1.Text.Split(' ');
                string[] masterFIO = comboBox2.Text.Split(' ');

                using (MySqlConnection con = new MySqlConnection(helper.connect))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand($"SELECT id_sketch FROM sketch WHERE `name` = '{helper.orderSketch}';", con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    sketch_id = dt.Rows[0][0].ToString();
                }

                using (MySqlConnection con = new MySqlConnection(helper.connect))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT id_master FROM `master` " +
                                                        $"WHERE surname = '{masterFIO[0]}' " +
                                                        $"AND name = '{masterFIO[1]}' " +
                                                        $"AND patronymic = '{masterFIO[2]}';", con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    master_id = dt.Rows[0][0].ToString();
                }

                using (MySqlConnection con = new MySqlConnection(helper.connect))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT id_client FROM client " +
                                                        $"WHERE surname = '{clientFIO[0]}' " +
                                                        $"AND name = '{clientFIO[1]}' " +
                                                        $"AND patronymic = '{clientFIO[2]}';", con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    client_id = dt.Rows[0][0].ToString();
                }

                using (MySqlConnection con = new MySqlConnection(helper.connect))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand($"SELECT id_employee FROM employee WHERE login = '{helper.login}';", con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    employee_id = dt.Rows[0][0].ToString();
                }

                using (MySqlConnection con = new MySqlConnection(helper.connect))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand($"INSERT INTO `order` (sketch_id, master_id, client_id, employee_id, `date`) " +
                                                        $"VALUES ('{sketch_id}', '{master_id}', '{client_id}', '{employee_id}', '{DateTime.Now.ToString("yyyy-MM-dd")}');", con);
                    cmd.ExecuteNonQuery();
                    var res = MessageBox.Show("Хотите распечатать чек?", "Услуга успешно оформлена!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        string path = AppDomain.CurrentDomain.BaseDirectory;
                        var dateNow = DateTime.Now.ToString("dd.MM.yyyy HH.mm");
                        File.Copy(path + "order.docx", path + $@"orders\order {dateNow}.docx");
                        try
                        {
                            Word.Document doc = new Word.Document();
                            Word.Application app = new Word.Application();
                            doc = app.Documents.Open(path + $@"orders\order {dateNow}.docx");
                            Word.Bookmarks wBookmarks = doc.Bookmarks;     
                            Word.Range wRange;
                            int i = 0;
                            string[] data = new string[6] { comboBox1.Text, label1.Text, DateTime.Now.ToString("dd.MM.yyyy"), $"{helper.surname} {helper.name} {helper.patronymic}", comboBox2.Text, label2.Text};
                            foreach (Word.Bookmark mark in wBookmarks)
                            {
                                wRange = mark.Range;
                                wRange.Text = data[i];
                                i++;
                            }
                            doc.Close();
                            MessageBox.Show("Успех!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    this.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show("Произошла неизвестная ошибка.\n\n" + ex.Message, "Оформление услуги", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
