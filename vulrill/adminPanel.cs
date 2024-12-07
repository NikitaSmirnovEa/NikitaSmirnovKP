using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using vulrill.Эскизы;
using System.Timers;

namespace vulrill
{
    public partial class adminPanel : Form
    {
 

        public adminPanel()
        {
            InitializeComponent();

        }

        private void adminPanel_Load(object sender, EventArgs e)
        {
            label2.Text = helper.surname + " " + helper.name;
        
        }
        private void adminPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
          
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            employee emp = new employee();
            emp.ShowDialog();
            this.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            master MASTER = new master();
            MASTER.ShowDialog();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Hide();
            using (client CLIENT = new client())
            {
                CLIENT.ShowDialog();
            }
            Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            menu SKETCH = new menu();
            SKETCH.ShowDialog();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            report rep = new report();
            rep.ShowDialog();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            import imp = new import();
            imp.ShowDialog();
            this.Hide();
        }
    }
}
