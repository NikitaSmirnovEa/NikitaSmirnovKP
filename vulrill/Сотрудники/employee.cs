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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace vulrill
{
    public partial class employee : Form
    {
        public employee()
        {
            InitializeComponent();
        }

        int row_id = 0;
        string login = "";

        private void employee_Load(object sender, EventArgs e)
        {
            label4.Text = helper.surname + " " + helper.name;
            view();
        }

        private void view()
        {
            using (MySqlConnection con = new MySqlConnection(helper.connect))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT login As 'Логин', `role`.`name` AS 'Роль', surname AS 'Фамилия', employee.`name` AS 'Имя', patronymic AS 'Отчество', phone_number AS 'Номер телефона' FROM employee " +
                                                    $"INNER JOIN `role` ON `role`.id_role = employee.role_id;", con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            employee_edit emp_edit = new employee_edit();
            emp_edit.ShowDialog();
            this.Show();
            view();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show($"Вы уверены, что хотите удалить сотрудника {login}?", "Удаление сотрудника", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                if (login != helper.login)
                {
                    try
                    {
                        using (MySqlConnection con = new MySqlConnection(helper.connect))
                        {
                            con.Open();
                            MySqlCommand cmd = new MySqlCommand($"DELETE FROM employee WHERE login = '{login}';", con);
                            cmd.ExecuteNonQuery();

                            view();
                            MessageBox.Show("Вы успешно удалили сотрудника!", "Удаление сотрудника", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            button1.Enabled = false; button2.Enabled = false;
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("произошла неизвестная ошибка\n\n" + ex.Message, "Удаление сотрудника", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else { MessageBox.Show("Вы не можете удалить самого себя,\nно попытка была не плохая", "Удаление сотрудника", MessageBoxButtons.OK, MessageBoxIcon.Information); } 
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            row_id = dataGridView1.CurrentCell.RowIndex;
            login = dataGridView1.Rows[row_id].Cells[0].Value.ToString();
            button2.Enabled = true; button1.Enabled = true;
            helper.loginEdit = login;

            view();
            dataGridView1.Rows[row_id].DefaultCellStyle.BackColor = Color.FromArgb(24, 23, 28);
            dataGridView1.Rows[row_id].DefaultCellStyle.ForeColor = Color.White;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            employee_add emp_add = new employee_add();
            emp_add.ShowDialog();
            this.Show();
            view();
        }
    }
}
