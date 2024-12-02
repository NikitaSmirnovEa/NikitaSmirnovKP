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

                dataGridView1.DataSource = dt;
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
            row_id = dataGridView1.CurrentCell.RowIndex;
            phone_num = dataGridView1.Rows[row_id].Cells[3].Value.ToString();
            button2.Enabled = true; button1.Enabled = true;
            helper.loginEdit = phone_num;
            FIO = dataGridView1.Rows[row_id].Cells[0].Value.ToString() + " " + dataGridView1.Rows[row_id].Cells[1].Value.ToString() + " " + dataGridView1.Rows[row_id].Cells[2].Value.ToString();

            view();
            dataGridView1.Rows[row_id].DefaultCellStyle.BackColor = Color.FromArgb(24, 23, 28);
            dataGridView1.Rows[row_id].DefaultCellStyle.ForeColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            client_edit client_Edit = new client_edit();
            client_Edit.ShowDialog();
            this.Show();
            view();
        }
    }
}
