namespace WindowsFormsApp4
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.txtId = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtPw = new Guna.UI2.WinForms.Guna2TextBox();
            this.btn_enter = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btn_cancel = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.lbl_register = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 50;
            this.guna2Elipse1.TargetControl = this;
            // 
            // txtId
            // 
            this.txtId.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtId.DefaultText = "";
            this.txtId.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtId.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtId.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtId.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtId.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtId.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtId.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
<<<<<<< HEAD
            this.txtId.Location = new System.Drawing.Point(122, 214);
            this.txtId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
=======
            this.txtId.Location = new System.Drawing.Point(86, 155);
>>>>>>> 24ed230feb9f59bff53aa82b64ad98f5ef2cd03a
            this.txtId.Name = "txtId";
            this.txtId.PlaceholderText = "아이디를 입력하세요.";
            this.txtId.SelectedText = "";
            this.txtId.Size = new System.Drawing.Size(387, 58);
            this.txtId.TabIndex = 2;
           // this.txtId.Click += new System.EventHandler(this.txtId_Click);
            // 
            // txtPw
            // 
            this.txtPw.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPw.DefaultText = "";
            this.txtPw.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtPw.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtPw.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPw.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPw.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPw.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPw.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
<<<<<<< HEAD
            this.txtPw.Location = new System.Drawing.Point(122, 322);
            this.txtPw.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
=======
            this.txtPw.Location = new System.Drawing.Point(86, 227);
>>>>>>> 24ed230feb9f59bff53aa82b64ad98f5ef2cd03a
            this.txtPw.Name = "txtPw";
            this.txtPw.PlaceholderText = "비밀번호를 입력하세요.";
            this.txtPw.SelectedText = "";
            this.txtPw.Size = new System.Drawing.Size(387, 53);
            this.txtPw.TabIndex = 3;
<<<<<<< HEAD
=======
            this.txtPw.Click += new System.EventHandler(this.txtPwd_Click);
            this.txtPw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPw_KeyDown);
>>>>>>> 24ed230feb9f59bff53aa82b64ad98f5ef2cd03a
            // 
            // btn_enter
            // 
<<<<<<< HEAD
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
            this.btn_enter.Location = new System.Drawing.Point(122, 426);
            this.btn_enter.Name = "btn_enter";
            this.btn_enter.Size = new System.Drawing.Size(553, 99);
            this.btn_enter.TabIndex = 4;
            this.btn_enter.Text = "ENTER";
            this.btn_enter.Click += new System.EventHandler(this.btn_enter_Click);
=======
            this.guna2GradientButton1.BackColor = System.Drawing.Color.Transparent;
            this.guna2GradientButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("guna2GradientButton1.BackgroundImage")));
            this.guna2GradientButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.guna2GradientButton1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2GradientButton1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2GradientButton1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2GradientButton1.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2GradientButton1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2GradientButton1.FillColor = System.Drawing.Color.Transparent;
            this.guna2GradientButton1.FillColor2 = System.Drawing.Color.Transparent;
            this.guna2GradientButton1.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.guna2GradientButton1.ForeColor = System.Drawing.Color.White;
            this.guna2GradientButton1.Location = new System.Drawing.Point(86, 296);
            this.guna2GradientButton1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.guna2GradientButton1.Name = "guna2GradientButton1";
            this.guna2GradientButton1.Size = new System.Drawing.Size(387, 66);
            this.guna2GradientButton1.TabIndex = 4;
            this.guna2GradientButton1.Text = "ENTER";
>>>>>>> 24ed230feb9f59bff53aa82b64ad98f5ef2cd03a
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
            this.btn_cancel.Location = new System.Drawing.Point(458, 14);
            this.btn_cancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(105, 100);
            this.btn_cancel.TabIndex = 6;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // guna2Button1
            // 
            this.guna2Button1.BorderColor = System.Drawing.Color.Silver;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.Sienna;
            this.guna2Button1.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
<<<<<<< HEAD
            this.guna2Button1.Location = new System.Drawing.Point(240, 47);
=======
            this.guna2Button1.Location = new System.Drawing.Point(168, 31);
            this.guna2Button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
>>>>>>> 24ed230feb9f59bff53aa82b64ad98f5ef2cd03a
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(224, 50);
            this.guna2Button1.TabIndex = 7;
            this.guna2Button1.Text = "LOGIN";
            // 
            // lbl_register
            // 
            this.lbl_register.AutoSize = false;
            this.lbl_register.BackColor = System.Drawing.Color.Transparent;
            this.lbl_register.Font = new System.Drawing.Font("함초롬돋움", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_register.Location = new System.Drawing.Point(279, 543);
            this.lbl_register.Name = "lbl_register";
            this.lbl_register.Size = new System.Drawing.Size(286, 31);
            this.lbl_register.TabIndex = 8;
            this.lbl_register.Text = "아직 계정이 없으신가요?";
            this.lbl_register.Click += new System.EventHandler(this.lbl_register_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGreen;
            this.BackgroundImage = global::WindowsFormsApp4.Properties.Resources.login;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
<<<<<<< HEAD
            this.ClientSize = new System.Drawing.Size(800, 700);
            this.Controls.Add(this.lbl_register);
=======
            this.ClientSize = new System.Drawing.Size(560, 467);
>>>>>>> 24ed230feb9f59bff53aa82b64ad98f5ef2cd03a
            this.Controls.Add(this.guna2Button1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_enter);
            this.Controls.Add(this.txtPw);
            this.Controls.Add(this.txtId);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Login";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2GradientButton btn_enter;
        private Guna.UI2.WinForms.Guna2TextBox txtPw;
        private Guna.UI2.WinForms.Guna2TextBox txtId;
        private Guna.UI2.WinForms.Guna2Button btn_cancel;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_register;
    }
}