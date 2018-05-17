namespace FaceRecognization
{
    partial class Main
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
            this.TextBoxID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ButtonRegister = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.VideoPlayer = new AForge.Controls.VideoSourcePlayer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTableNum = new System.Windows.Forms.TextBox();
            this.lbCode = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lbName = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBoxID
            // 
            this.TextBoxID.Location = new System.Drawing.Point(112, 479);
            this.TextBoxID.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxID.Multiline = true;
            this.TextBoxID.Name = "TextBoxID";
            this.TextBoxID.Size = new System.Drawing.Size(384, 38);
            this.TextBoxID.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 482);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "ID";
            // 
            // ButtonRegister
            // 
            this.ButtonRegister.Location = new System.Drawing.Point(34, 644);
            this.ButtonRegister.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonRegister.Name = "ButtonRegister";
            this.ButtonRegister.Size = new System.Drawing.Size(480, 34);
            this.ButtonRegister.TabIndex = 6;
            this.ButtonRegister.Text = "注册";
            this.ButtonRegister.UseVisualStyleBackColor = true;
            this.ButtonRegister.Click += new System.EventHandler(this.ButtonRegister_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(56, 30);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(399, 429);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // VideoPlayer
            // 
            this.VideoPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VideoPlayer.Location = new System.Drawing.Point(18, 28);
            this.VideoPlayer.Margin = new System.Windows.Forms.Padding(4);
            this.VideoPlayer.Name = "VideoPlayer";
            this.VideoPlayer.Size = new System.Drawing.Size(670, 722);
            this.VideoPlayer.TabIndex = 7;
            this.VideoPlayer.Text = "videoSourcePlayer1";
            this.VideoPlayer.VideoSource = null;
            this.VideoPlayer.Click += new System.EventHandler(this.VideoPlayer_Click);
            this.VideoPlayer.Paint += new System.Windows.Forms.PaintEventHandler(this.VideoPlayer_Paint);
            this.VideoPlayer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.VideoPlayer_MouseMove);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtTableNum);
            this.groupBox1.Controls.Add(this.lbCode);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.lbName);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.TextBoxID);
            this.groupBox1.Controls.Add(this.ButtonRegister);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(744, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(525, 730);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // txtTableNum
            // 
            this.txtTableNum.Location = new System.Drawing.Point(112, 571);
            this.txtTableNum.Margin = new System.Windows.Forms.Padding(4);
            this.txtTableNum.Multiline = true;
            this.txtTableNum.Name = "txtTableNum";
            this.txtTableNum.Size = new System.Drawing.Size(384, 38);
            this.txtTableNum.TabIndex = 15;
            // 
            // lbCode
            // 
            this.lbCode.AutoSize = true;
            this.lbCode.Location = new System.Drawing.Point(0, 574);
            this.lbCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCode.Name = "lbCode";
            this.lbCode.Size = new System.Drawing.Size(89, 18);
            this.lbCode.TabIndex = 14;
            this.lbCode.Text = "Table Num";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(112, 525);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Multiline = true;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(384, 38);
            this.txtName.TabIndex = 13;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(50, 528);
            this.lbName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(44, 18);
            this.lbName.TabIndex = 12;
            this.lbName.Text = "Name";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(34, 686);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(480, 34);
            this.button1.TabIndex = 11;
            this.button1.Text = "对比";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 768);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.VideoPlayer);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.Text = "人脸识别";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox TextBoxID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ButtonRegister;
        private System.Windows.Forms.PictureBox pictureBox1;
        private AForge.Controls.VideoSourcePlayer VideoPlayer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtTableNum;
        private System.Windows.Forms.Label lbCode;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lbName;
    }
}