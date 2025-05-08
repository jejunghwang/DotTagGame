namespace WindowsFormsApp4
{
    partial class Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.btn_menu = new Guna.UI2.WinForms.Guna2Button();
            this.guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2AnimateWindow1 = new Guna.UI2.WinForms.Guna2AnimateWindow(this.components);
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btn_start = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // btn_menu
            // 
            this.btn_menu.BackColor = System.Drawing.Color.Transparent;
            this.btn_menu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_menu.BackgroundImage")));
            this.btn_menu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_menu.BorderColor = System.Drawing.Color.Transparent;
            this.btn_menu.BorderRadius = 100;
            this.btn_menu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_menu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_menu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_menu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_menu.FillColor = System.Drawing.Color.Transparent;
            this.btn_menu.Font = new System.Drawing.Font("휴먼둥근헤드라인", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_menu.ForeColor = System.Drawing.Color.NavajoWhite;
            this.btn_menu.Location = new System.Drawing.Point(300, 480);
            this.btn_menu.Name = "btn_menu";
            this.btn_menu.ShadowDecoration.BorderRadius = 10;
            this.btn_menu.ShadowDecoration.Enabled = true;
            this.btn_menu.Size = new System.Drawing.Size(250, 100);
            this.btn_menu.TabIndex = 3;
            this.btn_menu.Text = "MENU";
            this.btn_menu.Click += new System.EventHandler(this.btn_menu_Click);
            // 
            // guna2HtmlLabel3
            // 
            this.guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel3.Location = new System.Drawing.Point(0, 0);
            this.guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            this.guna2HtmlLabel3.Size = new System.Drawing.Size(3, 2);
            this.guna2HtmlLabel3.TabIndex = 0;
            this.guna2HtmlLabel3.Text = null;
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(0, 0);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(3, 2);
            this.guna2HtmlLabel2.TabIndex = 0;
            this.guna2HtmlLabel2.Text = null;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(0, 0);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(3, 2);
            this.guna2HtmlLabel1.TabIndex = 0;
            this.guna2HtmlLabel1.Text = null;
            // 
            // btn_start
            // 
            this.btn_start.BackColor = System.Drawing.Color.Transparent;
            this.btn_start.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_start.BackgroundImage")));
            this.btn_start.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_start.BorderRadius = 100;
            this.btn_start.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_start.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_start.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_start.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_start.FillColor = System.Drawing.Color.Transparent;
            this.btn_start.Font = new System.Drawing.Font("휴먼둥근헤드라인", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_start.ForeColor = System.Drawing.Color.NavajoWhite;
            this.btn_start.Location = new System.Drawing.Point(700, 480);
            this.btn_start.Name = "btn_start";
            this.btn_start.ShadowDecoration.BorderRadius = 10;
            this.btn_start.ShadowDecoration.Enabled = true;
            this.btn_start.Size = new System.Drawing.Size(250, 100);
            this.btn_start.TabIndex = 4;
            this.btn_start.Text = "START";
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // Main
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.BackgroundImage = global::WindowsFormsApp4.Properties.Resources.title1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1258, 744);
            this.Controls.Add(this.guna2HtmlLabel3);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.btn_menu);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(750, 480);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button btn_menu;
        private Guna.UI2.WinForms.Guna2Button btn_start;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2AnimateWindow guna2AnimateWindow1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
    }
}

