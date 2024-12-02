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

namespace vulrill
{
    public partial class employee_add : Form
    {
        public employee_add()
        {
            InitializeComponent();         
        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void employee_add_FormClosing(object sender, FormClosingEventArgs e)
        {
            var res = MessageBox.Show("Вы уверены, что хотите выйти вернуться в меню?", "Добавление сотрудника", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            e.Cancel = !(res == DialogResult.Yes);
        }

        private void employee_add_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 1;

            using (MySqlConnection con = new MySqlConnection(helper.connect))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("", con);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Pass = "";
            string simvols = "1234567890qwertyuiopasdfghjklzxcvbnm";
            Random rnd = new Random();
            for (int i = 0; i < rnd.Next(8, 30); i = i + 1)
            {
                Pass = Pass + simvols[rnd.Next(0, simvols.Length)];
            }
            textBox2.Text = Pass;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != 8)
            { e.Handled = true; }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != 8)
            { e.Handled = true; }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != 8)
            { e.Handled = true; }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && maskedTextBox1.MaskCompleted) { button3.Enabled = true; }
            else { button3.Enabled = false; }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && maskedTextBox1.MaskCompleted) { button3.Enabled = true; }
            else { button3.Enabled = false; }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && maskedTextBox1.MaskCompleted) { button3.Enabled = true; }
            else { button3.Enabled = false; }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && maskedTextBox1.MaskCompleted) { button3.Enabled = true; }
            else { button3.Enabled = false; }
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && maskedTextBox1.MaskCompleted) { button3.Enabled = true; }
            else { button3.Enabled = false; }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'A' || l > 'z') && l != 8 && (l < '0' || l > '9'))
            { e.Handled = true; }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'A' || l > 'z') && l != 8 && (l < '0' || l > '9'))
            { e.Handled = true; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(helper.connect))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand($"SELECT login FROM employee WHERE login = '{textBox1.Text}';", con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Сотрудник с таким логином уже существует!", "Добавление сотрудника", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        goto skipAdd;
                    }
                }

                string role_id = Convert.ToString(comboBox1.SelectedIndex + 1);
                string num = maskedTextBox1.Text;
                num = num.Replace(" ", "");
                num = num.Replace(")", "");
                num = num.Replace("(", "");
                num = num.Replace("-", "");

                using (MySqlConnection con = new MySqlConnection(helper.connect))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand($"INSERT INTO employee (login, password, role_id, surname, `name`, patronymic, phone_number) " +
                                                        $"VALUES ('{textBox1.Text}', '{helper.CreateMD5(textBox2.Text)}', '{role_id}', '{textBox3.Text}', '{textBox4.Text}', '{textBox5.Text}', '{num}');", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Вы успешно добавили нового сотрудника!", "Добавление сотрудника", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); textBox5.Clear(); maskedTextBox1.Clear();
                }

            skipAdd:
                textBox1.Clear();
            }
            catch (Exception ex) { MessageBox.Show("произошла неизвестная ошибка\n\n" + ex.Message, "Добавление сотрудника", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
