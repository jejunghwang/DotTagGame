namespace WindowsFormsApp4
{
    partial class Map
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
            this.animationTimer = new System.Windows.Forms.Timer(this.components);
            this.hp_handling = new System.Windows.Forms.Timer(this.components);
            this.stun = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // hp_handling
            // 
            this.hp_handling.Interval = 1000;
            this.hp_handling.Tick += new System.EventHandler(this.hp_handling_Tick);
            // 
            // stun
            // 
            this.stun.Interval = 2000;
            this.stun.Tick += new System.EventHandler(this.stun_Tick);
            // 
            // Map
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 496);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "Map";
            this.Text = "Map";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Map_FormClosed);
            this.Load += new System.EventHandler(this.Map_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer animationTimer;
        private System.Windows.Forms.Timer hp_handling;
        private System.Windows.Forms.Timer stun;
    }
}