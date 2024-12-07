using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using vulrill.Клиенты;
using System.Timers;

namespace vulrill
{
    public partial class client : Form
    {

        public client()
        {
            InitializeComponent();
   
        }
  
        int row_id = 0;
        string phone_num = "";
        string FIO = "";
        string poisk = "";
        public int minScren = 1;
        public int maxScren = 10;
        public int count = 0;
        public int countlast;

        public string search = "";

        private void client_Load(object sender, EventArgs e)
        {
            label4.Text = helper.surname + " " + helper.name;
            view();
        }

        private void view()
        {
            using (MySqlConnection con = new MySqlConnection(helper.connect))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT surname AS 'Фамилия', `name` AS 'Имя', patronymic AS 'Отчество', phone_number AS 'Номер телефона', age AS 'Возраст' FROM `client`;", con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvUpdateForm.DataSource = dt;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            client_add client_Add = new client_add();
            client_Add.ShowDialog();
            this.Show();
            view();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show($"Вы уверены, что хотите удалить клиента \"{FIO}\"?", "Удаление клиента", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(helper.connect))
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand($"DELETE FROM client WHERE phone_number = '{phone_num}';", con);
                        cmd.ExecuteNonQuery();

                        view();
                        MessageBox.Show("Вы успешно удалили клиента!", "Удаление клиента", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button1.Enabled = false; button2.Enabled = false;
                    }
                }
                catch (Exception ex) { MessageBox.Show("произошла неизвестная ошибка\n\n" + ex.Message, "Удаление клиента", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            client_edit client_Edit = new client_edit();
            client_Edit.ShowDialog();
            this.Show();
            view();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            adminPanel admin = new adminPanel();
            this.Visible = false;
            admin.ShowDialog();
            this.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            minScren = 1;
            Search();
            label1.ForeColor = Color.Aqua;
            label5.ForeColor = Color.Black;
            label6.ForeColor = Color.Black;
            label9.ForeColor = Color.Black;
        }
        private void Search()
        {
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = helper.connect;

                con.Open();

                MySqlCommand cmd = new MySqlCommand($"SELECT surname AS 'Фамилия', `name` AS 'Имя', patronymic AS 'Отчество', phone_number AS 'Номер телефона', age AS 'Возраст' FROM `client` WHERE Name LIKE '%{search}%' OR Surname LIKE '%{search}%' LIMIT {minScren},{maxScren};", con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dgvUpdateForm.DataSource = dt;
                //this.dgvUpdateForm.Columns["id_Client"].Visible = false;
                count = dgvUpdateForm.RowCount;
                label2.Text = Convert.ToString("Общее кол-во строк: " + count);
                dgvUpdateForm.RowHeadersVisible = false;
            }
        }
        private void GetDate()
        {
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = helper.connect;

                con.Open();

                MySqlCommand cmd = new MySqlCommand($"select * from `client`;", con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dgvUpdateForm.DataSource = dt;
                this.dgvUpdateForm.Columns["idClient"].Visible = false;
                countlast = dgvUpdateForm.RowCount;
                label2.Text = Convert.ToString("Общее кол-во строк: " + count);
            }
            Search();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                if (minScren != 0)
                {
                    minScren -= 10;
                    if (minScren == 0)
                    {
                        label1.ForeColor = Color.Aqua;
                        label5.ForeColor = Color.Black;
                        label6.ForeColor = Color.Black;
                        label9.ForeColor = Color.Black;
                    }
                    else if (minScren == 10)
                    {
                        label1.ForeColor = Color.Black;
                        label5.ForeColor = Color.Aqua;
                        label6.ForeColor = Color.Black;
                        label9.ForeColor = Color.Black;
                    }
                    else if (minScren == 20)
                    {
                        label1.ForeColor = Color.Black;
                        label5.ForeColor = Color.Black;
                        label6.ForeColor = Color.Aqua;
                        label9.ForeColor = Color.Black;
                    }
                    //
                    //
                    else if (minScren == 30)
                    {
                        label1.ForeColor = Color.Black;
                        label5.ForeColor = Color.Black;
                        label6.ForeColor = Color.Black;
                        label9.ForeColor = Color.Aqua;
                    }
                    Search();
                }
                else
                {
                    minScren = countlast - 1;
                    maxScren = 10;
                    label1.ForeColor = Color.Black;
                    label5.ForeColor = Color.Black;
                    label6.ForeColor = Color.Black;
                    label9.ForeColor = Color.Aqua;
                    Search();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("ошибка: " + ex.Message);
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (maxScren <= count)
            {
                minScren += 10;
                if (minScren == 0)
                {
                    label1.ForeColor = Color.Aqua;
                    label5.ForeColor = Color.Black;
                    label6.ForeColor = Color.Black;
                    label9.ForeColor = Color.Black;
                }
                else if (minScren == 10)
                {
                    label1.ForeColor = Color.Black;
                    label5.ForeColor = Color.Aqua;
                    label6.ForeColor = Color.Black;
                    label9.ForeColor = Color.Black;
                }
                else if (minScren == 20)
                {
                    label1.ForeColor = Color.Black;
                    label5.ForeColor = Color.Black;
                    label6.ForeColor = Color.Aqua;
                    label9.ForeColor = Color.Black;
                }
                else if (minScren == 30)
                {
                    label1.ForeColor = Color.Black;
                    label5.ForeColor = Color.Black;
                    label6.ForeColor = Color.Black;
                    label9.ForeColor = Color.Aqua;
                }
                Search();
            }
            else
            {
                minScren = 0;
                maxScren = 10;
                label1.ForeColor = Color.Aqua;
                label5.ForeColor = Color.Black;
                label6.ForeColor = Color.Black;
                label9.ForeColor = Color.Black;
                Search();

            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            minScren = 10;
            Search();
            label1.ForeColor = Color.Black;
            label5.ForeColor = Color.Aqua;
            label6.ForeColor = Color.Black;
            label9.ForeColor = Color.Black;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            minScren = 20;
            Search();
            label1.ForeColor = Color.Black;
            label5.ForeColor = Color.Black;
            label6.ForeColor = Color.Aqua;
            label9.ForeColor = Color.Black;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            minScren = 30;
            Search();
            label1.ForeColor = Color.Black;
            label5.ForeColor = Color.Black;
            label6.ForeColor = Color.Black;
            label9.ForeColor = Color.Aqua;
        }
        private void label9_MouseHover(object sender, EventArgs e)
        {
            if (label9.ForeColor == Color.Aqua)
            {

            }
            else
            {
                label9.ForeColor = Color.Red;
            }
        }
        private void label6_MouseHover(object sender, EventArgs e)
        {
            if (label6.ForeColor == Color.Aqua)
            {

            }
            else
            {
                label6.ForeColor = Color.Red;
            }
        }
        private void label5_MouseHover(object sender, EventArgs e)
        {
            if (label5.ForeColor == Color.Aqua)
            {

            }
            else
            {
                label5.ForeColor = Color.Red;
            }
        }
        private void label1_MouseHover(object sender, EventArgs e)
        {
            if (label1.ForeColor == Color.Aqua)
            {

            }
            else
            {
                label1.ForeColor = Color.Red;
            }
        }
        private void label9_MouseLeave(object sender, EventArgs e)
        {
            if (label9.ForeColor == Color.Aqua)
            {

            }
            else
            {
                label9.ForeColor = Color.Black;
            }
        }
        private void label6_MouseLeave(object sender, EventArgs e)
        {
            if (label6.ForeColor == Color.Aqua)
            {

            }
            else
            {
                label6.ForeColor = Color.Black;
            }
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            if (label5.ForeColor == Color.Aqua)
            {

            }
            else
            {
                label5.ForeColor = Color.Black;
            }
        }
        private void label1_MouseLeave(object sender, EventArgs e)
        {
            if (label1.ForeColor == Color.Aqua)
            {

            }
            else
            {
                label1.ForeColor = Color.Black;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dgvUpdateForm_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            row_id = dgvUpdateForm.CurrentCell.RowIndex;
            phone_num = dgvUpdateForm.Rows[row_id].Cells[3].Value.ToString();
            button2.Enabled = true; button1.Enabled = true;
            helper.loginEdit = phone_num;
            FIO = dgvUpdateForm.Rows[row_id].Cells[0].Value.ToString() + " " + dgvUpdateForm.Rows[row_id].Cells[1].Value.ToString() + " " + dgvUpdateForm.Rows[row_id].Cells[2].Value.ToString();

            view();
            dgvUpdateForm.Rows[row_id].DefaultCellStyle.BackColor = Color.FromArgb(24, 23, 28);
            dgvUpdateForm.Rows[row_id].DefaultCellStyle.ForeColor = Color.Black;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            poisk = textBox3.Text;
            viewTable();
        }
        //private void InitializeDataGridView()
        //{
        //    dgvUpdateForm.Columns.Clear(); // Удаляем старые столбцы, если они есть

        //    dgvUpdateForm.Columns.Add("surname", "Фамилия");
        //    dgvUpdateForm.Columns.Add("name", "Имя");
        //    dgvUpdateForm.Columns.Add("patronymic", "Отчество");
        //    dgvUpdateForm.Columns.Add("phone_number", "Номер телефона");
        //    dgvUpdateForm.Columns.Add("age", "Возраст");
        //}
        private void viewTable()
        {
            using (MySqlConnection con = new MySqlConnection(helper.connect))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT surname, `name`, patronymic, phone_number, age FROM client " +
                                                    $"WHERE surname LIKE @search " + 
                                                    $"", con);
                cmd.Parameters.AddWithValue("@search", $"%{poisk}%");

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Устанавливаем DataTable как источник данных для DataGridView
                dgvUpdateForm.DataSource = dt;
            }
        }
        private void dgvUpdateForm_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                string Names = dgvUpdateForm.Rows[rowIndex].Cells["idClient"].Value.ToString();
                string sqlQuery = "select surname AS 'Фамилия', `name` AS 'Имя', patronymic AS 'Отчество', phone_number AS 'Номер телефона', age AS 'Возраст' FROM `client` WHERE idClient='" + Names + "';";

                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = helper.connect;
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

                MySqlDataReader rdr = cmd.ExecuteReader();

                rdr.Read();

                textBox3.Text = rdr["Name"].ToString();
                textBox3.Text = rdr["Surname"].ToString(); 
            }
        }
        private void dgvUpdateForm_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            // Пример: Меняем цвет всей строки, если фамилия начинается на "А"
            string surname = dgvUpdateForm.Rows[e.RowIndex].Cells[0].Value as string; // 0 - индекс столбца с фамилией
            if (!string.IsNullOrEmpty(surname) && surname.StartsWith("А"))
            {
                dgvUpdateForm.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
            }
            else
            {
                dgvUpdateForm.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }
    }
}