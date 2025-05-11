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
            this.inputBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.chatLogBox = new System.Windows.Forms.RichTextBox();
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
            this.inputBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
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
            this.chatLogBox.BackColor = System.Drawing.Color.Black;
            this.chatLogBox.Location = new System.Drawing.Point(39, 295);
            this.chatLogBox.Name = "chatLogBox";
            this.chatLogBox.Size = new System.Drawing.Size(402, 356);
            this.chatLogBox.TabIndex = 2;
            this.chatLogBox.Text = "";
            // 
            // Lounge
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::WindowsFormsApp4.Properties.Resources.lounge1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1258, 744);
            this.Controls.Add(this.chatLogBox);
            this.Controls.Add(this.inputBox);
            this.Name = "Lounge";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lounge";
            this.Load += new System.EventHandler(this.Lounge_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Lounge_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2TextBox inputBox;
        private System.Windows.Forms.RichTextBox chatLogBox;
    }
}