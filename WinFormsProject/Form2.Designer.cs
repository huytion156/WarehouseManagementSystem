using System.Windows.Forms;
namespace WinFormsProject
{
    partial class Form2
    {
        public class MyDisplay : Panel
        {
            public MyDisplay()
            {
                this.DoubleBuffered = true;
            }
        }
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btMove = new System.Windows.Forms.Button();
            this.tbFrom = new System.Windows.Forms.TextBox();
            this.tbTo = new System.Windows.Forms.TextBox();
            this.btRemovePack = new System.Windows.Forms.Button();
            this.btAddPack = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelWareHouse = new WinFormsProject.Form2.MyDisplay();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.lbZoom = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbType = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbArea = new System.Windows.Forms.Label();
            this.lbY = new System.Windows.Forms.Label();
            this.lbDay = new System.Windows.Forms.Label();
            this.lbPrice = new System.Windows.Forms.Label();
            this.lbX = new System.Windows.Forms.Label();
            this.lbID = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbAreaID = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btMove
            // 
            this.btMove.Location = new System.Drawing.Point(69, 71);
            this.btMove.Name = "btMove";
            this.btMove.Size = new System.Drawing.Size(75, 23);
            this.btMove.TabIndex = 1;
            this.btMove.Text = "MOVE";
            this.toolTip1.SetToolTip(this.btMove, "Click to move!");
            this.btMove.UseVisualStyleBackColor = true;
            this.btMove.Click += new System.EventHandler(this.btMove_Click);
            // 
            // tbFrom
            // 
            this.tbFrom.Location = new System.Drawing.Point(50, 19);
            this.tbFrom.Name = "tbFrom";
            this.tbFrom.Size = new System.Drawing.Size(113, 20);
            this.tbFrom.TabIndex = 3;
            this.toolTip1.SetToolTip(this.tbFrom, "Input place you want move from (area, x, y)");
            // 
            // tbTo
            // 
            this.tbTo.Location = new System.Drawing.Point(49, 45);
            this.tbTo.Name = "tbTo";
            this.tbTo.Size = new System.Drawing.Size(114, 20);
            this.tbTo.TabIndex = 4;
            this.toolTip1.SetToolTip(this.tbTo, "Input place you want move to (area, x, y)");
            // 
            // btRemovePack
            // 
            this.btRemovePack.Location = new System.Drawing.Point(49, 94);
            this.btRemovePack.Name = "btRemovePack";
            this.btRemovePack.Size = new System.Drawing.Size(114, 23);
            this.btRemovePack.TabIndex = 17;
            this.btRemovePack.Text = "Delete Package";
            this.toolTip1.SetToolTip(this.btRemovePack, "Click to delete this package!");
            this.btRemovePack.UseVisualStyleBackColor = true;
            this.btRemovePack.Click += new System.EventHandler(this.btRemovePack_Click);
            // 
            // btAddPack
            // 
            this.btAddPack.Location = new System.Drawing.Point(49, 121);
            this.btAddPack.Name = "btAddPack";
            this.btAddPack.Size = new System.Drawing.Size(114, 23);
            this.btAddPack.TabIndex = 16;
            this.btAddPack.Text = "Add new Package";
            this.toolTip1.SetToolTip(this.btAddPack, "Click to add package!");
            this.btAddPack.UseVisualStyleBackColor = true;
            this.btAddPack.Click += new System.EventHandler(this.btAddPack_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "To:";
            // 
            // panelWareHouse
            // 
            this.panelWareHouse.AutoScroll = true;
            this.panelWareHouse.AutoScrollMinSize = new System.Drawing.Size(2000, 2000);
            this.panelWareHouse.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelWareHouse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelWareHouse.Location = new System.Drawing.Point(13, 65);
            this.panelWareHouse.Name = "panelWareHouse";
            this.panelWareHouse.Size = new System.Drawing.Size(972, 481);
            this.panelWareHouse.TabIndex = 5;
            this.panelWareHouse.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panelWareHouse_Scroll);
            this.panelWareHouse.Paint += new System.Windows.Forms.PaintEventHandler(this.panelWareHouse_Paint);
            this.panelWareHouse.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelWareHouse_MouseDown);
            this.panelWareHouse.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelWareHouse_MouseMove);
            this.panelWareHouse.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelWareHouse_MouseUp);
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 10;
            this.trackBar1.Location = new System.Drawing.Point(427, 552);
            this.trackBar1.Maximum = 300;
            this.trackBar1.Minimum = 50;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(300, 45);
            this.trackBar1.SmallChange = 10;
            this.trackBar1.TabIndex = 6;
            this.trackBar1.TickFrequency = 10;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar1.Value = 50;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // lbZoom
            // 
            this.lbZoom.AutoSize = true;
            this.lbZoom.Location = new System.Drawing.Point(733, 567);
            this.lbZoom.Name = "lbZoom";
            this.lbZoom.Size = new System.Drawing.Size(33, 13);
            this.lbZoom.TabIndex = 7;
            this.lbZoom.Text = "100%";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btMove);
            this.groupBox1.Controls.Add(this.tbTo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbFrom);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(1003, 293);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 101);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Move Package";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(298, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(480, 32);
            this.label3.TabIndex = 9;
            this.label3.Text = "Warehouse Management System 1.0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btRemovePack);
            this.groupBox2.Controls.Add(this.btAddPack);
            this.groupBox2.Controls.Add(this.lbType);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.lbArea);
            this.groupBox2.Controls.Add(this.lbY);
            this.groupBox2.Controls.Add(this.lbDay);
            this.groupBox2.Controls.Add(this.lbPrice);
            this.groupBox2.Controls.Add(this.lbX);
            this.groupBox2.Controls.Add(this.lbID);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(1003, 129);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 158);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Package Information";
            // 
            // lbType
            // 
            this.lbType.AutoSize = true;
            this.lbType.Location = new System.Drawing.Point(153, 20);
            this.lbType.Name = "lbType";
            this.lbType.Size = new System.Drawing.Size(0, 13);
            this.lbType.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(119, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Type:";
            // 
            // lbArea
            // 
            this.lbArea.AutoSize = true;
            this.lbArea.Location = new System.Drawing.Point(154, 39);
            this.lbArea.Name = "lbArea";
            this.lbArea.Size = new System.Drawing.Size(0, 13);
            this.lbArea.TabIndex = 19;
            // 
            // lbY
            // 
            this.lbY.AutoSize = true;
            this.lbY.Location = new System.Drawing.Point(80, 39);
            this.lbY.Name = "lbY";
            this.lbY.Size = new System.Drawing.Size(0, 13);
            this.lbY.TabIndex = 18;
            // 
            // lbDay
            // 
            this.lbDay.AutoSize = true;
            this.lbDay.Location = new System.Drawing.Point(46, 75);
            this.lbDay.Name = "lbDay";
            this.lbDay.Size = new System.Drawing.Size(0, 13);
            this.lbDay.TabIndex = 17;
            // 
            // lbPrice
            // 
            this.lbPrice.AutoSize = true;
            this.lbPrice.Location = new System.Drawing.Point(46, 56);
            this.lbPrice.Name = "lbPrice";
            this.lbPrice.Size = new System.Drawing.Size(0, 13);
            this.lbPrice.TabIndex = 16;
            // 
            // lbX
            // 
            this.lbX.AutoSize = true;
            this.lbX.Location = new System.Drawing.Point(21, 39);
            this.lbX.Name = "lbX";
            this.lbX.Size = new System.Drawing.Size(0, 13);
            this.lbX.TabIndex = 15;
            // 
            // lbID
            // 
            this.lbID.AutoSize = true;
            this.lbID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbID.Location = new System.Drawing.Point(35, 20);
            this.lbID.Name = "lbID";
            this.lbID.Size = new System.Drawing.Size(0, 13);
            this.lbID.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Day:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Price:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(120, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Area:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(66, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Y:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "X:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "ID:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(384, 567);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "Zoom:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::WinFormsProject.Properties.Resources.logo1;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(203, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(73, 60);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // lbAreaID
            // 
            this.lbAreaID.AutoSize = true;
            this.lbAreaID.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAreaID.ForeColor = System.Drawing.Color.Blue;
            this.lbAreaID.Location = new System.Drawing.Point(1032, 80);
            this.lbAreaID.Name = "lbAreaID";
            this.lbAreaID.Size = new System.Drawing.Size(110, 25);
            this.lbAreaID.TabIndex = 13;
            this.lbAreaID.Text = "Area ID = ";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 598);
            this.Controls.Add(this.lbAreaID);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbZoom);
            this.Controls.Add(this.panelWareHouse);
            this.Controls.Add(this.trackBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.Text = "Warehouse management system";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btMove;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbFrom;
        private System.Windows.Forms.TextBox tbTo;
        private TrackBar trackBar1;
        private Form2.MyDisplay panelWareHouse;
        private Label lbZoom;
        private GroupBox groupBox1;
        private Label label3;
        private GroupBox groupBox2;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label lbArea;
        private Label lbY;
        private Label lbDay;
        private Label lbPrice;
        private Label lbX;
        private Label lbID;
        private Label lbType;
        private Label label10;
        private Label label11;
        private Button btAddPack;
        private Button btRemovePack;
        private PictureBox pictureBox1;
        private Label lbAreaID;
        private ToolTip toolTip1;
    }
}

