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
using System.Media;
using System.Net.Sockets;
using WindowsFormsApp4.Properties;

namespace WindowsFormsApp4
{
    public partial class Main : Form
    {
        public SoundPlayer bgm;
        private Panel overlayPanel;

        TcpClient client;
        NetworkStream stream;

        public Main()
        {
            InitializeComponent();
            this.Resize += Main_Resize;
        }

        // 메인 폼 크기 및 버튼 위치, 크기 조정 (반응형 ui)
        private void Main_Resize(object sender, EventArgs e)
        {
            int baseWidth = 1280;   
            int baseHeight = 800; 

            float scaleX = (float)this.Width / baseWidth;
            float scaleY = (float)this.Height / baseHeight;

            btn_menu.Left = (int)(baseWidth * 0.25f * scaleX);  
            btn_menu.Top = (int)(baseHeight * 0.64f * scaleY);

            btn_start.Left = (int)(baseWidth * 0.55f * scaleX);  
            btn_start.Top = btn_menu.Top;

            btn_menu.Width = (int)(250 * scaleX);
            btn_menu.Height = (int)(100 * scaleY);

            btn_start.Width = btn_menu.Width;
            btn_start.Height = btn_menu.Height;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            bgm = new SoundPlayer(Properties.Resources.bgm);
            bgm.PlayLooping();

            overlayPanel = new Panel();
            overlayPanel.Dock = DockStyle.Fill;
            overlayPanel.BackColor = Color.FromArgb(150, 0, 0, 0); // 반투명 검정
            overlayPanel.Visible = false;
            overlayPanel.BringToFront();

            this.Controls.Add(overlayPanel);
        }

        private void btn_menu_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                // BGM 정지
                bgm.Stop();

                // 버튼 효과음 동기 재생
                using (SoundPlayer effect = new SoundPlayer(Properties.Resources.buttonClick))
                {
                    effect.PlaySync();
                }

                // 효과음 끝난 뒤 BGM 다시 재생
                bgm.PlayLooping();
            });

            overlayPanel.Visible = true;
            overlayPanel.BringToFront();

            Menu menu = new Menu();
            menu.StartPosition = FormStartPosition.CenterParent;
            menu.ShowDialog();

            overlayPanel.Visible = false;
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                // BGM 정지
                bgm.Stop();

                // 버튼 효과음 동기 재생
                using (SoundPlayer effect = new SoundPlayer(Properties.Resources.buttonClick))
                {
                    effect.PlaySync();
                }

                // 효과음 끝난 뒤 BGM 다시 재생
                bgm.PlayLooping();
            });

            overlayPanel.Visible = true;
            overlayPanel.BringToFront();

            Login login = new Login(this);
            login.Owner = this;
            login.StartPosition = FormStartPosition.CenterParent;
            login.ShowDialog();

            overlayPanel.Visible = false;
        }
    }
}
