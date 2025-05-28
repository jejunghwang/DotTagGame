using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public bool isReady = false;

        public Character(string name, int x= 937, int y= 270, int hp=100, int speed=5)
        {
            Name = name;
            X = x;
            Y = y;
            HP = hp;
            Speed = speed;

            Pbox = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Properties.Resources.pang1_front_1,
                BackColor = Color.Transparent,
                Size = new Size(100, 100),
                Location = new Point(X, Y)
            };

            NameLabel = new Label
            {
                Text = name,
                AutoSize = true,
                Font = new Font("맑은 고딕", 9, FontStyle.Bold),
                ForeColor = Color.Black,
                BackColor = Color.Transparent,
                Location = new Point(X, Y + Pbox.Height)  // Pbox 바로 아래
            };
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
            Pbox.Location = new Point(X, Y);
            int labelX = X + (Pbox.Width - NameLabel.Width) / 2;
            int labelY = Y + Pbox.Height;
            NameLabel.Location = new Point(labelX, labelY);
        }
    }
}
