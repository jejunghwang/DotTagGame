namespace WindowsFormsApp4
{
    partial class lbl_bgm_toggle
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbl_setting = new System.Windows.Forms.Label();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.btn_cancel = new Guna.UI2.WinForms.Guna2Button();
            this.music_toggle = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.lbl_bgm = new System.Windows.Forms.Label();
            this.bgm_toggle = new System.Windows.Forms.Label();
            this.lbl_mode = new System.Windows.Forms.Label();
            this.lbl_mode_toggle = new System.Windows.Forms.Label();
            this.mode_toggle = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.other = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::WindowsFormsApp4.Properties.Resources.settingString;
            this.pictureBox1.Location = new System.Drawing.Point(223, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(350, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lbl_setting
            // 
            this.lbl_setting.AutoSize = true;
            this.lbl_setting.BackColor = System.Drawing.Color.Chocolate;
            this.lbl_setting.Font = new System.Drawing.Font("휴먼둥근헤드라인", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_setting.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_setting.Location = new System.Drawing.Point(284, 38);
            this.lbl_setting.Name = "lbl_setting";
            this.lbl_setting.Size = new System.Drawing.Size(227, 41);
            this.lbl_setting.TabIndex = 1;
            this.lbl_setting.Text = "SETTING";
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
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
            this.btn_cancel.Location = new System.Drawing.Point(677, -15);
            this.btn_cancel.Margin = new System.Windows.Forms.Padding(4);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(125, 127);
            this.btn_cancel.TabIndex = 7;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // music_toggle
            // 
            this.music_toggle.BackColor = System.Drawing.Color.Transparent;
            this.music_toggle.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.music_toggle.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.music_toggle.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.music_toggle.CheckedState.InnerColor = System.Drawing.Color.White;
            this.music_toggle.Location = new System.Drawing.Point(188, 220);
            this.music_toggle.Name = "music_toggle";
            this.music_toggle.Size = new System.Drawing.Size(123, 48);
            this.music_toggle.TabIndex = 9;
            this.music_toggle.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.music_toggle.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.music_toggle.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.music_toggle.UncheckedState.InnerColor = System.Drawing.Color.White;
            this.music_toggle.CheckedChanged += new System.EventHandler(this.music_toggle_CheckedChanged);
            // 
            // lbl_bgm
            // 
            this.lbl_bgm.AutoSize = true;
            this.lbl_bgm.BackColor = System.Drawing.Color.Transparent;
            this.lbl_bgm.Font = new System.Drawing.Font("휴먼둥근헤드라인", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_bgm.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_bgm.Location = new System.Drawing.Point(80, 231);
            this.lbl_bgm.Name = "lbl_bgm";
            this.lbl_bgm.Size = new System.Drawing.Size(102, 25);
            this.lbl_bgm.TabIndex = 10;
            this.lbl_bgm.Text = "BGM :";
            // 
            // bgm_toggle
            // 
            this.bgm_toggle.AutoSize = true;
            this.bgm_toggle.BackColor = System.Drawing.Color.Transparent;
            this.bgm_toggle.Font = new System.Drawing.Font("휴먼둥근헤드라인", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bgm_toggle.ForeColor = System.Drawing.Color.RoyalBlue;
            this.bgm_toggle.Location = new System.Drawing.Point(221, 284);
            this.bgm_toggle.Name = "bgm_toggle";
            this.bgm_toggle.Size = new System.Drawing.Size(54, 25);
            this.bgm_toggle.TabIndex = 11;
            this.bgm_toggle.Text = "ON";
            // 
            // lbl_mode
            // 
            this.lbl_mode.AutoSize = true;
            this.lbl_mode.BackColor = System.Drawing.Color.Transparent;
            this.lbl_mode.Font = new System.Drawing.Font("휴먼둥근헤드라인", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_mode.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_mode.Location = new System.Drawing.Point(354, 231);
            this.lbl_mode.Name = "lbl_mode";
            this.lbl_mode.Size = new System.Drawing.Size(118, 25);
            this.lbl_mode.TabIndex = 13;
            this.lbl_mode.Text = "MODE :";
            // 
            // lbl_mode_toggle
            // 
            this.lbl_mode_toggle.AutoSize = true;
            this.lbl_mode_toggle.BackColor = System.Drawing.Color.Transparent;
            this.lbl_mode_toggle.Font = new System.Drawing.Font("휴먼둥근헤드라인", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_mode_toggle.ForeColor = System.Drawing.Color.Gold;
            this.lbl_mode_toggle.Location = new System.Drawing.Point(490, 283);
            this.lbl_mode_toggle.Name = "lbl_mode_toggle";
            this.lbl_mode_toggle.Size = new System.Drawing.Size(102, 25);
            this.lbl_mode_toggle.TabIndex = 14;
            this.lbl_mode_toggle.Text = "LIGHT";
            // 
            // mode_toggle
            // 
            this.mode_toggle.BackColor = System.Drawing.Color.Transparent;
            this.mode_toggle.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.mode_toggle.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.mode_toggle.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.mode_toggle.CheckedState.InnerColor = System.Drawing.Color.White;
            this.mode_toggle.Location = new System.Drawing.Point(478, 220);
            this.mode_toggle.Name = "mode_toggle";
            this.mode_toggle.Size = new System.Drawing.Size(123, 48);
            this.mode_toggle.TabIndex = 12;
            this.mode_toggle.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.mode_toggle.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.mode_toggle.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.mode_toggle.UncheckedState.InnerColor = System.Drawing.Color.White;
            this.mode_toggle.CheckedChanged += new System.EventHandler(this.mode_toggle_CheckedChanged);
            // 
            // other
            // 
            this.other.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.other.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.other.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.other.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.other.FillColor = System.Drawing.Color.Crimson;
            this.other.Font = new System.Drawing.Font("휴먼둥근헤드라인", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.other.ForeColor = System.Drawing.Color.White;
            this.other.Location = new System.Drawing.Point(608, 217);
            this.other.Name = "other";
            this.other.Size = new System.Drawing.Size(94, 48);
            this.other.TabIndex = 15;
            this.other.Text = "other";
            this.other.Click += new System.EventHandler(this.other_Click);
            // 
            // lbl_bgm_toggle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.BackgroundImage = global::WindowsFormsApp4.Properties.Resources.setting;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.other);
            this.Controls.Add(this.lbl_mode_toggle);
            this.Controls.Add(this.lbl_mode);
            this.Controls.Add(this.mode_toggle);
            this.Controls.Add(this.bgm_toggle);
            this.Controls.Add(this.lbl_bgm);
            this.Controls.Add(this.music_toggle);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.lbl_setting);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "lbl_bgm_toggle";
            this.Text = "Setting";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbl_setting;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Button btn_cancel;
        private Guna.UI2.WinForms.Guna2ToggleSwitch music_toggle;
        private System.Windows.Forms.Label bgm_toggle;
        private System.Windows.Forms.Label lbl_bgm;
        private System.Windows.Forms.Label lbl_mode;
        private System.Windows.Forms.Label lbl_mode_toggle;
        private Guna.UI2.WinForms.Guna2Button other;
        private Guna.UI2.WinForms.Guna2ToggleSwitch mode_toggle;
        private System.Windows.Forms.ColorDialog colorDialog;
    }
}