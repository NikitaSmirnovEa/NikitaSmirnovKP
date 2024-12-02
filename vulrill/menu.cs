using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace vulrill
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            ActiveControl = null;
        }

        string poisk = "";
        string sort = "ASC";

        private void menu_Load(object sender, EventArgs e)
        {
            label4.Text = helper.surname + " " + helper.name;

            dataGridView1.Columns[2].Width = 150;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            viewTable();
        }

        private void viewTable ()
        {
            dataGridView1.Rows.Clear();

            using (MySqlConnection con = new MySqlConnection(helper.connect))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT `name`, cost, image FROM sketch " +
                                                    $"WHERE `name` like '%{poisk}%'" +
                                                    $"ORDER BY cost {sort};", con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j != 2)
                        {
                            dataGridView1.Rows[i].Cells[j].Value = dt.Rows[i][j].ToString();
                        }
                        else
                        {
                            string photo = dt.Rows[i][j].ToString();
                            if (photo == "") { photo = "picture.png"; }
                            Image sketch = new Bitmap($@"{helper.path}\{photo}");
                            dataGridView1.Rows[i].Cells[j].Value = sketch;
                        }
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            poisk = textBox1.Text;
            viewTable();
        }

        private void radioButton1_MouseClick(object sender, MouseEventArgs e)
        {
            radioButton2.Checked = false;
            sort = "ASC";
            viewTable();
        }

        private void radioButton2_MouseClick(object sender, MouseEventArgs e)
        {
            radioButton1.Checked = false;
            sort = "DESC";
            viewTable();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row_id = dataGridView1.CurrentCell.RowIndex;
            helper.orderSketch = dataGridView1.Rows[row_id].Cells[0].Value.ToString();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            this.Hide();
            Заказы ORDER = new Заказы();
            ORDER.ShowDialog();
            this.Show();
        }

        private void menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            var res = MessageBox.Show("Вы действительно хотите выйти из аккаунта?", "Эскизы", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            e.Cancel = !(res == DialogResult.Yes);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
