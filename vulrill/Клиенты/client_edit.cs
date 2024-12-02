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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace vulrill.Клиенты
{
    public partial class client_edit : Form
    {
        public client_edit()
        {
            InitializeComponent();
            trackBar1.Scroll += trackBar1_Scroll;
        }

        private void client_edit_Load(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(helper.connect))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand($"SELECT surname, `name`, patronymic, phone_number, age FROM client " +
                                                        $"WHERE phone_number = '{helper.loginEdit}';", con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    textBox3.Text = dt.Rows[0][0].ToString();
                    textBox4.Text = dt.Rows[0][1].ToString();
                    textBox5.Text = dt.Rows[0][2].ToString();
                    label2.Text = dt.Rows[0][4].ToString();
                    trackBar1.Value = Convert.ToInt32(label2.Text);
                    maskedTextBox1.Text = dt.Rows[0][3].ToString();
                }
            }
            catch (Exception ex) { MessageBox.Show("произошла неизвестная ошибка\n\n" + ex.Message, "Редактирвоание клиента", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = trackBar1.Value.ToString();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void client_edit_FormClosing(object sender, FormClosingEventArgs e)
        {
            var res = MessageBox.Show("Вы уверены, что хотите выйти вернуться в меню?", "Редактирование клиента", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            e.Cancel = !(res == DialogResult.Yes);
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

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

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
                    MySqlCommand cmd = new MySqlCommand($"UPDATE client SET `surname` = '{textBox3.Text}', `name` = '{textBox4.Text}', `patronymic` = '{textBox5.Text}', `phone_number` = '{num}', age = {label2.Text} " +
                                                        $"WHERE (phone_number = '{helper.loginEdit}');", con);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Вы успешно изменили клиента!", "Редактирование клиента", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    helper.loginEdit = num;
                }
            }
            catch (Exception ex) { MessageBox.Show("произошла неизвестная ошибка\n\n" + ex.Message, "Редактирование клиента", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox4.Text != "" && maskedTextBox1.MaskCompleted) { button3.Enabled = true; }
            else { button3.Enabled = false; }
        }
    }
}
