using System.Windows.Forms;

namespace WinFormsProject
{
    partial class FormNewPack
    {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbID = new System.Windows.Forms.TextBox();
            this.lsType = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lsA = new System.Windows.Forms.NumericUpDown();
            this.lsX = new System.Windows.Forms.NumericUpDown();
            this.lsY = new System.Windows.Forms.NumericUpDown();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.lbNoitice = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.lsType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lsA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lsX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lsY)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Type:";
            // 
            // tbID
            // 
            this.tbID.Location = new System.Drawing.Point(40, 33);
            this.tbID.Name = "tbID";
            this.toolTip1.SetToolTip(this.tbID, "Input package ID");
            this.tbID.Size = new System.Drawing.Size(121, 20);
            this.tbID.TabIndex = 2;
            this.tbID.TextChanged += new System.EventHandler(this.tbID_TextChanged);
            // 
            // lsType
            // 
            this.lsType.Location = new System.Drawing.Point(207, 33);
            this.lsType.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.lsType.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lsType.Name = "lsType";
            this.lsType.Size = new System.Drawing.Size(52, 20);
            this.lsType.TabIndex = 3;
            this.lsType.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Area:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(102, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "X:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(187, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Y:";
            // 
            // lsA
            // 
            this.lsA.Location = new System.Drawing.Point(40, 61);
            this.lsA.Name = "lsA";
            this.lsA.Size = new System.Drawing.Size(52, 20);
            this.lsA.TabIndex = 7;
            this.lsA.ValueChanged += new System.EventHandler(this.lsA_ValueChanged);
            // 
            // lsX
            // 
            this.lsX.Location = new System.Drawing.Point(121, 61);
            this.lsX.Name = "lsX";
            this.lsX.Size = new System.Drawing.Size(52, 20);
            this.lsX.TabIndex = 8;
            // 
            // lsY
            // 
            this.lsY.Location = new System.Drawing.Point(207, 61);
            this.lsY.Name = "lsY";
            this.lsY.Size = new System.Drawing.Size(52, 20);
            this.lsY.TabIndex = 9;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(40, 90);
            this.textBox2.Name = "textBox2";
            this.toolTip1.SetToolTip(this.textBox2, "Input package price");
            this.textBox2.Size = new System.Drawing.Size(219, 20);
            this.textBox2.TabIndex = 10;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Price:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 123);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Day:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(41, 118);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(218, 20);
            this.dateTimePicker1.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(103, 164);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "OK";
            this.toolTip1.SetToolTip(this.button1, "Click done!");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(43, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(194, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Update new package information";
            // 
            // lbNoitice
            // 
            this.lbNoitice.AutoSize = true;
            this.lbNoitice.ForeColor = System.Drawing.Color.Red;
            this.lbNoitice.Location = new System.Drawing.Point(40, 145);
            this.lbNoitice.Name = "lbNoitice";
            this.lbNoitice.Size = new System.Drawing.Size(0, 13);
            this.lbNoitice.TabIndex = 16;
            // 
            // FormNewPack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 191);
            this.Controls.Add(this.lbNoitice);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.lsY);
            this.Controls.Add(this.lsX);
            this.Controls.Add(this.lsA);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lsType);
            this.Controls.Add(this.tbID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormNewPack";
            this.Text = "New package";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormNewPack_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.lsType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lsA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lsX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lsY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbID;
        private System.Windows.Forms.NumericUpDown lsType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown lsA;
        private System.Windows.Forms.NumericUpDown lsX;
        private System.Windows.Forms.NumericUpDown lsY;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbNoitice;
        private ToolTip toolTip1;
    }
}