namespace WindowsFormsApp4
{
    partial class Resister
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resister));
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.btn_cancel = new Guna.UI2.WinForms.Guna2Button();
            this.btn_enter = new Guna.UI2.WinForms.Guna2GradientButton();
            this.txtNewPw = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtNewId = new Guna.UI2.WinForms.Guna2TextBox();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 50;
            this.guna2Elipse1.TargetControl = this;
            // 
            // guna2Button1
            // 
            this.guna2Button1.BorderColor = System.Drawing.Color.Silver;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.PaleVioletRed;
            this.guna2Button1.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Location = new System.Drawing.Point(240, 47);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(320, 75);
            this.guna2Button1.TabIndex = 12;
            this.guna2Button1.Text = "REGISTER";
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.Transparent;
            this.btn_cancel.BackgroundImage = global::WindowsFormsApp4.Properties.Resources.cancel;
            this.btn_cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_cancel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_cancel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_cancel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_cancel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_cancel.FillColor = System.Drawing.Color.Transparent;
            this.btn_cancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_cancel.ForeColor = System.Drawing.Color.White;
            this.btn_cancel.Location = new System.Drawing.Point(655, 21);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(150, 150);
            this.btn_cancel.TabIndex = 11;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_enter
            // 
            this.btn_enter.BackColor = System.Drawing.Color.Transparent;
            this.btn_enter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_enter.BackgroundImage")));
            this.btn_enter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_enter.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_enter.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_enter.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_enter.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_enter.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_enter.FillColor = System.Drawing.Color.Transparent;
            this.btn_enter.FillColor2 = System.Drawing.Color.Transparent;
            this.btn_enter.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_enter.ForeColor = System.Drawing.Color.White;
            this.btn_enter.Location = new System.Drawing.Point(123, 438);
            this.btn_enter.Name = "btn_enter";
            this.btn_enter.Size = new System.Drawing.Size(553, 99);
            this.btn_enter.TabIndex = 10;
            this.btn_enter.Text = "ENTER";
            this.btn_enter.Click += new System.EventHandler(this.btn_enter_Click);
            // 
            // txtNewPw
            // 
            this.txtNewPw.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNewPw.DefaultText = "";
            this.txtNewPw.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtNewPw.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtNewPw.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNewPw.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNewPw.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNewPw.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNewPw.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNewPw.Location = new System.Drawing.Point(123, 334);
            this.txtNewPw.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtNewPw.Name = "txtNewPw";
            this.txtNewPw.PlaceholderText = "비밀번호를 입력하세요.";
            this.txtNewPw.SelectedText = "";
            this.txtNewPw.Size = new System.Drawing.Size(553, 80);
            this.txtNewPw.TabIndex = 9;
            // 
            // txtNewId
            // 
            this.txtNewId.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNewId.DefaultText = "";
            this.txtNewId.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtNewId.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtNewId.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNewId.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNewId.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNewId.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNewId.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNewId.Location = new System.Drawing.Point(123, 226);
            this.txtNewId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtNewId.Name = "txtNewId";
            this.txtNewId.PlaceholderText = "아이디를 입력하세요.";
            this.txtNewId.SelectedText = "";
            this.txtNewId.Size = new System.Drawing.Size(553, 87);
            this.txtNewId.TabIndex = 8;
            // 
            // Resister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGreen;
            this.BackgroundImage = global::WindowsFormsApp4.Properties.Resources.login;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 700);
            this.Controls.Add(this.guna2Button1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_enter);
            this.Controls.Add(this.txtNewPw);
            this.Controls.Add(this.txtNewId);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Resister";
            this.Text = "Resister";
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Button btn_cancel;
        private Guna.UI2.WinForms.Guna2GradientButton btn_enter;
        private Guna.UI2.WinForms.Guna2TextBox txtNewPw;
        private Guna.UI2.WinForms.Guna2TextBox txtNewId;
    }
}