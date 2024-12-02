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

namespace vulrill.Эскизы
{
    public partial class sketch_edit : Form
    {
        public sketch_edit()
        {
            InitializeComponent();
        }

        string pictureName = "";

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "") { button3.Enabled = true; }
            else { button3.Enabled = false; }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < '0' || l > '9') && l != 8)
            { e.Handled = true; }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "") { button3.Enabled = true; }
            else { button3.Enabled = false; }
        }

        private void sketch_edit_FormClosing(object sender, FormClosingEventArgs e)
        {
            var res = MessageBox.Show("Вы уверены, что хотите выйти вернуться в меню?", "Редактирование эскиза", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            e.Cancel = !(res == DialogResult.Yes);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Bitmap imageFile = null;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = helper.path;
            openFileDialog1.Filter = "джипегешка (*.jpg)|*.jpg|пнгешка (*.png)|*.png|все файлы (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                imageFile = new Bitmap(openFileDialog1.FileName);
                string[] words = openFileDialog1.FileName.Split('\\');
                if (words[words.Length - 1] != "picture.png") { pictureName = words[words.Length - 1]; }
                else { pictureName = ""; }
                pictureBox1.Image = imageFile;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(helper.connect))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand($"UPDATE sketch SET `name` = '{textBox1.Text}', `cost` = '{textBox2.Text}', `image` = '{pictureName}' " +
                                                        $"WHERE (`name` = '{helper.loginEdit}');", con);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Вы успешно изменили эскиз!", "Редактирование эскиза", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    helper.loginEdit = textBox1.Text;
                }
            }
            catch (Exception ex) { MessageBox.Show("произошла неизвестная ошибка\n\n" + ex.Message, "Редактирование эскиза", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void sketch_edit_Load(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(helper.connect))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT name, cost, image  FROM sketch WHERE name = '{helper.loginEdit}';", con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                textBox1.Text = dt.Rows[0][0].ToString();
                textBox2.Text = dt.Rows[0][1].ToString();
                if (dt.Rows[0][2].ToString() != "") { pictureBox1.ImageLocation = helper.path + dt.Rows[0][2].ToString(); }
                else { pictureBox1.ImageLocation = helper.path + "picture.png"; }
            }
        }
    }
}
