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
            this.SuspendLayout();
            // 
            // hp_handling
            // 
            this.hp_handling.Interval = 1000;
            this.hp_handling.Tick += new System.EventHandler(this.hp_handling_Tick);
            // 
            // Map
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1259, 744);
            this.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.Name = "Map";
            this.Text = "Map";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Map_FormClosed);
            this.Load += new System.EventHandler(this.Map_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer animationTimer;
        private System.Windows.Forms.Timer hp_handling;
    }
}