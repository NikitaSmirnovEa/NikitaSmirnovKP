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

namespace vulrill.Клиенты
{
    public partial class client_add : Form
    {
        public client_add()
        {
            InitializeComponent();
            trackBar1.Scroll += trackBar1_Scroll;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = trackBar1.Value.ToString();
        }

        private void client_add_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox4.Text != "" && maskedTextBox1.MaskCompleted) { button3.Enabled = true; }
            else { button3.Enabled = false; }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox4.Text != "" && maskedTextBox1.MaskCompleted) { button3.Enabled = true; }
            else { button3.Enabled = false; }
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            if (textBox3.Text != "" && textBox4.Text != "" && maskedTextBox1.MaskCompleted) { button3.Enabled = true; }
            else { button3.Enabled = false; }
        }

        private void client_add_FormClosing(object sender, FormClosingEventArgs e)
        {
            var res = MessageBox.Show("Вы уверены, что хотите выйти вернуться в меню?", "Добавление клиента", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            e.Cancel = !(res == DialogResult.Yes);
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
                    MySqlCommand cmd = new MySqlCommand($"SELECT phone_number FROM client WHERE phone_number = '{num}';", con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Клиент с таким номером телефона уже существует!", "Добавление клиента", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        goto skipAdd;
                    }
                }

                using (MySqlConnection con = new MySqlConnection(helper.connect))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand($"INSERT INTO client (surname, name, patronymic, phone_number, age) " +
                                                        $"VALUES ('{textBox3.Text}', '{textBox4.Text}', '{textBox5.Text}', '{num}', '{label2.Text}');", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Вы успешно добавили нового клиента!", "Добавление клиента", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox3.Clear(); textBox4.Clear(); textBox5.Clear(); trackBar1.Value = 18; label2.Text = "0"; maskedTextBox1.Clear();
                    if (helper.role != "Администратор") { helper.orderClient = $"{textBox3.Text} {textBox4.Text} {textBox5.Text}"; }
                }

            skipAdd:
                maskedTextBox1.Clear();
            }
            catch (Exception ex) { MessageBox.Show("произошла неизвестная ошибка\n\n" + ex.Message, "Добавление клиента", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
