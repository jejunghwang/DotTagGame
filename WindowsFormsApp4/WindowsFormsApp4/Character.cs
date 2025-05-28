using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace WindowsFormsApp4
{
    public static class Players
    {
        public static Character[] players = new Character[100];
        
        public static void add_player(int playerTag, string name)
        {
            players[playerTag] = new Character(name, playerTag);
        }
    }
    public class Character
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int HP { get; set; }
        public int Speed { get; set; }
        public PictureBox Pbox { get; private set; }
        public Label NameLabel { get; private set; }
        public Label ReadyBubble { get; private set; }
        public PictureBox BubbleBox { get; private set; }

        public bool isReady = false;

        public Character(string name, int x= 937, int y= 270, int hp=100, int speed=5)
        {
            Name = name;
            X = x;
            Y = y;
            HP = hp;
            Speed = speed;

            // 캐릭터 이미지
            Pbox = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Properties.Resources.pang1_front_1,
                BackColor = Color.Transparent,
                Size = new Size(100, 100),
                Location = new Point(X, Y)
            };

            // 이름 라벨
            NameLabel = new Label
            {
                Text = name,
                AutoSize = true,
                Font = new Font("맑은 고딕", 9, FontStyle.Bold),
                ForeColor = Color.Black,
                BackColor = Color.Transparent,
                Location = new Point(X, Y + Pbox.Height)  // Pbox 바로 아래
            };

            // 말풍선 박스
            BubbleBox = new PictureBox
            {
                Image = Properties.Resources.bubble,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
                Visible = false
            };

            // 말풍선 안 라벨
            ReadyBubble = new Label
            {
                Text = "준비",
                AutoSize = true,
                Font = new Font("맑은 고딕", 8, FontStyle.Bold),
                ForeColor = Color.Black,
                BackColor = Color.Transparent,
            };

            BubbleBox.Controls.Add(ReadyBubble);
        }

        public void Move(int dx, int dy)
        {
            X += dx * Speed;
            Y += dy * Speed;
            //Pbox.Location = new Point(X, Y);
            UpdateControls();
        }

        public void SetPosition(int x, int y)
        {
            X = x; Y = y;
            //Pbox.Location = new Point(X, Y);
            UpdateControls();
        }

        private void UpdateControls()
        {
            // 캐릭터 위치
            Pbox.Location = new Point(X, Y);

            // 이름 라벨
            int labelX = X + (Pbox.Width - NameLabel.Width) / 2;
            int labelY = Y + Pbox.Height;
            NameLabel.Location = new Point(labelX, labelY);

            // 말풍선 박스 위치 (Pbox 위 중앙)
            int bx = X + (Pbox.Width - BubbleBox.Width) / 2;
            int by = Y - BubbleBox.Height - 4;
            BubbleBox.Location = new Point(bx, by);

            // 말풍선 안 Label 중앙
            ReadyBubble.Location = new Point(
              (BubbleBox.Width - ReadyBubble.Width) / 2,
              (BubbleBox.Height - ReadyBubble.Height) / 2
            );
        }
    }
}
