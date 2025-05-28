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
        }

        public void Move(int dx, int dy)
        {
            X += dx * Speed;
            Y += dy * Speed;
            Pbox.Location = new Point(X, Y);
        }

        public void SetPosition(int x, int y)
        {
            X = x; Y = y;
            Pbox.Location = new Point(X, Y);
        }
    }
}
