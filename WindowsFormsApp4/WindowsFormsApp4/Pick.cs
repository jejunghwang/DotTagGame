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
    public partial class Pick : Form
    {
        private PictureBox[] pbs;

        public Pick()
        {
            InitializeComponent();

            pbs = new[] { pang1, pang2, pang3, pang4 };

            pang1.Image = Properties.Resources.pang1_front_1;
            pang2.Image = Properties.Resources.pang2_front_1;
            pang3.Image = Properties.Resources.pang3_front_1;
            pang4.Image = Properties.Resources.pang4_front_4;

            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";

            this.Resize += Pick_Resize;

            Pick_Resize(this, EventArgs.Empty);
        }

        private void Pick_Resize(object sender, EventArgs e)
        {
            int count = pbs.Length;
            int marginInside = 20;  // PictureBox 간 수평 간격
            int marginVertical = 40;  // PictureBox, Button 간 수직 간격

            int charOffsetX = 40;   // 캐릭터 그룹을 오른쪽으로 이동
            int charOffsetY = 30;   // 캐릭터 그룹을 아래로 이동
            int buttonOffsetX = -5;  // 버튼을 왼쪽으로 이동
            int buttonOffsetY = -30;  // 버튼을 위로 이동
                                      

            // 1) PictureBox 크기 계산
            int totalMarginInside = marginInside * (count - 1);
            int availableWidth = ClientSize.Width - totalMarginInside;
            int pbWidth = availableWidth / count;
            int pbHeight = (int)(pbWidth * 0.75);

            // 2) 캐릭터 그룹 가로 중앙 정렬 + charOffsetX
            int totalWidthBoxes = pbWidth * count + totalMarginInside;
            int startX = (ClientSize.Width - totalWidthBoxes) / 2 + charOffsetX;

            // 3) 캐릭터 그룹 세로 중앙 정렬 + charOffsetY
            int groupHeight = pbHeight + marginVertical + btn_pick.Height;
            int groupTop = (ClientSize.Height - groupHeight) / 2 + charOffsetY;

            // 4) PictureBox 배치
            for (int i = 0; i < count; i++)
            {
                var pb = pbs[i];
                pb.Width = pbWidth;
                pb.Height = pbHeight;
                pb.Left = startX + i * (pbWidth + marginInside);
                pb.Top = groupTop;
            }

            // 5) 버튼 배치: 기본 중앙 정렬 + 버튼 전용 오프셋
            btn_pick.Left = (ClientSize.Width - btn_pick.Width) / 2 + buttonOffsetX;
            btn_pick.Top = groupTop + pbHeight + marginVertical + buttonOffsetY;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
