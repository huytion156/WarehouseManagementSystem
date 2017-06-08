//version 11:56am - 6/6/2017
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
    public partial class Form1 : KryptonForm
    {
        
        // 
        //#region Properties
        #region InitServerClient
        public Server server;
        public Client client;
        public delegate void UpdateTextCallback(string text);
        private delegate void ReconnectCallback();
        #endregion
        // Define:
        private const int penWidth = 2; //pen Width
        Color penCol = Color.Blue;      //pen Color

        // Graphics Define:
        public const int dis = 0; // translation from (0,0): (x,y) = (dis+x,dis+y) 
        public const int h = 10;   // heingt of rectangle
        public const int w = 15;   // width of rectangle
        public Color COL_EMPTY = Color.Lavender;
        public Color COL_TYPE1 = Color.Red;      //type1 Color
        public Color COL_TYPE2 = Color.Green;    //type2 Color

        #region ATTRIBUTES
        // Attributes: 
        public Area[] arr; //aray of Areas
        public List<Package> packages = new List<Package>();
        public int noA; // num of Areas
        public int noP; // num of Packages
        private float zoom=1f;
        public float Xpos = 0f;
        public float Ypos = 0f;
        #endregion
        public Form1()
        {
            InitializeComponent();
            this.trackBar1.Value = 100;
            // 
            #region InitServerClient
            IPHostEntry IPHost = Dns.GetHostEntry(Dns.GetHostName());
            string[] IPs;
            foreach(var k in IPHost.AddressList)
            {
                IPs = k.ToString().Split('.');
                //MessageBox.Show(k.ToString());
                if (IPs.Length != 4) { continue; MessageBox.Show("ok"); }
                textBox1.AppendText("My IP address is: " + k + "\r\n");
                textBox3.Text = IPs[0];
                textBox4.Text = IPs[1];
                textBox5.Text = IPs[2];
                textBox6.Text = IPs[3];
                textBox7.Text = "101";
                break;
            }

            #endregion

            btRemovePack.Enabled = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Read Data From Text File
            ReadDataFromTextFile("Data.txt");
        }
        #region READ_FROM_FILES
        private void ReadDataFromTextFile(string FileName)
        {
            // Read ware house data from file:
            int x, y, n, m, num, type;
            float rad, price;
            string s, id, day;
            string[] sp;
            noP = 0;
            try
            {
                using (StreamReader sr = new StreamReader(FileName))
                {
                    String line = sr.ReadLine();
                    noA = Int32.Parse(line);
                    arr = new Area[noA];
                    for (int i = 0; i < noA; i++)
                    {
                        s = sr.ReadLine();
                        sp = s.Split(' ');
                        x = int.Parse(sp[0]);
                        y = int.Parse(sp[1]);
                        rad = float.Parse(sp[2]);
                        n = int.Parse(sp[3]);
                        m = int.Parse(sp[4]);
                        num = int.Parse(sp[5]);

                        arr[i] = new Area(x, y, Convert.ToInt32((rad / Math.PI * 180)), n, m, i);
                        for (int t = 0; t < num; ++t)
                        {
                            noP++;
                            s = sr.ReadLine();
                            sp = s.Split(' ');
                            x = int.Parse(sp[0]);
                            y = int.Parse(sp[1]);
                            type = int.Parse(sp[2]);
                            id = sp[3];
                            price = float.Parse(sp[4]);
                            day = sp[5];
                            if (type ==1 )
                            {
                                // Add vao danh sach kien hang truoc, sau do cap nhat kien hang len so do khu vuc
                                packages.Add(new Package1(x, y, i, id, day, price, noP));
                                arr[i].AddPack(x, y, noP, 1);
                            }
                                
                            else
                            {
                                // Add vao danh sach kien hang truoc, sau do cap nhat kien hang len so do khu vuc
                                packages.Add(new Package2(x, y, i, id, day, price, noP));
                                arr[i].AddPack(x, y, noP, 2);
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "The file could not be read:");
            }
        }
        #endregion

        #region DRAWING_AREAS
        private void panelWareHouse_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.ScaleTransform(zoom, zoom);
            drawAllAreas(e.Graphics);
        }


        private void drawAllAreas(Graphics graphics)
        {
            for (int i = 0; i < noA; i++)
                drawArea(arr[i],graphics);
        }
        public void drawArea(Area area, System.Drawing.Graphics graphics)
        {
            Matrix myMatrix = new Matrix();
            myMatrix.Rotate(area.mDeg, MatrixOrder.Append);
            myMatrix.Scale(zoom, zoom,MatrixOrder.Append);
            myMatrix.Translate(Xpos, Ypos,MatrixOrder.Append);
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
            if ((id > 0) && (id <= noP))
            {
                lbType.Text = packages[id - 1].getType().ToString();
                lbID.Text = packages[id - 1].mId.ToString();
                lbX.Text = packages[id - 1].mx.ToString();
                lbY.Text = packages[id - 1].my.ToString();
                lbArea.Text = packages[id - 1].mArea.ToString();
                lbDay.Text = packages[id - 1].mDay;
                lbPrice.Text = packages[id - 1].mPrice.ToString();
            }
        }
        public void Send_create(string a, string x, string y, string type, string price, string id, string day, object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox2.Text = "A " + a + " " + x + " " + y + " " + type + " " + price + " " + id + " " + day;
            button1_Click((object) sender, (EventArgs) e);
        }
        public void Send_delete(string s1, string s2, string s3, object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox2.Text = "D " + s1 + " " + s2 + " " + s3;
            button1_Click(sender, (EventArgs)e);
        }
        public void Send_move(string s1, string s2, object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox2.Text = "M " + s1 + " " + s2;
            button1_Click(sender, (EventArgs)e);
        }
        
    public void createNewPack(int a, int x, int y, int type, float price, string id, string day)
        {
            noP++;
            if (type == 1)
            {
                // Add vao danh sach kien hang truoc, sau do cap nhat kien hang len so do khu vuc
                packages.Add(new Package1(x, y, a,id, day, price, noP));
                arr[a].AddPack(x, y, noP, 1);
            }
            else
            {
                // Add vao danh sach kien hang truoc, sau do cap nhat kien hang len so do khu vuc
                packages.Add(new Package2(x, y, a,
                                             id, day, price, noP));
                arr[a].AddPack(x, y, noP, 2);
            }
            panelWareHouse.Refresh();
        }
        public int movePack(int x1, int y1, int a1, int x2, int y2, int a2)
        {
            if ((a1 == a2) && (x1 == x2) && (y1 == y2)) return 1;
            int packKey = arr[a1].getIDatXY(x1, y1); //lay ID cua kien hang tai vi tri x1,y1,a1
            if ((packKey == 0) || (packKey == -1)) //khong co kien hang hoac vi tri khong ton tai
                return 0;
            else
            {
                int packType = packages[packKey - 1].getType(); // lay loai kien hang
                if (packType == 1)
                { // kien hang loai 1
                    if (arr[a2].getIDatXY(x2, y2) != 0) return 0; // neu vi tri dich khong ton tai hoac da co kien hang khac
                }
                else //kien hang loai 2
                {
                    if (!(((a1 == a2) && (x1 == x2)) &&
                          ((y1 + 1 == y2) && (arr[a2].isEmpty(x2, y2 + 1)) || (y2 + 1 == y1) && (arr[a2].isEmpty(x2, y2)))))
                    {
                        if (arr[a2].isEmpty(x2, y2, 2) == 0) // neu x2,y2 la vi tri dau kien hang va khong con trong
                            if (arr[a2].isEmpty(x2, y2 - 1, 2) == 0) // ney x2,y2 la vi tri duoi kien hang va khong con trong
                            {
                                return 0;
                            }
                            else y2--; // chuyen vi tri dich len dau kien hang
                    }
                }
                arr[a1].RemovePack(packages[packKey - 1].mx, packages[packKey - 1].my, packType); //xoa vi tri cu
                arr[a2].AddPack(x2, y2, packKey, packType); //chuyen qua vi tri moi
                packages[packKey - 1].UpdatePos(x2, y2, a2);  // cap nhat lai vi tri moi cho pack
                return 1;
            }
        }

        
        public void deletePackage(int a, int x, int y)
        {
            int id=arr[a].getIDatXY(x,y);
            if (id >= 1)
            {
                arr[a].RemovePack(x, y, packages[id - 1].getType());
                //packages.Remove(packages[id - 1]);
                panelWareHouse.Refresh();

            }
        }
        #endregion
        
        #region ZOOM_SCROLL_PANEL
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            zoom = trackBar1.Value / 100f;
            lbZoom.Text = trackBar1.Value.ToString() +"%";
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

        #region ProcessServerClient
        private void groupBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)//Connect
        {
            string ipAddr = textBox3.Text + "." + textBox4.Text + "." + textBox5.Text + "." + textBox6.Text;
            string port = textBox7.Text;
            if (IsValidIPAddress(ipAddr) == true)
            {
                if (radioButton1.Checked == true)//Server Mode
                {
                    try
                    {
                        if (server == null)
                            server = new Server(this);
                        textBox1.AppendText("\r\nServer is Online !... \r\n");
                        //textBox1.AppendText("\r\nServer said: (@" + DateTime.Now.ToString() + ")\r\n" + data + "\r\n");
                        server.Connect(ipAddr, port);
                        button2.Enabled = false;
                        button3.Enabled = true;
                        textBox2.Focus();

                    }
                    catch (SocketException se)
                    {
                        MessageBox.Show("Server Connect Error.\r\n" + se.ToString());
                    }
                }
                else if (radioButton2.Checked == true)//Client Mode
                {
                    try
                    {
                        if (client == null)
                            client = new Client(this);
                        client.Connect(ipAddr, port);
                        string data = "Client is online";
                        client.Send(Encoding.GetEncoding("GB18030").GetBytes(data));
                        textBox1.AppendText("\r\nClient : " + data + "\r\n");
                        button2.Enabled = false;
                        button3.Enabled = true;
                        textBox2.Focus();
                    }
                    catch (SocketException se)
                    {
                        MessageBox.Show("Client Connect Error.\r\n" + se.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid IP address input.");
            }
        }
    

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)//Send the message
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, (EventArgs)e);//Send
            }
        }
        private void textBox2_KeyUp(object sender, KeyEventArgs e)//Clear the text in textBox2
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox2.Lines.Length > 0)
                    textBox2.Lines = new string[] { };
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)//Server Mode
            {
                server.Disconnect();
                if (button2.Enabled == false)
                {
                    button2.Enabled = true;
                    button3.Enabled = false;
                }
            }
            else if (radioButton2.Checked == true)//Client Mode
            {
                client.Disconnect();
                if (button2.Enabled == false)
                {
                    button2.Enabled = true;
                    button3.Enabled = false;
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        private void check_request_move(string t)
        {
            string[] tmp = t.Split(' ');
            int ok;
            
            // Move
            if ((tmp.Length == 7)&&(tmp[0] == "M"))
            { 
                ok = movePack(Int32.Parse(tmp[2]), Int32.Parse(tmp[3]), Int32.Parse(tmp[1]), Int32.Parse(tmp[5]), Int32.Parse(tmp[6]), Int32.Parse(tmp[4]));
                panelWareHouse.Refresh();
            }
            // Delete
            if(tmp.Length == 4&&tmp[0] == "D")
            {
                deletePackage(int.Parse(tmp[1]), int.Parse(tmp[2]), int.Parse(tmp[3]));
                panelWareHouse.Refresh();
            }
            if(tmp.Length == 8&& tmp[0] == "A" )
            {
                int a, x, y, type;
                float price;
                string id, day;
                a = Int32.Parse(tmp[1]);
                x = Int32.Parse(tmp[2]);
                y = Int32.Parse(tmp[3]);
                type = Int32.Parse(tmp[4]);
                price = float.Parse(tmp[5]);
                id = tmp[6];
                day = tmp[7];
                createNewPack(a, x, y, type, price, id, day);
                panelWareHouse.Refresh();
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                if (radioButton1.Checked == true)//Server Mode
                {
                    try
                    {
                        if (server != null)
                        {
                            
                            //MessageBox.Show(Convert.ToString(textBox1.Text.Split(' ').Length));
                            byte[] bytes = Encoding.GetEncoding("GB18030").GetBytes(textBox2.Text);
                            
                            server.Send(bytes);
                            string str = "";
                            str = "\r\nServer : " + textBox2.Text;
                            check_request_move(textBox2.Text);
                            // Move
                            textBox1.AppendText(str + "\r\n");
                            textBox2.Clear();
                        }
                    }
                    catch (SocketException se)
                    {
                        MessageBox.Show("Server send error!\r\n" + se.Message);
                    }

                }
                else if (radioButton2.Checked == true)//Client Mode
                {
                    try
                    {
                        if (client != null)
                        {
                            byte[] bytes = Encoding.GetEncoding("GB18030").GetBytes(textBox2.Text);
                            client.Send(bytes);
                            string str = "";
                            str = "\r\nClient : " + textBox2.Text;

                            // Move
                            check_request_move(textBox2.Text);
                            textBox1.AppendText(str + "\r\n");
                            textBox2.Clear();
                        }
                    }
                    catch (SocketException se)
                    {
                        MessageBox.Show("Client send error!\r\n" + se.Message);
                    }
                }
            }

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        
        
        public void UpdateText(string text)//Update the text on textBox1
        {
            if (this.textBox1.InvokeRequired)
            {
                UpdateTextCallback temp = new UpdateTextCallback(UpdateText);
                this.Invoke(temp, new object[] { text });
            }
            else
            {
                string str = "";
                
                    if (radioButton1.Checked == true)
                    {
                        str = "\r\nClient : " + text;
                        check_request_move(text);
                    }
                    else if (radioButton2.Checked == true)
                    {
                        str = "\r\nServer : " + text;
                        check_request_move(text);
                    }
                
                textBox1.AppendText(str);
            }
        }

        
        private bool IsValidIPAddress(string ipaddr)//Validate the input IP address
        {
            try
            {
                IPAddress.Parse(ipaddr);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "IsValidIPAddress Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private void Reconnect()//Reconnect the Ethernet
        {
            try
            {
                if (button4.InvokeRequired)
                {
                    ReconnectCallback r = new ReconnectCallback(Reconnect);
                    this.Invoke(r, new object[] { });
                }
                else
                {
                    button4_Click(null, null);//Reconnect
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Reconnect failed.  Please restart.\r\n" + e.Message, "Reconnect Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button3.Enabled == true)
                button3_Click(sender, e);//Disconnect
            Thread.Sleep(200);
            button2_Click(sender, e);//Connect
        }

        private void textBox2_KeyDown_1(object sender, KeyEventArgs e)
        {

        }

        private void tbFrom_TextChanged(object sender, EventArgs e)
        {

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
            check = movePack(Int32.Parse(from[1]), Int32.Parse(from[2]), Int32.Parse(from[0]), Int32.Parse(to[1]), Int32.Parse(to[2]), Int32.Parse(to[0]));
            if (check == 0) MessageBox.Show("Cannot Move !");
            else
            {
                Send_move(tbFrom.Text, tbTo.Text, sender, (EventArgs)e);
            }
            panelWareHouse.Refresh();
        }

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
        }
        //public void sendDeleteC
        private void btRemovePack_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete package \" " + lbID.Text + " \" ?",
                                     "Delete Package:",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                deletePackage(Int32.Parse(lbArea.Text), Int32.Parse(lbX.Text), Int32.Parse(lbY.Text));
                Send_delete(lbArea.Text, lbX.Text, lbY.Text, sender, e);
            }
            else
            {
                // If 'No', do something here.
            }

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
            for (int k = 0; k < noA; k++)
            {
                for (int i = 0; i < arr[k].mN; i++)
                    for (int j = 0; j < arr[k].mM; j++)
                    {
                        p = RotatePoint(-arr[k].mDeg, TransformMouseClick(e));
                        if (arr[k].inRec(i, j, p) == 1)
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
            if (res !=null)
                    {
                        int k = res[0];
                        int i = res[1];
                        int j = res[2];
                        int id = arr[k].getIDatXY(i, j);
                        tbFrom.Text = k.ToString() + " " + i.ToString() + " " + j.ToString();
                        printPackageInfo(arr[k].getIDatXY(i, j));
                        box_type = id; //lay ID cua kien hang tai vi tri x1,y1,a1
                        if (id>=1) btRemovePack.Enabled = true; // neu co kien hang, enable nut delete
                    }
                is_Move = true; //Bật hiệu ứng dời box
                if (box_type > 0) box_type = packages[box_type - 1].getType();//Kiểm tra dời box xanh hay box đỏ
                else box_type = -1;
            }
            else
            {
                //Xử lí quét chọn bằng chuột phải
                rec_preview.Location = new Point(e.X, e.Y);
                is_Move = true;
                if (res != null) tbTo.Text = res[0].ToString() +" " + res[1].ToString() +" "+ res[2].ToString();
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
                int width = Math.Abs( (e.X - rec_preview.X) ) * trackBar1.Value / 100; //Lay chieu rong box sau khi calib
                int height = Math.Abs( (e.Y - rec_preview.Y) ) * trackBar1.Value / 100; //Lay chieu dai box sau khi calib
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
            if ((e.Button == MouseButtons.Left)&&isDragDrop)
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
                check = movePack(Int32.Parse(from[1]), Int32.Parse(from[2]), Int32.Parse(from[0]), Int32.Parse(to[1]), Int32.Parse(to[2]), Int32.Parse(to[0]));
                if (check == 0) MessageBox.Show("Cannot Move !");
                else
                {
                    Send_move(tbFrom.Text, tbTo.Text, sender, (EventArgs)e);
                }
                panelWareHouse.Refresh();
            }
            #endregion
            
            #region Xu li chuot phai
            if ((e.Button == MouseButtons.Right)&&isDragDrop)
            {
                isDragDrop = false;
                //MessageBox.Show(getAreaChoose().ToString());
                Form2 fr = new Form2(this, getAreaChoose());
                fr.ShowDialog();
            }
            #endregion
        }

        //Lấy số thứ tự area được chọn
        //Hàm này đang bị lỗi
        private int getAreaChoose()
        {
            int type = -1;
            double distan = 999999;
            for (int i = 0; i < noA; i++)
            {
                double tmp = getDistance(rec_preview.X, rec_preview.Y, arr[i].mX, arr[i].mY);
                if (tmp < distan)
                {
                    distan = tmp;
                    type = i;
                }
            }
            return type;
        }


    }
    #endregion

    #region DATA_STRUCTURE
    public class Color1Rec
    {
        public int mA;
        public int I;
        public int J;
        public Color PenCol;
        public Color BrushCol;
        public Color1Rec(int A, int i, int j, Color penCol, Color brushCol)
        {
            mA = A;
            I = i;
            J = j;
            PenCol = penCol;
            BrushCol = brushCol;
        }
    }
    public class Area : Form1
    {
        public int mX;
        public int mY;
        public int mN;
        public int mM;
        public int mDeg;
        public int mID;
        public int[, ,] a;
        public Color[,] c;
        public Area()
        {
            mX = 0;
            mY = 0;
            mM = 0;
            mN = 0;
            mDeg = 0;
            mID = 0;
        }
        public void setID(int i, int j, int id)
        {
            a[i, j, 4] = id;
        }
        public int getIDatXY(int i, int j)
        {
            if ((i >= mN) || (i < 0) || (j >= mM) || (j < 0)) return -1;
            else return a[i, j, 4];
        }
        public void AddPack(int x, int y, int id, int type)
        {
            if (type == 1)
            {
                setID(x,y,id);
                setRecCol(x, y, COL_TYPE1);
            }
            else
            {
                setID(x, y, id);
                setID(x, y + 1, id);
                setRecCol(x, y, COL_TYPE2);
                setRecCol(x, y + 1, COL_TYPE2);
            }
        }
        public void RemovePack(int x, int y, int type)
        {
            if (type == 1)
            {
                setID(x, y, 0);
                setRecCol(x, y, COL_EMPTY);
            }
            else
            {
                setID(x, y, 0);
                setRecCol(x, y, COL_EMPTY);
                setID(x, y+1, 0);
                setRecCol(x, y+1, COL_EMPTY);
            }
        }
        public Area(int X, int Y, int Deg, int N, int M, int ID)
        {
            mX = X;
            mY = Y;
            mM = M;
            mN = N;
            mDeg = Deg;
            mID = ID;
            a = new int[mN, mM, 5];
            c = new Color[mN, mM];

            Matrix myMatrix = new Matrix();
            myMatrix.Rotate(mDeg, MatrixOrder.Append);
            for (int i = 0; i < mN; i++)
                for (int j = 0; j < mM; j++)
                {
                    setRecCol(i, j, COL_EMPTY); 
                    setID(i, j, 0);
                    System.Drawing.Graphics graphics = this.CreateGraphics();
                    graphics.Transform = myMatrix;
                    System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(X + dis + i * h, Y + dis + j * w, h, w);
                    if (a[i, j, 0] != X + dis + i * h)
                        setRec(i, j, X + dis + i * h, X + dis + i * h + h, Y + dis + j * w, Y + dis + j * w + w);
                }
        }
        private void setRec(int i, int j, int x1, int x2, int y1, int y2)
        {
            a[i, j, 0] = x1;
            a[i, j, 1] = x2;
            a[i, j, 2] = y1;
            a[i, j, 3] = y2;
        }
        public int inRec(int i, int j, Point p)
        {
            if ((p.X > a[i, j, 0]) && (p.X < a[i, j, 1]) && (p.Y > a[i, j, 2]) && (p.Y < a[i, j, 3])) return 1;
            else return 0;
        }
        public bool isEmpty(int i, int j)
        {
            return (getIDatXY(i, j) == 0);
        }
        private void setRecCol(int i, int j, Color col){
            c[i,j] = col;
        }
        public int isEmpty(int x, int y, int type)
        {
            if (type == 1)
            {
                if (getIDatXY(x, y) == 0) return 1; else return 0;
            }
            else
            {
                if ((getIDatXY(x, y) == 0) && (getIDatXY(x, y + 1) == 0)) return 1; else return 0;
            }
        }
        public System.Drawing.Rectangle getDrawingRectangle(int i, int j)
        {
            int x = a[i, j, 0];
            int y = a[i, j, 2];
            int h = a[i, j, 1] - x;
            int w = a[i, j, 3] - y;
            return new System.Drawing.Rectangle(x, y, h, w);

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Area
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Area";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
    public class Package
    {
        public int mx;
        public int my;
        public int mArea;
        public int mKey;
        public string mId;
        public string mDay;
        public float mPrice;
       
        public Package() { }
        public void UpdatePos(int x, int y, int area)
        {
            mx = x;
            my = y;
            mArea = area;
        }
        public virtual int getType()
        {
            return 0;
        }
    }
    public class Package1 : Package
    {
        public Package1(int x, int y, int Area, string Id, string Day, float Price, int Key)
        {
            mx = x;
            my = y;
            mArea = Area;
            mId = Id;
            mDay = Day;
            mPrice = Price;
            mKey = Key;

        }
        public override int getType()
        {
            return 1;
        }
    }

    public class Package2 : Package
    {
        public Package2(int x, int y, int Area, string Id, string Day, float Price, int Key)
        {
            mx = x;
            my = y;
            mArea = Area;
            mId = Id;
            mDay = Day;
            mPrice = Price;
            mKey = Key;
                 }
        public override int getType()
        {
            return 2;
        }
    }
    public class KeyValuePair
    {
        public Socket socket;
        public byte[] dataBuffer = new byte[1];
    }
}
    #endregion