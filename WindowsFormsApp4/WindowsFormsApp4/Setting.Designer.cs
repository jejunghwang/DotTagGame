namespace WindowsFormsApp4
{
    partial class Setting
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
            this.music_toggle.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.music_toggle.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.music_toggle.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.music_toggle.CheckedState.InnerColor = System.Drawing.Color.White;
            this.music_toggle.Location = new System.Drawing.Point(190, 222);
            this.music_toggle.Name = "music_toggle";
            this.music_toggle.Size = new System.Drawing.Size(123, 48);
            this.music_toggle.TabIndex = 9;
            this.music_toggle.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.music_toggle.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.music_toggle.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.music_toggle.UncheckedState.InnerColor = System.Drawing.Color.White;
            this.music_toggle.CheckedChanged += new System.EventHandler(this.music_toggle_CheckedChanged);
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BackgroundImage = global::WindowsFormsApp4.Properties.Resources.setting;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.music_toggle);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.lbl_setting);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Setting";
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
    }
}