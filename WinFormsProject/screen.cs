using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsProject
{
    public partial class screen : Form
    {
        public screen()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            
            Form1 fr = new Form1();
            this.Visible = false;
            this.ShowInTaskbar = false;
            progressBar1.Visible = false;
            timer1.Enabled = false;
            fr.ShowDialog();
            this.Close();
        }
    }
}
