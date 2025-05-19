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
            this.btn_select.ForeColor = System.Drawing.Color.Black;
            this.btn_select.Location = new System.Drawing.Point(39, 28);
            this.btn_select.Name = "btn_select";
            this.btn_select.ShadowDecoration.CustomizableEdges.BottomLeft = false;
            this.btn_select.ShadowDecoration.CustomizableEdges.BottomRight = false;
            this.btn_select.ShadowDecoration.CustomizableEdges.TopLeft = false;
            this.btn_select.ShadowDecoration.CustomizableEdges.TopRight = false;
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
            // Lounge
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::WindowsFormsApp4.Properties.Resources.lounge1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1258, 744);
            this.Controls.Add(this.btn_select);
            this.Controls.Add(this.chatLogBox);
            this.Controls.Add(this.inputBox);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
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
        private System.Windows.Forms.Timer animationTimer;
        private Guna.UI2.WinForms.Guna2Button btn_select;
        private Guna.UI2.WinForms.Guna2ShapesTool guna2ShapesTool1;
    }
}