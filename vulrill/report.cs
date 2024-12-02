using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace vulrill
{
    public partial class report : Form
    {
        public report()
        {
            InitializeComponent();
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        string startDate = "2024-10-01";
        string endDate = DateTime.Now.ToString("yyyy-MM-dd");

        private void report_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MaxDate = DateTime.Now;
            dateTimePicker2.MaxDate = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

            view();
        }

        private void view()
        {
            dataGridView1.Rows.Clear();

            using (MySqlConnection con = new MySqlConnection(helper.connect))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT sketch.`name`, sketch.cost, sketch.image, " +
                                                    "`master`.surname, `master`.`name`, `master`.patronymic, " +
                                                    "`client`.surname, `client`.`name`, `client`.patronymic, " +
                                                    "`employee`.surname, `employee`.`name`, `employee`.patronymic, " +
                                                    "`date` FROM `order` " +
                                                    "INNER JOIN sketch ON sketch_id = sketch.id_sketch " +
                                                    "INNER JOIN `master` ON master_id = `master`.id_master " +
                                                    "INNER JOIN `client` ON client_id = `client`.id_client " +
                                                    "INNER JOIN `employee` ON employee_id = `employee`.id_employee " +
                                                    $"WHERE `date` >= '{startDate}' AND `date` <= '{endDate}';", con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();

                    dataGridView1.Rows[i].Cells[0].Value = dt.Rows[i][0].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i][1].ToString();
                    string photo = dt.Rows[i][2].ToString();
                    if (photo == "") { photo = "picture.png"; }
                    Image sketch = new Bitmap($@"{helper.path}\{photo}");
                    dataGridView1.Rows[i].Cells[2].Value = sketch;
                    dataGridView1.Rows[i].Cells[3].Value = dt.Rows[i][3].ToString() + " " + dt.Rows[i][4].ToString() + " " + dt.Rows[i][5].ToString();
                    dataGridView1.Rows[i].Cells[4].Value = dt.Rows[i][6].ToString() + " " + dt.Rows[i][7].ToString() + " " + dt.Rows[i][8].ToString();
                    dataGridView1.Rows[i].Cells[5].Value = dt.Rows[i][9].ToString() + " " + dt.Rows[i][10].ToString() + " " + dt.Rows[i][11].ToString();
                    dataGridView1.Rows[i].Cells[6].Value = dt.Rows[i][12].ToString().Replace(" 0:00:00", "");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int helper;
            try
            {
                Excel.Application excel = new Excel.Application();
                excel.Visible = true;
                Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
                int j = 0, i = 0;

                for (j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    helper = j;
                    if (helper >= 3) { helper--; }
                    Excel.Range myRange = (Excel.Range)sheet1.Cells[1, helper + 1];
                        myRange.Value = dataGridView1.Columns[j].HeaderText;
                    
                }

                for (i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        helper = j;
                        if (helper >= 3) { helper--; }
                        Excel.Range myRange = (Excel.Range)sheet1.Cells[2 + i, helper + 1];
                        myRange.Value = dataGridView1[j, i].Value == null ? "" : dataGridView1[j, i].Value.ToString();
                    }
                }

                var Rng = sheet1.get_Range("B:B");
                double sum = excel.WorksheetFunction.Sum(Rng);
                Excel.Range range = (Excel.Range)sheet1.Cells[1, 8];
                range.Value = $"Итоговая полученная сумм в период от {dateTimePicker1.Value.ToString("dd.MM.yyyy")} до {dateTimePicker2.Value.ToString("dd.MM.yyyy")}";
                Excel.Range range2 = (Excel.Range)sheet1.Cells[2, 8];
                range2.Value = sum;             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            startDate = dateTimePicker1.Text.ToString();
            view();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            endDate = dateTimePicker2.Text.ToString();
            view();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Text = "2024-10-01";
            startDate = "2024-10-01";
            endDate = DateTime.Now.ToString("yyyy-MM-dd");
            dateTimePicker2.Text = endDate;
            view();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void report_FormClosing(object sender, FormClosingEventArgs e)
        {
            var res = MessageBox.Show("Вы действительно вернуться в меню?", "Отчёт услуг", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            e.Cancel = !(res == DialogResult.Yes);
        }
    }
}
