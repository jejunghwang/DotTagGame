namespace WindowsFormsApp4
{
    partial class Lounge
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
            this.inputBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.chatLogBox = new System.Windows.Forms.RichTextBox();
            this.animationTimer = new System.Windows.Forms.Timer(this.components);
            this.btn_select = new Guna.UI2.WinForms.Guna2Button();
            this.guna2ShapesTool1 = new Guna.UI2.WinForms.Guna2ShapesTool(this.components);
            this.btn_ready = new Guna.UI2.WinForms.Guna2Button();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btn_start = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // inputBox
            // 
            this.inputBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.inputBox.DefaultText = "";
            this.inputBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.inputBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.inputBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.inputBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.inputBox.FillColor = System.Drawing.Color.DarkGoldenrod;
            this.inputBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.inputBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.inputBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.inputBox.Location = new System.Drawing.Point(39, 659);
            this.inputBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.inputBox.Name = "inputBox";
            this.inputBox.PlaceholderText = "";
            this.inputBox.SelectedText = "";
            this.inputBox.Size = new System.Drawing.Size(402, 42);
            this.inputBox.TabIndex = 1;
            this.inputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputBox_KeyDown);
            // 
            // chatLogBox
            // 
            this.chatLogBox.BackColor = System.Drawing.Color.DarkGreen;
            this.chatLogBox.ForeColor = System.Drawing.Color.Transparent;
            this.chatLogBox.Location = new System.Drawing.Point(39, 295);
            this.chatLogBox.Name = "chatLogBox";
            this.chatLogBox.Size = new System.Drawing.Size(402, 356);
            this.chatLogBox.TabIndex = 2;
            this.chatLogBox.Text = "";
            // 
            // btn_select
            // 
            this.btn_select.BackColor = System.Drawing.Color.Transparent;
            this.btn_select.BackgroundImage = global::WindowsFormsApp4.Properties.Resources.select;
            this.btn_select.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_select.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_select.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_select.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_select.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_select.FillColor = System.Drawing.Color.Transparent;
            this.btn_select.Font = new System.Drawing.Font("휴먼둥근헤드라인", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_select.ForeColor = System.Drawing.Color.DarkOrange;
            this.btn_select.Location = new System.Drawing.Point(39, 28);
            this.btn_select.Name = "btn_select";
            this.btn_select.ShadowDecoration.BorderRadius = 3;
            this.btn_select.Size = new System.Drawing.Size(274, 111);
            this.btn_select.TabIndex = 3;
            this.btn_select.Text = "캐릭터 선택";
            this.btn_select.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // guna2ShapesTool1
            // 
            this.guna2ShapesTool1.Location = new System.Drawing.Point(0, 0);
            this.guna2ShapesTool1.PolygonSkip = 1;
            this.guna2ShapesTool1.Rotate = 0F;
            this.guna2ShapesTool1.Size = new System.Drawing.Size(200, 200);
            this.guna2ShapesTool1.TargetControl = null;
            // 
            // btn_ready
            // 
            this.btn_ready.BackColor = System.Drawing.Color.Transparent;
            this.btn_ready.BackgroundImage = global::WindowsFormsApp4.Properties.Resources.ready;
            this.btn_ready.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ready.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_ready.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_ready.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_ready.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_ready.FillColor = System.Drawing.Color.Transparent;
            this.btn_ready.Font = new System.Drawing.Font("휴먼둥근헤드라인", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_ready.ForeColor = System.Drawing.Color.White;
            this.btn_ready.Location = new System.Drawing.Point(970, 476);
            this.btn_ready.Name = "btn_ready";
            this.btn_ready.Size = new System.Drawing.Size(250, 100);
            this.btn_ready.TabIndex = 4;
            this.btn_ready.Text = "READY";
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.BackColor = System.Drawing.Color.DarkGreen;
            this.guna2PictureBox1.BackgroundImage = global::WindowsFormsApp4.Properties.Resources.deco;
            this.guna2PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.guna2PictureBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(318, 602);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(111, 46);
            this.guna2PictureBox1.TabIndex = 6;
            this.guna2PictureBox1.TabStop = false;
            // 
            // btn_start
            // 
            this.btn_start.BackColor = System.Drawing.Color.Transparent;
            this.btn_start.BackgroundImage = global::WindowsFormsApp4.Properties.Resources.ready;
            this.btn_start.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_start.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_start.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_start.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_start.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_start.FillColor = System.Drawing.Color.Transparent;
            this.btn_start.Font = new System.Drawing.Font("휴먼둥근헤드라인", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_start.ForeColor = System.Drawing.Color.Crimson;
            this.btn_start.Location = new System.Drawing.Point(970, 601);
            this.btn_start.Name = "btn_start";
            this.btn_start.ShadowDecoration.Color = System.Drawing.Color.IndianRed;
            this.btn_start.Size = new System.Drawing.Size(250, 100);
            this.btn_start.TabIndex = 4;
            this.btn_start.Text = "START";
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // Lounge
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::WindowsFormsApp4.Properties.Resources.lounge1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1258, 744);
            this.Controls.Add(this.guna2PictureBox1);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.btn_ready);
            this.Controls.Add(this.btn_select);
            this.Controls.Add(this.chatLogBox);
            this.Controls.Add(this.inputBox);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "Lounge";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lounge";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Lounge_FormClosed);
            this.Load += new System.EventHandler(this.Lounge_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Lounge_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2TextBox inputBox;
        private System.Windows.Forms.RichTextBox chatLogBox;
        private System.Windows.Forms.Timer animationTimer;
        private Guna.UI2.WinForms.Guna2Button btn_select;
        private Guna.UI2.WinForms.Guna2ShapesTool guna2ShapesTool1;
        private Guna.UI2.WinForms.Guna2Button btn_ready;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2Button btn_start;
    }
}