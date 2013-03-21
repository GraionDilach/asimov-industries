namespace nxtBlueTooth
{
    partial class Form1
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.buttonGetInfo = new System.Windows.Forms.Button();
            this.buttonGetVersion = new System.Windows.Forms.Button();
            this.buttonReadMailbox = new System.Windows.Forms.Button();
            this.buttonWriteMailboxBool = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.numericUpDownMailBoxNbr = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownBool = new System.Windows.Forms.NumericUpDown();
            this.buttonWriteMailBoxInt = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownInt = new System.Windows.Forms.NumericUpDown();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMailBoxNbr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBool)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInt)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(67, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(49, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "COM7";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Com Port";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(14, 113);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(378, 42);
            this.textBox2.TabIndex = 3;
            // 
            // buttonGetInfo
            // 
            this.buttonGetInfo.Enabled = false;
            this.buttonGetInfo.Location = new System.Drawing.Point(299, 14);
            this.buttonGetInfo.Name = "buttonGetInfo";
            this.buttonGetInfo.Size = new System.Drawing.Size(91, 19);
            this.buttonGetInfo.TabIndex = 1;
            this.buttonGetInfo.Text = "Get NXT Info";
            this.buttonGetInfo.UseVisualStyleBackColor = true;
            this.buttonGetInfo.Click += new System.EventHandler(this.GetInfo_Click);
            // 
            // buttonGetVersion
            // 
            this.buttonGetVersion.Enabled = false;
            this.buttonGetVersion.Location = new System.Drawing.Point(195, 14);
            this.buttonGetVersion.Name = "buttonGetVersion";
            this.buttonGetVersion.Size = new System.Drawing.Size(98, 19);
            this.buttonGetVersion.TabIndex = 1;
            this.buttonGetVersion.Text = "Get NXT Version";
            this.buttonGetVersion.UseVisualStyleBackColor = true;
            this.buttonGetVersion.Click += new System.EventHandler(this.GetVersion_Click);
            // 
            // buttonReadMailbox
            // 
            this.buttonReadMailbox.Enabled = false;
            this.buttonReadMailbox.Location = new System.Drawing.Point(14, 65);
            this.buttonReadMailbox.Name = "buttonReadMailbox";
            this.buttonReadMailbox.Size = new System.Drawing.Size(102, 19);
            this.buttonReadMailbox.TabIndex = 4;
            this.buttonReadMailbox.Text = "Read MailBox";
            this.buttonReadMailbox.UseVisualStyleBackColor = true;
            this.buttonReadMailbox.Click += new System.EventHandler(this.ReadMailBox_Click);
            // 
            // buttonWriteMailboxBool
            // 
            this.buttonWriteMailboxBool.Enabled = false;
            this.buttonWriteMailboxBool.Location = new System.Drawing.Point(127, 65);
            this.buttonWriteMailboxBool.Name = "buttonWriteMailboxBool";
            this.buttonWriteMailboxBool.Size = new System.Drawing.Size(127, 19);
            this.buttonWriteMailboxBool.TabIndex = 5;
            this.buttonWriteMailboxBool.Text = "Write MailBox Bool";
            this.buttonWriteMailboxBool.UseVisualStyleBackColor = true;
            this.buttonWriteMailboxBool.Click += new System.EventHandler(this.WriteMailBoxBool_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(122, 14);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(67, 19);
            this.buttonConnect.TabIndex = 6;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // numericUpDownMailBoxNbr
            // 
            this.numericUpDownMailBoxNbr.Location = new System.Drawing.Point(67, 39);
            this.numericUpDownMailBoxNbr.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownMailBoxNbr.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMailBoxNbr.Name = "numericUpDownMailBoxNbr";
            this.numericUpDownMailBoxNbr.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownMailBoxNbr.TabIndex = 7;
            this.numericUpDownMailBoxNbr.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Mailbox #";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(124, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Bool To Send";
            // 
            // numericUpDownBool
            // 
            this.numericUpDownBool.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.numericUpDownBool.Location = new System.Drawing.Point(202, 39);
            this.numericUpDownBool.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownBool.Name = "numericUpDownBool";
            this.numericUpDownBool.Size = new System.Drawing.Size(52, 20);
            this.numericUpDownBool.TabIndex = 9;
            // 
            // buttonWriteMailBoxInt
            // 
            this.buttonWriteMailBoxInt.Enabled = false;
            this.buttonWriteMailBoxInt.Location = new System.Drawing.Point(263, 65);
            this.buttonWriteMailBoxInt.Name = "buttonWriteMailBoxInt";
            this.buttonWriteMailBoxInt.Size = new System.Drawing.Size(127, 19);
            this.buttonWriteMailBoxInt.TabIndex = 5;
            this.buttonWriteMailBoxInt.Text = "Write MailBox Int";
            this.buttonWriteMailBoxInt.UseVisualStyleBackColor = true;
            this.buttonWriteMailBoxInt.Click += new System.EventHandler(this.WriteMailBoxInt_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(260, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Int To Send";
            // 
            // numericUpDownInt
            // 
            this.numericUpDownInt.Location = new System.Drawing.Point(329, 39);
            this.numericUpDownInt.Maximum = new decimal(new int[] {
            66536,
            0,
            0,
            0});
            this.numericUpDownInt.Minimum = new decimal(new int[] {
            65536,
            0,
            0,
            -2147483648});
            this.numericUpDownInt.Name = "numericUpDownInt";
            this.numericUpDownInt.Size = new System.Drawing.Size(61, 20);
            this.numericUpDownInt.TabIndex = 9;
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Location = new System.Drawing.Point(14, 161);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox3.Size = new System.Drawing.Size(378, 42);
            this.textBox3.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(127, 88);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 19);
            this.button1.TabIndex = 5;
            this.button1.Text = "Write MailBox Stuff";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.WriteMailBoxOurMessage_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 275);
            this.Controls.Add(this.numericUpDownInt);
            this.Controls.Add(this.numericUpDownBool);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownMailBoxNbr);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.buttonWriteMailBoxInt);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonWriteMailboxBool);
            this.Controls.Add(this.buttonReadMailbox);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonGetInfo);
            this.Controls.Add(this.buttonGetVersion);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "NXT Bluetooth Tester v0.2";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMailBoxNbr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBool)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button buttonGetInfo;
        private System.Windows.Forms.Button buttonGetVersion;
        private System.Windows.Forms.Button buttonReadMailbox;
        private System.Windows.Forms.Button buttonWriteMailboxBool;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.NumericUpDown numericUpDownMailBoxNbr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownBool;
        private System.Windows.Forms.Button buttonWriteMailBoxInt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownInt;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button1;
    }
}

