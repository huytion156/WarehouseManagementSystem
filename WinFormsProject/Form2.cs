//version 9:15pm - 1/6/2017
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Net.Sockets;
using ComponentFactory.Krypton.Toolkit;
namespace WinFormsProject
{
    public partial class Form2 : KryptonForm
    {
        private const int penWidth = 2; //pen Width
        Color penCol = Color.Blue;      //pen Color
        public Form1 frm;
        int id;
        #region ATTRIBUTES
        // Attributes: 
        private float zoom = 1f;
        private float Xpos = 0f;
        private float Ypos = 0f;
        #endregion
        public Form2()
        {
            InitializeComponent();
            this.trackBar1.Value = 100; 
            btRemovePack.Enabled = false;
        }
        public Form2(Form1 frm1, int Area_id)
        {
            frm = frm1;
            id = Area_id;
            InitializeComponent();
            this.trackBar1.Value = 100;
            btRemovePack.Enabled = false;
            lbAreaID.Text += Area_id.ToString();
            Xpos = - frm.arr[id].mX +50;
            Ypos = - frm.arr[id].mY +50;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            // Read Data From Text File
        }
        #region DRAWING_AREAS
        private void panelWareHouse_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.ScaleTransform(zoom, zoom);
            drawArea(frm.arr[id], e.Graphics);
        }
        public void drawArea(Area area, System.Drawing.Graphics graphics)
        {
            Matrix myMatrix = new Matrix();
            myMatrix.Rotate(area.mDeg, MatrixOrder.Append);
            myMatrix.Scale(zoom, zoom, MatrixOrder.Append);
            myMatrix.Translate(Xpos, Ypos, MatrixOrder.Append);
            System.Drawing.Pen myPen = new System.Drawing.Pen(penCol, penWidth); // outline of rectangle 

            for (int i = 0; i < area.mN; i++)
                for (int j = 0; j < area.mM; j++)
                {
                    System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(area.c[i, j]);
                    graphics.Transform = myMatrix;
                    System.Drawing.Rectangle rectangle = area.getDrawingRectangle(i, j);
                    graphics.DrawRectangle(myPen, rectangle);
                    graphics.FillRectangle(myBrush, rectangle);
                }
        }

        #endregion

        #region PRINT_ADD_MOVE_DELETE_PACKAGE
        private void printPackageInfo(int id)
        {
            if ((id > 0) && (id <= frm.noP))
            {
                lbType.Text = frm.packages[id - 1].getType().ToString();
                lbID.Text = frm.packages[id - 1].mId.ToString();
                lbX.Text = frm.packages[id - 1].mx.ToString();
                lbY.Text = frm.packages[id - 1].my.ToString();
                lbArea.Text = frm.packages[id - 1].mArea.ToString();
                lbDay.Text = frm.packages[id - 1].mDay;
                lbPrice.Text = frm.packages[id - 1].mPrice.ToString();
            }
        }
        #endregion

