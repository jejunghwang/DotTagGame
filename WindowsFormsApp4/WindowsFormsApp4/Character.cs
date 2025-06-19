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
    public enum Direction { up, down, right, left }
    public static class Players
    {
        public static Character[] players = new Character[100];
        
        public static void add_player(int playerTag, string name, int charIdx)
        {
            players[playerTag] = new Character(name, charIdx);
        }
    }
    public class Character
    {
        public string Name { get; set; }
        public int CharacterIndex { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int HP { get; set; }
        public int Speed { get; set; }

        public bool isTransparent { get; set; }

        public int itemDuration { get; set; }

        public int item = -1;
        public PictureBox Pbox { get; private set; }
        public Label NameLabel { get; private set; }
        public Label ReadyBubble { get; private set; }
        public PictureBox BubbleBox { get; private set; }
        public bool isTagger = false;

        public bool isReady = false;
        private Dictionary<Direction, List<Image>> frames = new Dictionary<Direction, List<Image>>();
        private Dictionary<Direction, List<Image>> tagger_frames = new Dictionary<Direction, List<Image>>();
        private int animIndex = 0;
        private Direction dir = Direction.down;

        public Character(string name, int charIdx, int x= 937, int y= 270, int hp=100, int speed=5)
        {
            Name = name;
            CharacterIndex = (charIdx >= 1 && charIdx <= 4) ? charIdx : 1;
            X = x;
            Y = y;
            HP = hp;
            Speed = speed;
            itemDuration = 5;
            isTransparent = false;
            // 캐릭터 이미지
            Pbox = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                //Image = Properties.Resources.pang1_front_1,
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
            LoadAllFrames();
            LoadTaggerFrames();
            Pbox.Image = frames[dir][0];
            UpdateControls();
        }

        public void Move(int dx, int dy)
        {
            if (dx < 0) dir = Direction.left;
            else if (dx > 0) dir = Direction.right;
            else if (dy < 0) dir = Direction.up;
            else if (dy > 0) dir = Direction.down;

            X += dx * Speed;
            Y += dy * Speed;
            //Pbox.Location = new Point(X, Y);
            UpdateControls();
            Animate();
        }



        public void SetPosition(int x, int y)
        {
            X = x; Y = y;
            //Pbox.Location = new Point(X, Y);
            UpdateControls();
            Animate();
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
        public void SetCharacter(int newIdx)
        {
            CharacterIndex = newIdx;
            LoadAllFrames();
            dir = Direction.down;
            animIndex = 0;
            Pbox.Image = frames[dir][animIndex];
        }

        public void SetTagger(bool tagger)
        {
            isTagger = tagger;
            animIndex = 0;
            LoadTaggerFrames();
            dir = Direction.down;  
                                 
            Pbox.Image = (isTagger
                ? tagger_frames[dir][0]
                : frames[dir][0]);
        }

        private void LoadAllFrames()
        {
            frames.Clear();
            var map = new Dictionary<Direction, string>
            {
                [Direction.up] = "back",
                [Direction.down] = "front",
                [Direction.left] = "left",
                [Direction.right] = "right"
            };
            foreach (var kv in map)
            {
                var list = new List<Image>();
                for (int i = 1; i <= 4; i++)
                {
                    string key = $"pang{CharacterIndex}_{kv.Value}_{i}";
                    var img = Properties.Resources.ResourceManager.GetObject(key) as Image;
                    if (img != null) list.Add(img);
                }
                frames[kv.Key] = list;
            }
        }
        private void LoadTaggerFrames()
        {
            tagger_frames.Clear();
            var map = new Dictionary<Direction, string>
            {
                [Direction.up] = "tagger_back",
                [Direction.down] = "tagger_front",
                [Direction.left] = "tagger_left",
                [Direction.right] = "tagger_right"
            };
            foreach (var kv in map)
            {
                var list = new List<Image>();
                string key = kv.Value;
                if (Properties.Resources.ResourceManager.GetObject(key) is Image img)
                    list.Add(img);
                tagger_frames[kv.Key] = list;
            }
        }

        private void Animate()
        {
            var dict = isTagger ? tagger_frames : frames;
            var list = dict[dir];
            if (list.Count == 0) return;
            animIndex = (animIndex + 1) % list.Count;
            Pbox.Image = list[animIndex];
        }

        // 원격에서 이동
        public void MoveAbsolute(int newX, int newY)
        {
            int dx = newX - X;
            int dy = newY - Y;

            // 방향 갱신
            if (dx < 0) dir = Direction.left;
            else if (dx > 0) dir = Direction.right;
            else if (dy < 0) dir = Direction.up;
            else if (dy > 0) dir = Direction.down;

            // 위치 갱신
            X = newX;
            Y = newY;

            UpdateControls();
            Animate();
        }

    }
}
