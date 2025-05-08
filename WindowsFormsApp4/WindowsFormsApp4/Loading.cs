using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Loading : Form
    {
        private int frame = 0;
        private List<Image> runFrames = new List<Image>();

        public Loading()
        {
            InitializeComponent();

            runFrames.Add(Properties.Resources.pang1_left_1);
            runFrames.Add(Properties.Resources.pang1_left_2);
            runFrames.Add(Properties.Resources.pang1_left_3);
            runFrames.Add(Properties.Resources.pang1_left_4);

            pang.Image = runFrames[0];

            timerFrame.Tick += TimerFrame_Tick;

            timerClose.Tick += (s, e) =>
            {
                timerClose.Stop();
                this.Close();
            };

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            timerFrame.Start();
            timerClose.Start();
        }

        private void TimerFrame_Tick(object sender, EventArgs e)
        {
            frame = (frame + 1) % runFrames.Count;
            pang.Image = runFrames[frame];
            lbl_count.Text = "" + frame;
        }
    }
}
