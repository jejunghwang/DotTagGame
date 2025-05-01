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
using WindowsFormsApp4.Properties;

namespace WindowsFormsApp4
{
    public partial class Main : Form
    {
        public static SoundPlayer bgm;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            bgm = new SoundPlayer(Properties.Resources.bgm);
            bgm.PlayLooping();
        }

        private void btn_manual_Click(object sender, EventArgs e)
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

            Manual manual = new Manual();
            manual.Owner = this;
            manual.Show();
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

            Start start = new Start();
            start.Owner = this;
            start.Show();
        }
    }
}
