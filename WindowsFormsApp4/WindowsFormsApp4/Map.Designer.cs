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
            this.item_get = new System.Windows.Forms.Timer(this.components);
            this.speedup_timer = new System.Windows.Forms.Timer(this.components);
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
            // item_get
            // 
            this.item_get.Interval = 10;
            this.item_get.Tick += new System.EventHandler(this.item_get_Tick);
            // 
            // speedup_timer
            // 
            this.speedup_timer.Tick += new System.EventHandler(this.speedup_timer_Tick);
            // 
            // Map
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1636, 992);
            this.Margin = new System.Windows.Forms.Padding(2);
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
        private System.Windows.Forms.Timer item_get;
        private System.Windows.Forms.Timer speedup_timer;
    }
}