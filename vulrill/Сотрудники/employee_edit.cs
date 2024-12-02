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

namespace vulrill
{
    public partial class employee_edit : Form
    {
        public employee_edit()
        {
            InitializeComponent();
        }

        private void employee_edit_Load(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(helper.connect))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand($"SELECT login, role_id, surname, `name`, patronymic, phone_number FROM vulrill.employee " +
                                                        $"WHERE login = '{helper.loginEdit}';", con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    textBox1.Text = dt.Rows[0][0].ToString();
                    comboBox1.SelectedIndex = Convert.ToInt32(dt.Rows[0][1]) - 1;
                    textBox3.Text = dt.Rows[0][2].ToString();
                    textBox4.Text = dt.Rows[0][3].ToString();
                    textBox5.Text = dt.Rows[0][4].ToString();
                    maskedTextBox1.Text = dt.Rows[0][5].ToString();
                }
            }
            catch (Exception ex) { MessageBox.Show("произошла неизвестная ошибка\n\n" + ex.Message, "Редактирование сотрудника", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string num = maskedTextBox1.Text;
                num = num.Replace(" ", "");
                num = num.Replace(")", "");
                num = num.Replace("(", "");
                num = num.Replace("-", "");

                using (MySqlConnection con = new MySqlConnection(helper.connect))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand($"UPDATE employee SET login = '{textBox1.Text}', `password` = '{helper.CreateMD5(textBox2.Text)}', `role_id` = '{comboBox1.SelectedIndex + 1}', `surname` = '{textBox3.Text}', `name` = '{textBox4.Text}', `patronymic` = '{textBox5.Text}', `phone_number` = '{num}' " +
                                                        $"WHERE (login = '{helper.loginEdit}');", con);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Вы успешно изменили сотрудника!", "Редактирование сотрудника", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    helper.loginEdit = textBox1.Text;
                }
            }
            catch (Exception ex) { MessageBox.Show("произошла неизвестная ошибка\n\n" + ex.Message, "Редактирование сотрудника", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void label9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void employee_edit_FormClosing(object sender, FormClosingEventArgs e)
        {
            var res = MessageBox.Show("Вы уверены, что хотите выйти вернуться в меню?", "Редактирование сотрудника", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            e.Cancel = !(res == DialogResult.Yes);
        }
    }
}
