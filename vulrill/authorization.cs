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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            ActiveControl = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                using(MySqlConnection con = new MySqlConnection(helper.connect))
                {
                    con.Open();
                }
            }
            catch 
            { 
                MessageBox.Show("Произошла ошибка подключения к БД", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            button1.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "") { button1.Enabled = true; }
            else {  button1.Enabled = false; }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "") { button1.Enabled = true; }
            else { button1.Enabled = false; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = helper.CreateMD5(textBox2.Text);

            using(MySqlConnection con = new MySqlConnection(helper.connect))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand($"SELECT login, `password`, role_id, surname, `name`, patronymic FROM employee WHERE login = '{login}'", con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count != 0)
                    {
                        if (dt.Rows[0][1].ToString() == password)
                        {
                            helper.login = dt.Rows[0][0].ToString();
                            if (dt.Rows[0][2].ToString() == "1") { helper.role = "Администратор"; }
                            else { helper.role = "Менеджер"; }
                            helper.surname = dt.Rows[0][3].ToString();
                            helper.name = dt.Rows[0][4].ToString();
                            helper.patronymic = dt.Rows[0][5].ToString();

                            MessageBox.Show($"Здраствуйте, {helper.name}!", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox1.Clear(); textBox2.Clear();

                            if (helper.role == "Администратор")
                            {
                                this.Hide();
                                adminPanel ap = new adminPanel();
                                ap.ShowDialog();
                                this.Show();
                            }
                            else
                            {
                                this.Hide();
                                menu MENU = new menu();
                                MENU.ShowDialog();
                                this.Show();
                            }
                        }
                        else { MessageBox.Show("Неверный пароль", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    }
                    else { MessageBox.Show("Сотрудник с таким логином не найден", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
                catch { MessageBox.Show("Произошла неизвестная ошибка", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
         
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
          
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var res = MessageBox.Show("Вы действительно хотите закрыть программу?", "Выход из приложения", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            e.Cancel = !(res == DialogResult.Yes);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

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
    }
}
