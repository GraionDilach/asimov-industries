namespace A.I.NXT
{
    partial class UserInterface
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserInterface));
            this.picConGreen = new System.Windows.Forms.PictureBox();
            this.picConRed = new System.Windows.Forms.PictureBox();
            this.buttonConnection = new System.Windows.Forms.Button();
            this.labelMap = new System.Windows.Forms.Label();
            this.gyuszkoBox2 = new System.Windows.Forms.TextBox();
            this.gyuszkoBox1 = new System.Windows.Forms.TextBox();
            this.Abort_Button = new System.Windows.Forms.Button();
            this.Start_Button = new System.Windows.Forms.Button();
            this.panelMap = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picConGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picConRed)).BeginInit();
            this.SuspendLayout();
            // 
            // picConGreen
            // 
            this.picConGreen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picConGreen.BackgroundImage")));
            this.picConGreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picConGreen.Location = new System.Drawing.Point(348, 285);
            this.picConGreen.Name = "picConGreen";
            this.picConGreen.Size = new System.Drawing.Size(24, 24);
            this.picConGreen.TabIndex = 17;
            this.picConGreen.TabStop = false;
            this.picConGreen.Visible = false;
            // 
            // picConRed
            // 
            this.picConRed.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picConRed.BackgroundImage")));
            this.picConRed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picConRed.Location = new System.Drawing.Point(348, 285);
            this.picConRed.Name = "picConRed";
            this.picConRed.Size = new System.Drawing.Size(24, 24);
            this.picConRed.TabIndex = 16;
            this.picConRed.TabStop = false;
            // 
            // buttonConnection
            // 
            this.buttonConnection.Location = new System.Drawing.Point(254, 285);
            this.buttonConnection.Name = "buttonConnection";
            this.buttonConnection.Size = new System.Drawing.Size(75, 23);
            this.buttonConnection.TabIndex = 15;
            this.buttonConnection.Text = "connection";
            this.buttonConnection.UseVisualStyleBackColor = true;
            this.buttonConnection.Click += new System.EventHandler(this.buttonConnection_Click);
            // 
            // labelMap
            // 
            this.labelMap.AutoSize = true;
            this.labelMap.Location = new System.Drawing.Point(259, 32);
            this.labelMap.Name = "labelMap";
            this.labelMap.Size = new System.Drawing.Size(69, 13);
            this.labelMap.TabIndex = 14;
            this.labelMap.Text = "Balls location";
            // 
            // gyuszkoBox2
            // 
            this.gyuszkoBox2.Location = new System.Drawing.Point(393, 285);
            this.gyuszkoBox2.Name = "gyuszkoBox2";
            this.gyuszkoBox2.Size = new System.Drawing.Size(100, 20);
            this.gyuszkoBox2.TabIndex = 18;
            // 
            // gyuszkoBox1
            // 
            this.gyuszkoBox1.Location = new System.Drawing.Point(83, 263);
            this.gyuszkoBox1.Name = "gyuszkoBox1";
            this.gyuszkoBox1.Size = new System.Drawing.Size(100, 20);
            this.gyuszkoBox1.TabIndex = 19;
            // 
            // Abort_Button
            // 
            this.Abort_Button.Location = new System.Drawing.Point(254, 409);
            this.Abort_Button.Name = "Abort_Button";
            this.Abort_Button.Size = new System.Drawing.Size(74, 47);
            this.Abort_Button.TabIndex = 11;
            this.Abort_Button.Text = "Abort";
            this.Abort_Button.UseVisualStyleBackColor = true;
            this.Abort_Button.Click += new System.EventHandler(this.Abort_Button_Click);
            // 
            // Start_Button
            // 
            this.Start_Button.Enabled = false;
            this.Start_Button.Location = new System.Drawing.Point(254, 331);
            this.Start_Button.Name = "Start_Button";
            this.Start_Button.Size = new System.Drawing.Size(74, 47);
            this.Start_Button.TabIndex = 10;
            this.Start_Button.Text = "Start";
            this.Start_Button.UseVisualStyleBackColor = true;
            this.Start_Button.Click += new System.EventHandler(this.Start_Button_Click);
            // 
            // panelMap
            // 
            this.panelMap.BackColor = System.Drawing.SystemColors.Window;
            this.panelMap.Location = new System.Drawing.Point(173, 67);
            this.panelMap.Name = "panelMap";
            this.panelMap.Size = new System.Drawing.Size(245, 116);
            this.panelMap.TabIndex = 9;
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(591, 488);
            this.Controls.Add(this.picConGreen);
            this.Controls.Add(this.picConRed);
            this.Controls.Add(this.buttonConnection);
            this.Controls.Add(this.labelMap);
            this.Controls.Add(this.gyuszkoBox2);
            this.Controls.Add(this.gyuszkoBox1);
            this.Controls.Add(this.Abort_Button);
            this.Controls.Add(this.Start_Button);
            this.Controls.Add(this.panelMap);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UserInterface";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.UserInterface_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picConGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picConRed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picConGreen;
        private System.Windows.Forms.PictureBox picConRed;
        private System.Windows.Forms.Button buttonConnection;
        private System.Windows.Forms.Label labelMap;
        private System.Windows.Forms.TextBox gyuszkoBox2;
        private System.Windows.Forms.TextBox gyuszkoBox1;
        private System.Windows.Forms.Button Abort_Button;
        private System.Windows.Forms.Button Start_Button;
        private System.Windows.Forms.Panel panelMap;

    }
}

