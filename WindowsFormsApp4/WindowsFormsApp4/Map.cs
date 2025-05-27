using Packets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Map : Form
    {
        private int playerX = 937, playerY = 270;
        private int moveSpeed = 5;

        // 캐릭터 애니메이션 이미지 (방향별)
        private List<Image> upFrames = new List<Image>();
        private List<Image> downFrames = new List<Image>();
        private List<Image> leftFrames = new List<Image>();
        private List<Image> rightFrames = new List<Image>();
        private int frameIndex = 0;
        private List<Image> frames;
        private Dictionary<int, Point> characterPositions = new Dictionary<int, Point>();


        private HashSet<Keys> pressedKeys = new HashSet<Keys>();
        private Dictionary<int,Image> dict=new Dictionary<int,Image>();

        private readonly HashSet<int> walkableTiles = new HashSet<int> {-15, -14, -13, -12, -11, -10, -9, -8, -5, -4, -3, -2, -1, 2, 3, 5};


        private int[,] map = {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7},
            {1,1,2,2,2,2,3,3,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6},
            {1,1,2,2,2,2,3,3,2,2,2,2,1,1,2,2,3,3,3,3,3,3,3,3,3,3,2,2,2,2,1,1,-7,-7,-6,-6,-6,-8,-9,-9,-9,-9,-9,-10,-7,-7},
            {1,1,2,2,2,2,3,3,2,2,2,2,1,1,2,2,3,3,3,3,3,3,3,3,3,3,2,2,2,2,1,1,-6,-6,-6,-6,-6,-15,3,3,3,3,3,-11,-6,-6},
            {1,1,2,2,2,2,3,3,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,-7,-7,-6,-6,-6,-15,3,-16,-16,-16,3,-11,-7,-7},
            {1,1,2,2,2,2,3,3,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,-6,-6,-1,-1,-1,-15,-16,-16,-16,-16,-16,-11,-6,-6},
            {1,1,2,2,2,2,3,3,2,2,2,2,1,1,2,2,2,2,2,2,2,2,3,3,3,3,2,2,2,2,1,1,-7,-7,-5,-5,-5,-15,-16,-16,-16,-16,-16,-11,-7,-7},
            {1,1,2,2,2,2,3,3,2,2,2,2,1,1,2,2,2,2,2,2,2,2,3,3,3,3,2,2,2,2,1,1,-6,-6,-2,-2,-2,-15,-16,-16,-16,-16,-16,-11,-6,-6},
            {1,1,2,2,2,2,3,3,2,2,2,2,1,1,2,2,2,2,2,2,2,2,3,3,3,3,2,2,2,2,1,1,-7,-7,-3,5,-4,-14,-13,-13,-13,-13,-13,-12,-7,-7 },
            {1,1,2,2,2,2,3,3,2,2,2,6,7,7,7,-3,5,-4,7,7,8,1,1,1,1,1,1,1,1,2,1,1,-6,-6,-3,5,-4,-6,-6,-6,-6,-3,5,-4,-6,-6},
            {1,1,2,2,2,2,3,3,2,2,2,13,4,4,4,-3,5,-4,4,4,9,1,1,1,1,1,1,1,1,2,1,1,-7,-7,-3,5,-4,-6,-6,-6,-6,-3,5,-4,-7,-7},
            {1,1,2,2,2,2,3,3,2,2,2,13,4,4,4,-3,5,-4,4,4,9,2,2,2,2,2,2,2,2,2,1,1,-6,-6,-3,5,-4,-6,-6,-6,-6,-3,5,-4,-6,-6},
            {1,1,2,2,2,2,3,3,2,2,2,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,2,2,3,3,2,2,2,2,2,1,1,-7,-7,-3,5,-4,-6,-6,-6,-6,-3,5,-4,-7,-7},
            {1,1,2,2,2,2,3,3,2,2,2,-5,-5,-5,-5,-5,-5,-5,-5,-5,-5,2,2,3,3,2,2,2,2,2,1,1,-6,-6,-3,5,-4,-6,-6,-6,-6,-3,5,-4,-6,-6},
            {1,1,2,2,2,2,2,2,2,2,2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,2,2,3,3,2,2,2,2,2,-1,-1,-1,-1,-1,-1,-1,-1,-1,-6,-6,-3,5,-4,-7,-7},
            {1,1,2,2,2,2,2,2,2,2,2,13,4,4,-3,5,-4,4,4,4,9,2,2,3,3,2,2,2,2,2,-5,-5,-5,-5,-5,-5,-5,-5,-5,-6,-6,-3,5,-4,-6,-6},
            {1,1,6,7,7,7,8,2,2,2,2,13,4,4,-3,5,-4,4,4,4,9,2,2,3,3,2,2,2,2,2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-6,-6,-3,5,-4,-7,-7},
            {1,1,13,-1,-1,-1,-1,2,2,2,2,13,4,4,-3,5,-4,4,4,4,9,2,2,3,3,2,2,2,2,2,1,1,-6,-6,-6,-6,-3,5,-4,-6,-6,-3,5,-4,-6,-6},
            {1,1,13,-5,-5,-5,-5,2,2,2,2,13,4,4,-3,5,-4,4,4,4,9,2,2,3,3,2,2,2,2,2,1,1,-7,-7,-6,-6,-3,5,-4,-6,-6,-3,5,-4,-7,-7},
            {1,1,13,-2,-2,-2,-2,2,2,2,2,12,11,11,-3,5,-4,11,11,11,10,2,2,3,3,2,2,2,2,2,1,1,-6,-6,-6,-6,-3,5,-4,-6,-6,-3,5,-4,-6,-6},
            {1,1,13,-3,5,-4,9,3,3,3,2,2,2,2,2,2,2,2,2,2,2,2,2,3,3,2,2,2,2,2,1,1,-7,-7,-6,-6,-3,5,-4,-6,-6,-3,5,-4,-7,-7},
            {1,1,13,-3,5,-4,9,3,3,3,2,2,2,2,2,2,2,2,2,2,2,2,2,3,3,2,2,2,2,2,1,1,-6,-6,-6,-6,-3,5,-4,-6,-6,-3,5,-4,-6,-6},
            {1,1,13,-3,5,-4,9,3,3,3,2,2,2,2,2,2,2,2,2,2,2,2,2,3,3,2,2,2,2,2,1,1,-7,-7,-6,-6,-3,5,-4,-6,-6,-3,5,-4,-7,-7},
            {1,1,13,-3,5,-4,9,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,3,3,2,2,2,2,2,1,1,-6,-6,-6,-6,-3,5,-4,-6,-6,-3,5,-4,-6,-6},
            {1,1,13,-1,-1,-1,-1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,3,3,2,2,2,2,2,1,1,-7,-7,-6,-6,-3,5,-4,-1,-1,-1,-1,-1,-7,-7},
            {1,1,13,-5,-5,-5,-5,3,3,3,3,3,3,3,3,3,3,2,2,2,2,2,2,2,2,6,7,7,7,8,1,1,-6,-6,-6,-6,-3,5,-4,-5,-5,-5,-5,-5,-6,-6},
            {1,1,13,-2,-2,-2,-2,3,3,3,3,3,3,3,3,3,3,2,2,2,2,2,2,2,2,13,4,4,4,9,1,1,-7,-7,-6,-6,-3,5,-4,-2,-2,-2,-2,-2,-7,-7},
            {1,1,13,4,4,4,9,3,3,3,3,3,3,3,3,3,3,2,2,2,2,2,2,2,2,13,4,4,4,9,1,1,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6},
            {1,1,12,11,11,11,10,3,3,3,3,3,3,3,3,3,3,2,2,2,2,2,2,2,2,12,11,11,11,10,1,1,-7,-7,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-7,-7},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7}
        };
        private int tileSize = 32;
        int characterSize = 32; // 캐릭터 크기

        public Map()
        {
            InitializeComponent();
            init();

        }
        private void init()
        {
            // this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;      
            this.StartPosition = FormStartPosition.CenterScreen;    
            this.ClientSize = new Size(1280, 800);                

            this.Paint += new PaintEventHandler(DrawMap);
            this.DoubleBuffered = true;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(map.GetLength(1) * tileSize, map.GetLength(0) * tileSize);
            this.KeyDown += Map_KeyDown;
            this.KeyUp += Map_KeyUp;
            this.Shown += Map_Shown;
            LoadTileImage();

        }

        private void LoadCharacterFrames()
        {
            upFrames.AddRange(new[] {
                Properties.Resources.pang1_back_1, // front와 back 위치 바꿈
                Properties.Resources.pang1_back_2,
                Properties.Resources.pang1_back_3,
                Properties.Resources.pang1_back_4
            });
            downFrames.AddRange(new[] {
                Properties.Resources.pang1_front_1,
                Properties.Resources.pang1_front_2,
                Properties.Resources.pang1_front_3,
                Properties.Resources.pang1_front_4
            });
            leftFrames.AddRange(new[] {
                Properties.Resources.pang1_left_1,
                Properties.Resources.pang1_left_2,
                Properties.Resources.pang1_left_3,
                Properties.Resources.pang1_left_4
            });
            rightFrames.AddRange(new[] {
                Properties.Resources.pang1_right_1,
                Properties.Resources.pang1_right_2,
                Properties.Resources.pang1_right_3,
                Properties.Resources.pang1_right_4
            });
        }

        private void LoadTileImage()
        {
            dict[-16] = Properties.Resources.otile; dict[-15] = Properties.Resources.otile8; dict[-14] = Properties.Resources.otile7; dict[-13] = Properties.Resources.otile6;
            dict[-12] = Properties.Resources.otile5; dict[-11] = Properties.Resources.otile4; dict[-10] = Properties.Resources.otile3; dict[-9] = Properties.Resources.otile2;
            dict[-8] = Properties.Resources.otile1; dict[-7] = Properties.Resources.stone; dict[-6] = Properties.Resources.ocean; dict[-5] = Properties.Resources.bridge2;
            dict[-4] = Properties.Resources.bridge6; dict[-3] = Properties.Resources.bridge4; dict[-2] = Properties.Resources.bridge3; dict[-1] = Properties.Resources.bridge1;
            dict[1] = Properties.Resources.tree; dict[2] = Properties.Resources.tile; dict[3] = Properties.Resources.grass; dict[4] = Properties.Resources.water;
            dict[5] = Properties.Resources.bridge5; dict[6] = Properties.Resources.water1; dict[7] = Properties.Resources.water2; dict[8] = Properties.Resources.water3;
            dict[9] = Properties.Resources.water4; dict[10] = Properties.Resources.water5; dict[11] = Properties.Resources.water6; dict[12] = Properties.Resources.water7;
            dict[13] = Properties.Resources.water8;

        }

        private void OnPacketReceived(byte[] body)
        {
            if (InvokeRequired)
            {
                var pt = (PacketType)body[0];
                System.Diagnostics.Debug.WriteLine($"[Client] OnPacketReceived: {pt}");
                this.BeginInvoke((MethodInvoker)(() => ProcessPacket(body)));
            }
            else
            {
                ProcessPacket(body);
            }
        }
        private void ProcessPacket(byte[] body)
        {
            switch ((PacketType)body[0])
            {
                case PacketType.welcomeResponse:
                    var welcome = WelcomeResponsePacket.FromBytes(body);
                    foreach (var (id, (tag, px, py)) in welcome.Entries)
                    {
                        AddOrUpdateCharacter(tag, (int)px, (int)py, tag == AppState.CurrentUserId);
                    }
                    break;
                case PacketType.move:
                    var mv = MovePacket.FromBytes(body);
                    if (mv.playerId != AppState.CurrentUserId)
                    {
                        AddOrUpdateCharacter(mv.playerId, (int)mv.x, (int)mv.y, mv.playerId == AppState.CurrentUserId);
                    }
                    break;
                default:
                    break;
            }
        }

        private async void Map_Shown(object sender, EventArgs e)
        {
            AppState.Connection.PacketReceived += OnPacketReceived;
            var req = new WelcomeRequestPacket();
            var buf = req.ToBytes();
            await AppState.Connection.Stream.WriteAsync(buf, 0, buf.Length);
        }

        private void Map_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.A ||
               e.KeyCode == Keys.S || e.KeyCode == Keys.D)
            {
                if (!pressedKeys.Contains(e.KeyCode))
                    pressedKeys.Add(e.KeyCode);
            }
        }

        private void Map_KeyUp(object sender, KeyEventArgs e)
        {
            pressedKeys.Remove(e.KeyCode);
        }
        private void Map_Load(object sender, EventArgs e)
        {
            LoadCharacterFrames();
            frames = downFrames; // 기본 방향

            animationTimer.Interval = 16; // 밀리초 단위: 100ms마다 프레임 변경
            animationTimer.Tick += AnimateCharacter;
            animationTimer.Start();

        }

        private void DrawTransparentImage(Graphics g, Image image, Rectangle destRect, float alpha)
        {
            using (ImageAttributes attributes = new ImageAttributes())
            {
                ColorMatrix matrix = new ColorMatrix
                {
                    Matrix33 = alpha // 0.0 (완전 투명) ~ 1.0 (완전 불투명)
                };
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                g.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            }
        }

        private void DrawMap(object sender, PaintEventArgs e)
        {
            Point offset = this.AutoScrollPosition;
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);
            int viewW = this.ClientSize.Width;
            int viewH = this.ClientSize.Height;

            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    int tile = map[y, x];
                    if (dict.ContainsKey(tile))
                    {
                        int drawX = x * tileSize + offset.X;
                        int drawY = y * tileSize + offset.Y;
                        e.Graphics.DrawImage(dict[tile], drawX, drawY, tileSize, tileSize);
                    }
                }
            }

            // 오른쪽 여백 채움
            int blankStart = cols;
            int blankEnd = (viewW - offset.X + tileSize - 1) / tileSize;
            for (int y = 0; y < rows; y++)
            {
                for (int x = blankStart; x < blankEnd; x++)
                {
                    bool isEdge = (y == 0)
                               || (y == rows - 1)
                               || (x == blankStart)
                               || (x == blankEnd - 1);

                    int tileID = isEdge ? 1  // tree
                                        : 2; // grass

                    e.Graphics.DrawImage(
                        dict[tileID],
                        x * tileSize + offset.X,
                        y * tileSize + offset.Y,
                        tileSize, tileSize);
                }
            }

            foreach (var kvp in characterPositions)
            {
                int playerId = kvp.Key;
                Point worldPos = kvp.Value;
                int drawX = worldPos.X + offset.X;
                int drawY = worldPos.Y + offset.Y;

                // 중심 기준 타일 좌표 계산
                int centerX = worldPos.X + characterSize / 2;
                int centerY = worldPos.Y + characterSize / 2;
                int tileCol = centerX / tileSize;
                int tileRow = centerY / tileSize;

                bool isOnTile3 = (tileRow >= 0 && tileRow < map.GetLength(0) &&
                                  tileCol >= 0 && tileCol < map.GetLength(1) &&
                                  map[tileRow, tileCol] == 3);

                float alpha = isOnTile3 ? 0.4f : 1.0f; // 투명도 설정

                Rectangle destRect = new Rectangle(drawX, drawY, characterSize, characterSize);

                if (playerId == AppState.CurrentUserId && frames.Count > 0)
                {
                    DrawTransparentImage(e.Graphics, frames[frameIndex], destRect, alpha);
                }
                else
                {
                    DrawTransparentImage(e.Graphics, Properties.Resources.pang1_front_1, destRect, alpha);
                }

            }
        }

       

        private void AnimateCharacter(object sender, EventArgs e)
        {
            if (pressedKeys.Count == 0) return;

            int dx = 0, dy = 0;
            frames = downFrames; // 기본 방향

            // 방향별 속도 및 애니메이션 프레임 설정
            if (pressedKeys.Contains(Keys.W)) { dy -= moveSpeed; frames = upFrames; }
            if (pressedKeys.Contains(Keys.S)) { dy += moveSpeed; frames = downFrames; }
            if (pressedKeys.Contains(Keys.A)) { dx -= moveSpeed; frames = leftFrames; }
            if (pressedKeys.Contains(Keys.D)) { dx += moveSpeed; frames = rightFrames; }

            // 대각선 이동 시 속도 보정
            if (dx != 0 && dy != 0)
            {
                double factor = 1 / Math.Sqrt(2);
                dx = (int)(dx * factor);
                dy = (int)(dy * factor);
            }
            // 충돌 감지
            int nextX = playerX + dx;
            int nextY = playerY + dy;

            int hitboxMargin = 6; // 캐릭터 이미지 여유 공간
            Rectangle hitbox = new Rectangle(
                nextX + hitboxMargin,
                nextY + hitboxMargin,
                tileSize - 2 * hitboxMargin,
                tileSize - 2 * hitboxMargin
            );

            bool canMove = true;
            for (int y = hitbox.Top / tileSize; y <= hitbox.Bottom / tileSize; y++)
            {
                for (int x = hitbox.Left / tileSize; x <= hitbox.Right / tileSize; x++)
                {
                    if (y < 0 || y >= map.GetLength(0) || x < 0 || x >= map.GetLength(1))
                    {
                        canMove = false;
                        break;
                    }

                    if (!walkableTiles.Contains(map[y, x]))
                    {
                        canMove = false;
                        break;
                    }
                }
            }
            if (!canMove)
                return;

            // 이동 허용
            playerX = nextX;
            playerY = nextY;

            AddOrUpdateCharacter(AppState.CurrentUserId, playerX, playerY, true);
            SendPlayerPosition(playerX, playerY);
            UpdateCameraPosition();
            this.Invalidate();

            frameIndex = (frameIndex + 1) % frames.Count;

        }

        // 카메라(스크롤) 위치를 캐릭터 중심으로 업데이트
        private void UpdateCameraPosition()
        {
            int centerX = playerX - this.ClientSize.Width / 2 / tileSize * tileSize;
            int centerY = playerY - this.ClientSize.Height / 2 / tileSize * tileSize;

            // 맵 경계 체크
            int maxScrollX = (map.GetLength(1) * tileSize) - this.ClientSize.Width;
            int maxScrollY = (map.GetLength(0) * tileSize) - this.ClientSize.Height;

            centerX = Math.Max(0, Math.Min(centerX, maxScrollX));
            centerY = Math.Max(0, Math.Min(centerY, maxScrollY));

            this.AutoScrollPosition = new Point(centerX, centerY);
        }
        private void AddOrUpdateCharacter(int playerId, int x, int y, bool isLocal = false)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)(() => AddOrUpdateCharacter(playerId, x, y, isLocal)));
                return;
            }

            // 월드 좌표 저장
            characterPositions[playerId] = new Point(x, y);

            
            if (isLocal)
            {
                frameIndex = (frameIndex + 1) % frames.Count;
            }

            
            this.Invalidate();
        }


        private void SendPlayerPosition(int x, int y)
        {
            var data = new MovePacket
            {
                playerId = AppState.CurrentUserId,
                x = x,
                y = y
            }.ToBytes();

            _ = AppState.Connection.Stream.WriteAsync(data, 0, data.Length);
        }

        

    }
}