        #region ZOOM_SCROLL_PANEL
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            zoom = trackBar1.Value / 100f;
            lbZoom.Text = trackBar1.Value.ToString() + "%";
            panelWareHouse.Refresh();
        }

        private void panelWareHouse_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                Xpos = -e.NewValue;
            }
            else
                Ypos = -e.NewValue;
            panelWareHouse.Refresh();
        }
        #endregion

        #region BUTTON_EVENTS
        private void btMove_Click(object sender, EventArgs e) // MOVE PACKAGE
        {

            if ((string.IsNullOrWhiteSpace(tbFrom.Text)) || (string.IsNullOrWhiteSpace(tbTo.Text)))
            {
                MessageBox.Show("From and To must not empty!", "Cannot Move:");
                return;
            }
            string[] from = tbFrom.Text.Split(' ');
            string[] to = tbTo.Text.Split(' ');
            int check = 0;
            check = frm.movePack(Int32.Parse(from[1]), Int32.Parse(from[2]), Int32.Parse(from[0]), Int32.Parse(to[1]), Int32.Parse(to[2]), Int32.Parse(to[0]));
            if (check == 0) MessageBox.Show("Cannot Move !");
            else
            {
                frm.Send_move(tbFrom.Text, tbTo.Text, sender, (EventArgs)e);
                //textBox2.Text = "";
                //textBox2.Text = "M " + tbFrom.Text + " " + tbTo.Text;
                //button1_Click(sender, (EventArgs)e);
            }
            panelWareHouse.Refresh();
        }
        /*
        private void btAddPack_Click(object sender, EventArgs e) //ADD NEW PACKAGE
        {
            FormNewPack fnp;
            if (string.IsNullOrEmpty(tbFrom.Text))
            {
                fnp = new FormNewPack(this);
            }
            else
            {
                string[] from = tbFrom.Text.Split(' ');
                fnp = new FormNewPack(Int32.Parse(from[0]), Int32.Parse(from[1]), Int32.Parse(from[2]), this);
            }
            fnp.Show();
        }*/

        private void btRemovePack_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete package \" " + lbID.Text + " \" ?",
                                     "Delete Package:",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                frm.deletePackage(Int32.Parse(lbArea.Text), Int32.Parse(lbX.Text), Int32.Parse(lbY.Text));
                frm.Send_delete(lbArea.Text, lbX.Text, lbY.Text, sender, (EventArgs) e);
                //textBox2.Text = "";
                //textBox2.Text = "D " + lbArea.Text + " " + lbX.Text + " " + lbY.Text;
                //button1_Click(sender, (EventArgs)e);
            }
            else
            {
                // If 'No', do something here.
            }
            panelWareHouse.Refresh();
        }
        #endregion

        #region MOUSE_EVENTS
        bool is_Move = false; //State kiem tra trang thai hieu ung Drag and Drop
        int box_type = -1; //Kiểu box (đỏ hoặc xanh)
        Rectangle rec_preview = new Rectangle(); //Hiệu ứng quét chọn
        private bool isDragDrop = false; // Dang thuc hien keo tha

        #region CHECK_MOUSE_IN_PACKAGE_AREA

        private Point TransformMouseClick(MouseEventArgs e)
        {
            Point[] arrayPoint = new Point[1];
            arrayPoint[0] = new Point(e.X, e.Y);
            Matrix myMatrix = new Matrix();
            myMatrix.Translate(-Xpos, -Ypos, MatrixOrder.Append);
            myMatrix.Scale(1 / zoom, 1 / zoom, MatrixOrder.Append);
            myMatrix.TransformPoints(arrayPoint);
            return arrayPoint[0];
        }
        private Point RotatePoint(float angle, Point pt)
        {
            var a = angle * System.Math.PI / 180.0;
            double cosa = Math.Cos(a), sina = Math.Sin(a);
            return new Point(Convert.ToInt32(pt.X * cosa - pt.Y * sina), Convert.ToInt32(pt.X * sina + pt.Y * cosa));
        }
        private int[] MouseInRectangle(MouseEventArgs e)
        {
            Point p = new Point(0, 0);
//x            for (int k = 0; k < noA; k++)
            int k = id;
            {
                for (int i = 0; i < frm.arr[k].mN; i++)
                    for (int j = 0; j < frm.arr[k].mM; j++)
                    {
                        p = RotatePoint(-frm.arr[k].mDeg, TransformMouseClick(e));
                        if (frm.arr[k].inRec(i, j, p) == 1)
                        {
                            int[] temp = new int[3];
                            temp[0] = k;
                            temp[1] = i;
                            temp[2] = j;
                            return temp;
                        }
                    }
            }
            return null;
        }
        #endregion
        //Bắt đầu dời box khi ấn chuột xuống
        private void panelWareHouse_MouseDown(object sender, MouseEventArgs e)
        {
            //Xử lí dời box bằng chuột trái
            int[] res = MouseInRectangle(e);
            if (e.Button == MouseButtons.Left)
            {
                if (res != null)
                {
                    int k = res[0];
                    int i = res[1];
                    int j = res[2];
                    int id = frm.arr[k].getIDatXY(i, j);
                    tbFrom.Text = k.ToString() + " " + i.ToString() + " " + j.ToString();
                    printPackageInfo(frm.arr[k].getIDatXY(i, j));
                    box_type = id; //lay ID cua kien hang tai vi tri x1,y1,a1
                    if (id >= 1) btRemovePack.Enabled = true; // neu co kien hang, enable nut delete
                }
                is_Move = true; //Bật hiệu ứng dời box
                if (box_type > 0) box_type = frm.packages[box_type - 1].getType();//Kiểm tra dời box xanh hay box đỏ
                else box_type = -1;
            }
            else
            {
                //Xử lí quét chọn bằng chuột phải
                rec_preview.Location = new Point(e.X, e.Y);
                is_Move = true;
                if (res != null) tbTo.Text = res[0].ToString() + " " + res[1].ToString() + " " + res[2].ToString();
            }
        }


        private void panelWareHouse_MouseMove(object sender, MouseEventArgs e)
        {

            #region Xu li chuot trai
            if (e.Button == MouseButtons.Left)
            {
                isDragDrop = true;
                if (box_type == -1) return;
                int width = 10 * trackBar1.Value / 100; //Lay chieu rong box sau khi calib
                int height = 15 * trackBar1.Value / 100; //Lay chieu dai box sau khi calib
                panelWareHouse.Invalidate(); //Xoa di nhung net ve cu
                if (is_Move) //Neu cho phep ve (chuot dang di chuyen)
                {
                    Graphics graph = panelWareHouse.CreateGraphics();

                    Rectangle rect = ClientRectangle;
                    rect.Location = new Point(e.X, e.Y);
                    rect.Size = new Size(width, height);

                    if (box_type == 2)
                    {
                        using (Brush brush = new SolidBrush(Color.Green))
                        {
                            rect.Size = new Size(width, height * 2);
                            graph.FillRectangle(brush, rect);
                        }
                    }
                    else if (box_type == 1)
                    {
                        using (Brush brush = new SolidBrush(Color.Red))
                        {
                            graph.FillRectangle(brush, rect);
                        }
                    }
                }
            }
            #endregion

            #region Xu li chuot phai
            if (e.Button == MouseButtons.Right)
            {
                isDragDrop = true;
                int width = Math.Abs((e.X - rec_preview.X)) * trackBar1.Value / 100; //Lay chieu rong box sau khi calib
                int height = Math.Abs((e.Y - rec_preview.Y)) * trackBar1.Value / 100; //Lay chieu dai box sau khi calib
                panelWareHouse.Invalidate();
                if (is_Move)
                {
                    Graphics graph = panelWareHouse.CreateGraphics();
                    rec_preview.Size = new Size(width, height);
                    using (Brush brush = new SolidBrush(Color.DeepSkyBlue))
                    {
                        graph.FillRectangle(brush, rec_preview);
                    }
                }
            }
            #endregion

        }

        //Lấy khoảng cách
        double getDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        //Xử lí khi thả chuột  ra 
        private void panelWareHouse_MouseUp(object sender, MouseEventArgs e)
        {
            #region Xu Li Chuot Trai
            Point p = new Point(0, 0);
            if ((e.Button == MouseButtons.Left) && isDragDrop)
            {
                isDragDrop = false;
                int[] res = MouseInRectangle(e);
                if (res != null)
                {
                    tbTo.Text = res[0].ToString() + " " + res[1].ToString() + " " + res[2].ToString();
                }

                is_Move = false; //Tắt hiệu ứng kéo thả
                //Bắt đầu dời
                if ((string.IsNullOrWhiteSpace(tbFrom.Text)) || (string.IsNullOrWhiteSpace(tbTo.Text)))
                {
                    MessageBox.Show("From and To must not empty!", "Cannot Move:");
                    return;
                }
                string[] from = tbFrom.Text.Split(' ');
                string[] to = tbTo.Text.Split(' ');
                int check = 0;
                check = frm.movePack(Int32.Parse(from[1]), Int32.Parse(from[2]), Int32.Parse(from[0]), Int32.Parse(to[1]), Int32.Parse(to[2]), Int32.Parse(to[0]));
                if (check == 0) MessageBox.Show("Cannot Move !");
                else
                {
                    frm.Send_move(tbFrom.Text, tbTo.Text, sender, (EventArgs) e);
                    //textBox2.Text = "";
                    //textBox2.Text = "M " + tbFrom.Text + " " + tbTo.Text;
                    //button1_Click(sender, (EventArgs)e);
                }
                panelWareHouse.Refresh();
            }
            #endregion

            #region Xu li chuot phai
            if ((e.Button == MouseButtons.Right) && isDragDrop)
            {
                isDragDrop = false;
            }
            #endregion
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm.Refresh();
        }

        private void btAddPack_Click(object sender, EventArgs e)
        {
            FormNewPack fnp;
            if (string.IsNullOrEmpty(tbFrom.Text))
            {
                fnp = new FormNewPack(this);
            }
            else
            {
                string[] from = tbFrom.Text.Split(' ');
                fnp = new FormNewPack(Int32.Parse(from[0]), Int32.Parse(from[1]), Int32.Parse(from[2]), this);
            }
            fnp.Show();
        }
    }
        #endregion
}