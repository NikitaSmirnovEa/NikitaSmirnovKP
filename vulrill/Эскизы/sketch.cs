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

namespace vulrill.Эскизы
{
    public partial class sketch : Form
    {
        public sketch()
        {
            InitializeComponent();
        }

        int row_id;

        private void sketch_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            viewTable();
        }

        private void viewTable()
        {
            dataGridView1.Rows.Clear();

            using (MySqlConnection con = new MySqlConnection(helper.connect))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT `name`, cost, image FROM sketch;", con);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            row_id = dataGridView1.CurrentCell.RowIndex;
            button2.Enabled = true; button1.Enabled = true;
            helper.loginEdit = dataGridView1.Rows[row_id].Cells[0].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            sketch_add sketch_Add = new sketch_add();
            sketch_Add.ShowDialog();
            this.Show();
            viewTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show($"Вы уверены, что хотите удалить эскиз '{helper.loginEdit}'?", "Удаление эскиза", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(helper.connect))
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand($"DELETE FROM sketch WHERE name = '{helper.loginEdit}';", con);
                        cmd.ExecuteNonQuery();

                        viewTable();
                        MessageBox.Show("Вы успешно удалили эскиз!", "Удаление эскиза", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button1.Enabled = false; button2.Enabled = false;
                    }
                }
                catch (Exception ex) { MessageBox.Show("произошла неизвестная ошибка\n\n" + ex.Message, "Удаление эскиза", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            sketch_edit sketch_Edit = new sketch_edit();
            sketch_Edit.ShowDialog();
            this.Show();
            viewTable();
        }
    }
}
