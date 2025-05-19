using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
//using System.Net.Sockets;

namespace WindowsFormsApp4
{
    public partial class Loading : Form
    {
        private Main mainForm;

        private int frame = 0;
        int count = 19;
        private List<Image> runFrames = new List<Image>();
/*        private TcpClient client;
        private NetworkStream stream;
        private string userId;*/

        public Loading(Main mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;

            runFrames.Add(Properties.Resources.pang1_left_1);
            runFrames.Add(Properties.Resources.pang1_left_2);
            runFrames.Add(Properties.Resources.pang1_left_3);
            runFrames.Add(Properties.Resources.pang1_left_4);

            pang.Image = runFrames[0];
/*            userId = id;
            client = tcp;
            stream = network;*/
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            timerFrame.Start();
            //timerClose.Start();
        }

        private void TimerFrame_Tick(object sender, EventArgs e)
        {
            if(count == 0)
            {
                timerFrame.Stop();
                this.Close();

                Lounge lounge = new Lounge(mainForm);
                lounge.Owner = mainForm;
                lounge.Show(mainForm);
            }
            frame = (frame + 1) % runFrames.Count;
            pang.Image = runFrames[frame];
            lbl_count.Text = ((int)(count/5)).ToString();
            count--;
        }
    }
}
