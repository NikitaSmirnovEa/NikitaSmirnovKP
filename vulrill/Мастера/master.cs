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
using System.Timers;

namespace vulrill
{
    public partial class master : Form
    {
        private System.Timers.Timer inactivityTimer;
        private int inactivityLimit; // Параметр бездействия в миллисекундах

  
        public master()
        {

            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            // Настройка параметра бездействия (30 секунд по умолчанию)
            inactivityLimit = 10000; // 30 секунд

            // Инициализация таймера
            inactivityTimer = new System.Timers.Timer(inactivityLimit);
            inactivityTimer.Elapsed += OnInactivityTimeout;
            inactivityTimer.AutoReset = false; // Остановить таймер после первого срабатывания
            inactivityTimer.Start();
        }
        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            // Сброс таймера при движении мыши
            ResetInactivityTimer();
        }

        private void OnInactivityTimeout(object sender, ElapsedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate {
                // Перенаправление на форму авторизации
                MessageBox.Show("Блокировка системы в случаи отсутствия активности! ");
                Form1 authForm = new Form1();
                authForm.Show();
                this.Hide();
            });
        }


        private void ResetInactivityTimer()
        {
            // Сброс таймера
            inactivityTimer.Stop();
            inactivityTimer.Start();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Обработка нажатия клавиш
            ResetInactivityTimer();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        int row_id = 0;
        string phone_num = "";
        string FIO = "";
        string exp1 = "0"; string exp2 = "100";

        private void master_Load(object sender, EventArgs e)
        {
            label4.Text = helper.surname + " " + helper.name;
            comboBox1.SelectedIndex = 0;
            view();
        }
        private void view()
        {
            using (MySqlConnection con = new MySqlConnection(helper.connect))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT surname AS 'Фамилия', `name` AS 'Имя', patronymic AS 'Отчество', experience AS 'Стаж работы', phone_number AS 'Номер телефона' FROM `master` " +
                                                    $"WHERE experience >= {exp1} AND experience <= {exp2};", con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
                dataGridView1.DataSource = dt;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            master_add master_Add = new master_add();
            master_Add.ShowDialog();
            this.Show();
            view();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show($"Вы уверены, что хотите удалить мастера \"{FIO}\"?", "Удаление мастера", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(helper.connect))
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand($"DELETE FROM master WHERE phone_number = '{phone_num}';", con);
                        cmd.ExecuteNonQuery();

                        view();
                        MessageBox.Show("Вы успешно удалили мастера!", "Удаление мастера", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button1.Enabled = false; button2.Enabled = false;
                    }
                }
                catch (Exception ex) { MessageBox.Show("произошла неизвестная ошибка\n\n" + ex.Message, "Удаление мастера", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            master_edit master_EDIT = new master_edit();
            master_EDIT.ShowDialog();
            this.Show();
            view();
        }
            
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            row_id = dataGridView1.CurrentCell.RowIndex;
            phone_num = dataGridView1.Rows[row_id].Cells[4].Value.ToString();
            button2.Enabled = true; button1.Enabled = true;
            helper.loginEdit = phone_num;
            FIO = dataGridView1.Rows[row_id].Cells[0].Value.ToString() + " " + dataGridView1.Rows[row_id].Cells[1].Value.ToString() + " " + dataGridView1.Rows[row_id].Cells[2].Value.ToString();

            view();
            dataGridView1.Rows[row_id].DefaultCellStyle.BackColor = Color.FromArgb(24, 23, 28);
            dataGridView1.Rows[row_id].DefaultCellStyle.ForeColor = Color.White;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) { exp1 = "0"; exp2 = "100"; }
            else if (comboBox1.SelectedIndex == 1) { exp1 = "0"; exp2 = "2"; }
            else if (comboBox1.SelectedIndex == 2) { exp1 = "3"; exp2 = "5"; }
            else if (comboBox1.SelectedIndex == 3) { exp1 = "6"; exp2 = "9"; }
            else if (comboBox1.SelectedIndex == 4) { exp1 = "10"; exp2 = "100"; }
            view();
        }
    }
}
