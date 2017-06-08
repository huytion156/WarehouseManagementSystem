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
    public partial class FormNewPack : Form
    {
        private Form1 frm;
        private Form2 frm2;
        public FormNewPack()
        {
            InitializeComponent();
        }
        public FormNewPack(Form1 form1)
        {
            InitializeComponent();
            frm = form1;
        }
        public FormNewPack(Form2 form2)
        {
            InitializeComponent();
            frm = form2.frm;
            frm2 = form2;
        }
        public FormNewPack(int a, int x, int y, Form1 form1)
        {
            InitializeComponent();
            frm = form1;
            lsA.Value = a;
            lsA.Maximum = frm.noA;
            lsX.Value = x;
            lsX.Maximum = frm.arr[a].mN;
            lsY.Value = y;
            lsY.Maximum = frm.arr[a].mM;
        }

        public FormNewPack(int a, int x, int y, Form2 form2)
        {
            InitializeComponent();
            frm = form2.frm;
            frm2 = form2;
            lsA.Value = a;
            lsA.Maximum = frm.noA;
            lsX.Value = x;
            lsX.Maximum = frm.arr[a].mN;
            lsY.Value = y;
            lsY.Maximum = frm.arr[a].mM;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lbNoitice.Text) != true) return;
            if (string.IsNullOrEmpty(tbID.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                lbNoitice.Text = "ID or Price must not empty!";
            }
            else
            {
                int a, x, y;
                x = Int32.Parse(lsX.Value.ToString());
                y = Int32.Parse(lsY.Value.ToString());
                a = Int32.Parse(lsA.Value.ToString());

                if (lsType.Value.ToString() == "1")
                { // kien hang loai 1
                    if (frm.arr[a].getIDatXY(x, y) != 0)
                    {
                        lbNoitice.Text = "The package location is invalid!";
                        return; // neu vi tri dich khong ton tai hoac da co kien hang khac
                    }
                }
                else //kien hang loai 2
                {
                    if (frm.arr[a].isEmpty(x, y, 2) == 0) // neu x2,y2 la vi tri dau kien hang va khong con trong
                        if (frm.arr[a].isEmpty(x, y - 1, 2) == 0) // ney x2,y2 la vi tri duoi kien hang va khong con trong
                        {
                            lbNoitice.Text = "The package location is invalid!";
                            return;
                        }
                        else y--; // chuyen vi tri dich len dau kien hang
                }
                lbNoitice.Text = "";
                frm.createNewPack(a, x, y, Int32.Parse(lsType.Value.ToString()), float.Parse(textBox2.Text),
                                             tbID.Text, dateTimePicker1.Text);

                frm.Send_create(lsA.Value.ToString(), lsX.Value.ToString(), lsY.Value.ToString(), lsType.Value.ToString(), textBox2.Text,
                                              tbID.Text, dateTimePicker1.Text, sender, e);
                this.Close();
            }
        }
       
        private void lsA_ValueChanged(object sender, EventArgs e)
        {
            int a = Int32.Parse(lsA.Value.ToString());
            lsX.Maximum = frm.arr[a].mN;
            lsY.Maximum = frm.arr[a].mM;
        }

        private void FormNewPack_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm.Refresh();
            if (frm2!=null) frm2.Refresh();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            float result;
            bool check = float.TryParse(textBox2.Text, out result);
            if (check == false)
                lbNoitice.Text = "Price must be a number!";
            else lbNoitice.Text = "";
        }

        private void tbID_TextChanged(object sender, EventArgs e)
        {
            if (tbID.Text.Contains(' ') == true)
            {
                lbNoitice.Text = "ID must not contain space character!";
            }
            else lbNoitice.Text = "";
        }
    }
}
