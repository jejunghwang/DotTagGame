namespace WindowsFormsApp4
{
    partial class Loading
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Loading));
            this.lbl_loading = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.timerFrame = new System.Windows.Forms.Timer(this.components);
            this.timerClose = new System.Windows.Forms.Timer(this.components);
            this.lbl_count = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.pang = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pang)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_loading
            // 
            this.lbl_loading.AutoSize = false;
            this.lbl_loading.BackColor = System.Drawing.Color.Transparent;
            this.lbl_loading.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_loading.Location = new System.Drawing.Point(538, 298);
            this.lbl_loading.Name = "lbl_loading";
            this.lbl_loading.Size = new System.Drawing.Size(198, 31);
            this.lbl_loading.TabIndex = 1;
            this.lbl_loading.Text = "라 운 지 입 장 중 ...";
            // 
            // timerFrame
            // 
            this.timerFrame.Interval = 200;
            this.timerFrame.Tick += new System.EventHandler(this.TimerFrame_Tick);
            // 
            // timerClose
            // 
            this.timerClose.Interval = 5000;
            // 
            // lbl_count
            // 
            this.lbl_count.AutoSize = false;
            this.lbl_count.BackColor = System.Drawing.Color.Transparent;
            this.lbl_count.Font = new System.Drawing.Font("휴먼둥근헤드라인", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_count.Location = new System.Drawing.Point(593, 335);
            this.lbl_count.Name = "lbl_count";
            this.lbl_count.Size = new System.Drawing.Size(25, 25);
            this.lbl_count.TabIndex = 2;
            this.lbl_count.Text = "3";
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 50;
            // 
            // pang
            // 
            this.pang.BackColor = System.Drawing.Color.Transparent;
            this.pang.ImageRotate = 0F;
            this.pang.Location = new System.Drawing.Point(581, 225);
            this.pang.Name = "pang";
            this.pang.Size = new System.Drawing.Size(186, 167);
            this.pang.TabIndex = 0;
            this.pang.TabStop = false;
            // 
            // Loading
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Wheat;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.lbl_count);
            this.Controls.Add(this.lbl_loading);
            this.Controls.Add(this.pang);
            this.Name = "Loading";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lounge";
            this.Load += new System.EventHandler(this.Loading_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2PictureBox pang;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_loading;
        private System.Windows.Forms.Timer timerFrame;
        private System.Windows.Forms.Timer timerClose;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_count;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
    }
}