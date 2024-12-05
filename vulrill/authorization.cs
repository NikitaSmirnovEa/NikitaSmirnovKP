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
using System.Configuration;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;

namespace vulrill
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string captchaText;
        string conString = helper.connect;
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
            if (textBox1.Text == localAdmin.localName)
            {
                if (textBox2.Text == localAdmin.localPassword)
                {
                    helper.role = "Локальный";
                    adminPanel admin = new adminPanel();
                    this.Visible = false;
                    admin.ShowDialog();
                    this.Visible = true;
                }
                else
                {
                    MessageBox.Show("Неверный пароль");
                    textBox2.Clear();
                    Captha();
                }
                return;
            }
            string login = textBox1.Text;
            string password = helper.CreateMD5(textBox2.Text);

            // Load admin credentials from config
            string adminUsername = ConfigurationManager.AppSettings["AdminUsername"];
            string adminPassword = ConfigurationManager.AppSettings["AdminPassword"];

            if (login == adminUsername && password == helper.CreateMD5(adminPassword))
            {
                // If the credentials match, open the import form
                this.Hide();
                import importForm = new import(); // Oткрываем форму import
                importForm.ShowDialog();
                this.Show();
                return;
            }

            using (MySqlConnection con = new MySqlConnection(helper.connect))
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

                            MessageBox.Show($"Здравствуйте, {helper.name}!", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox1.Clear(); textBox2.Clear();

                            if (helper.role == "Администратор")
                            {
                                this.Hide();
                                adminPanel ap = new adminPanel();
                                ap.ShowDialog();
                                this.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неверный пароль");
                            textBox2.Text = "";
                            Captha();

                          
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Captha();
                }
            }
        }
        private void Captha() //Создание капчи
        {
            button4.Enabled = true;
            button5.Enabled = true;
            textBox3.Enabled = true;
            CaptchaToImage();
            button1.Enabled = false;
            textBox1.Text = null;
            textBox2.Text = null;
            this.Width = 700;
        }
        private void CaptchaToImage() //Отрисовка капчи
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            captchaText = ""; for (int i = 0; i < 5; i++)
            {
                captchaText += chars[random.Next(chars.Length)];
            }
            Bitmap bmp = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.SmoothingMode = SmoothingMode.AntiAlias; graphics.Clear(Color.Gray);
            Font font = new Font("Arial", 25, FontStyle.Bold);
            for (int i = 0; i < 5; i++)
            {
                PointF point = new PointF(i * 20, 0);
                graphics.TranslateTransform(100, 50);
                graphics.RotateTransform(random.Next(-10, 10));
                graphics.DrawString(captchaText[i].ToString(), font, Brushes.Black, point);
                graphics.ResetTransform();
            }
            for (int i = 0; i < 10; i++)
            {
                Pen pen = new Pen(Color.Black, random.Next(2, 5));
                int x1 = random.Next(pictureBox2.Width);
                int y1 = random.Next(pictureBox2.Height);
                int x2 = random.Next(pictureBox2.Width);
                int y2 = random.Next(pictureBox2.Height); graphics.DrawLine(pen, x1, y1, x2, y2);
            }
            pictureBox2.Image = bmp;
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

        private void button4_Click(object sender, EventArgs e)
        {
            Captha();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == captchaText)
            {
                button1_Click(sender, e);
            }
            else //Блокировка системы на 10 секунд посленеудачного ввода
            {
                MessageBox.Show("Неверный ввод, блокировка системы на 10 секунд");
                button5.Enabled = false;
                button4.Enabled = false;
                Thread.Sleep(10000);
                button5.Enabled = true;
                button4.Enabled = true;
                Captha();
            }
        }
    }
}
