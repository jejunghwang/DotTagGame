using Packets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CharacterSelectPacket;

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
        private string initialTaggerName = "unknown";
        private int currentTaggerId = -1;
        private Dictionary<int, Point> characterPositions = new Dictionary<int, Point>();
        private Dictionary<int, int> characterIndices = new Dictionary<int, int>();
        private Dictionary<int, Direction> characterDirections = new Dictionary<int, Direction>();
        private Dictionary<Direction, Image> taggerSprites = new Dictionary<Direction, Image>();
        private HashSet<Keys> pressedKeys = new HashSet<Keys>();
        private Dictionary<int,Image> dict=new Dictionary<int,Image>();

        private readonly HashSet<int> walkableTiles = new HashSet<int> {-16,-15, -14, -13, -12, -11, -10, -9, -8, -5, -4, -3, -2, -1, 2, 3, 5};

        private Label countdownLabel = new Label();
        private Timer countdownTimer = new Timer();
        private bool isCountdownRunning = false;
        private bool hasInitialCountdownRun = false;
        private int remainingSeconds;
        private Point lockedScrollPosition;

        private int[,] map = {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,-7,-7,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-7,-7},
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

        private int[,] bushZones; 
        private int nextZoneId = 1;
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

            countdownLabel.Font = new Font("맑은 고딕", 48, FontStyle.Bold);
            countdownLabel.ForeColor = Color.White;
            countdownLabel.BackColor = Color.FromArgb(128, 0, 0, 0);
            countdownLabel.TextAlign = ContentAlignment.MiddleCenter;
            countdownLabel.Dock = DockStyle.None;
            countdownLabel.AutoSize = false;
            countdownLabel.Visible = false;
            this.Controls.Add(countdownLabel);

            this.Scroll += (s, e) => UpdateCountdownLabelPosition();
            this.Resize += (s, e) => UpdateCountdownLabelPosition();

            countdownTimer.Interval = 1000;
            countdownTimer.Tick += CountdownTimer_Tick;

            taggerSprites[Direction.up] = Properties.Resources.tagger_back;
            taggerSprites[Direction.down] = Properties.Resources.tagger_front;
            taggerSprites[Direction.left] = Properties.Resources.tagger_left;
            taggerSprites[Direction.right] = Properties.Resources.tagger_right;
            AssignBushZones();
        }

        private void UpdateCountdownLabelPosition()
        {
            countdownLabel.Location = new Point(0, 0);
            countdownLabel.Size = this.ClientSize;
        }


        private void LoadCharacterFrames(int charIdx)
        {
            /*upFrames.AddRange(new[] {
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
            });*/
            upFrames.Clear(); downFrames.Clear();
            leftFrames.Clear(); rightFrames.Clear();

            // keyMap: Direction → 리소스 suffix
            var map = new Dictionary<Direction, string>
            {
                [Direction.up] = "back",
                [Direction.down] = "front",
                [Direction.left] = "left",
                [Direction.right] = "right"
            };

            foreach (var kv in map)
            {
                for (int i = 1; i <= 4; i++)
                {
                    string key = $"pang{charIdx}_{kv.Value}_{i}";
                    var img = Properties.Resources.ResourceManager.GetObject(key) as Image;
                    if (img == null) continue;

                    switch (kv.Key)
                    {
                        case Direction.up: upFrames.Add(img); break;
                        case Direction.down: downFrames.Add(img); break;
                        case Direction.left: leftFrames.Add(img); break;
                        case Direction.right: rightFrames.Add(img); break;
                    }
                }
            }
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

        private void AssignBushZones()
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);
            bushZones = new int[rows, cols];
            bool[,] visited = new bool[rows, cols];

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    if (map[y, x] == 3 && !visited[y, x])
                    {
                        MarkZone(x, y, nextZoneId++, visited);
                    }
                }
            }
        }

        private void MarkZone(int startX, int startY, int zoneId, bool[,] visited)
        {
            Queue<Point> queue = new Queue<Point>();
            queue.Enqueue(new Point(startX, startY));
            visited[startY, startX] = true;
            bushZones[startY, startX] = zoneId;

            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            while (queue.Count > 0)
            {
                var p = queue.Dequeue();
                for (int i = 0; i < 4; i++)
                {
                    int nx = p.X + dx[i];
                    int ny = p.Y + dy[i];

                    if (nx >= 0 && ny >= 0 && nx < map.GetLength(1) && ny < map.GetLength(0))
                    {
                        if (map[ny, nx] == 3 && !visited[ny, nx])
                        {
                            visited[ny, nx] = true;
                            bushZones[ny, nx] = zoneId;
                            queue.Enqueue(new Point(nx, ny));
                        }
                    }
                }
            }
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
                    foreach (var (id, (tag, px, py, charIdx)) in welcome.Entries)
                    {
                        AddOrUpdateCharacter(tag, (int)px, (int)py, charIdx, tag == AppState.CurrentUserId);
                    }
                    if (characterIndices.TryGetValue(AppState.CurrentUserId, out var myCi))
                    {
                        LoadCharacterFrames(myCi);
                        frames = downFrames;
                        frameIndex = 0;
                    }

                    break;
                case PacketType.move:
                    var mv = MovePacket.FromBytes(body);
                    if (mv.playerId != AppState.CurrentUserId)
                    {
                        int charIdx = characterIndices.TryGetValue(mv.playerId, out var idx) ? idx : 1;
                        AddOrUpdateCharacter(mv.playerId, (int)mv.x, (int)mv.y, charIdx, mv.playerId == AppState.CurrentUserId);
                    }
                    break;
                case PacketType.changeTagger:
                    var packet = ChangeTaggerPacket.FromBytes(body);
                    currentTaggerId = packet.playerTag;
                    /*foreach(var player in Players.players)
                    {
                        if (player != null && player.isTagger)
                        {
                            player.isTagger = false;
                        }
                    }*/
                    foreach (var p in Players.players.Where(p => p != null && p.isTagger))
                        p.SetTagger(false);
                    Players.players[packet.playerTag].isTagger = true;

                    initialTaggerName = Players.players[packet.playerTag].Name;
                    if (!isCountdownRunning && !hasInitialCountdownRun)
                    {
                        StartCountdown(5);
                        hasInitialCountdownRun = true;
                    }
                    this.Invalidate();
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

            var readyPkt = new ReadyPacket { playerTag = AppState.CurrentUserId };
            await AppState.Connection.Stream.WriteAsync(readyPkt.ToBytes(), 0, readyPkt.ToBytes().Length);
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
            int ci = characterIndices.TryGetValue(AppState.CurrentUserId, out var idx) ? idx : 1;
            LoadCharacterFrames(ci);
            frames = downFrames; // 기본 방향

            animationTimer.Interval = 16; // 밀리초 단위: 100ms마다 프레임 변경
            animationTimer.Tick += AnimateCharacter;
            animationTimer.Stop();

            UpdateCameraPosition();

            lockedScrollPosition = new Point(-AutoScrollPosition.X, -AutoScrollPosition.Y);
            this.Scroll += Map_Scroll;
            //StartCountdown(5);
        }

        private void Map_Scroll(object sender, ScrollEventArgs e)
        {
            if (isCountdownRunning)
            {
                // 잠금된 위치로 즉시 되돌림
                this.AutoScrollPosition = lockedScrollPosition;
            }
            // 라벨이 Dock=Fill 이라도, 위치 재계산 하려면 호출
            UpdateCountdownLabelPosition();
        }


        private void StartCountdown(int seconds)
        {
            remainingSeconds = seconds;
            isCountdownRunning = true;
            countdownLabel.Text = $"술래: {initialTaggerName}\n{remainingSeconds}";
            countdownLabel.Visible = true;
            UpdateCountdownLabelPosition();
            remainingSeconds--;
            countdownTimer.Start();
        }
        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (remainingSeconds > 0)
            {
                countdownLabel.Text = $"술래: {initialTaggerName}\n{remainingSeconds}";
                remainingSeconds--;
            }
            else
            {
                countdownTimer.Stop();
                countdownLabel.Text = $"술래: {initialTaggerName}\nSTART!";
                // START! 잠시 보여주고
                Task.Delay(500).ContinueWith(_ =>
                    this.Invoke((Action)(() =>
                    {
                        countdownLabel.Visible = false;
                        isCountdownRunning = false;
                        animationTimer.Start();
                    }))
                );
                hp_handling.Enabled = true;
            }

            // 매 틱마다 화면 중앙에 갱신
            UpdateCountdownLabelPosition();
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

                // 상대 캐릭터의 숲 zone ID
                int targetZone = 0;
                if (tileRow >= 0 && tileRow < map.GetLength(0) &&
                    tileCol >= 0 && tileCol < map.GetLength(1))
                {
                    targetZone = bushZones[tileRow, tileCol];
                }

                // 현재 플레이어의 숲 zone ID
                int myZone = 0;
                if (characterPositions.TryGetValue(AppState.CurrentUserId, out var mePos))
                {
                    int meCol = (mePos.X + characterSize / 2) / tileSize;
                    int meRow = (mePos.Y + characterSize / 2) / tileSize;

                    if (meRow >= 0 && meRow < map.GetLength(0) &&
                        meCol >= 0 && meCol < map.GetLength(1))
                    {
                        myZone = bushZones[meRow, meCol];
                    }
                }

                bool isInBush = targetZone > 0;
                bool meInBush = myZone > 0;

                bool shouldDraw = true;
                float alpha = 1.0f;

                if (isInBush)
                {
                    if (meInBush && myZone == targetZone)
                    {
                        alpha = 0.4f; // 같은 풀숲 zone일 경우 반투명
                    }
                    else
                    {
                        shouldDraw = false; // 다른 zone 또는 숲 밖이면 안 보임
                    }
                }

                if (shouldDraw)
                {
                    Rectangle destRect = new Rectangle(drawX, drawY, characterSize, characterSize);
                    Image spriteToDraw;

                    if (playerId == currentTaggerId)
                    {
                        var dir = characterDirections.TryGetValue(playerId, out var d) ? d : Direction.down;
                        spriteToDraw = taggerSprites[dir];
                    }
                    else if (playerId == AppState.CurrentUserId && frames.Count > 0)
                    {
                        spriteToDraw = frames[frameIndex];
                    }
                    else
                    {
                        int ci = characterIndices.TryGetValue(playerId, out var idx) ? idx : 1;
                        string key = $"pang{ci}_front_1";
                        spriteToDraw = Properties.Resources.ResourceManager.GetObject(key) as Image ?? Properties.Resources.pang1_front_1;
                    }

                    DrawTransparentImage(e.Graphics, spriteToDraw, destRect, alpha);
                }
            }
        }

       

        private void AnimateCharacter(object sender, EventArgs e)
        {
            if (isCountdownRunning)
                return;

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

            int ci = characterIndices.TryGetValue(AppState.CurrentUserId, out var idx) ? idx : 1;

            AddOrUpdateCharacter(AppState.CurrentUserId, playerX, playerY, ci, true);
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
        private void AddOrUpdateCharacter(int playerId, int x, int y, int charIdx, bool isLocal = false)
        {
            if (InvokeRequired)
            {  
                BeginInvoke((MethodInvoker)(() => AddOrUpdateCharacter(playerId, x, y, charIdx, isLocal)));
                return;
            }

            // 이전 위치가 있으면 방향 계산해서 저장
            if (characterPositions.TryGetValue(playerId, out var oldPos))
            {
                int dx = x - oldPos.X;
                int dy = y - oldPos.Y;

                Direction dir = Direction.down;
                if (dx < 0) dir = Direction.left;
                else if (dx > 0) dir = Direction.right;
                else if (dy < 0) dir = Direction.up;
                else if (dy > 0) dir = Direction.down;

                characterDirections[playerId] = dir;
            }
            else if (!characterDirections.ContainsKey(playerId))
            {
                // 첫 등장인 경우 기본 방향 세팅
                characterDirections[playerId] = Direction.down;
            }

            // 월드 좌표 저장
            characterPositions[playerId] = new Point(x, y);
            characterIndices[playerId] = charIdx;

            if (isLocal)
            {
                frameIndex = (frameIndex + 1) % frames.Count;
            }

            
            this.Invalidate();
        }

        private async void hp_handling_Tick(object sender, EventArgs e)
        {
            for(int i=0; i<Players.players.Length; i++)
            {
                var player = Players.players[i];
                if (player != null && player.isTagger)
                {
                    player.HP--;
                    if(player.HP == 0)
                    {
                        byte[] buffer = new DeathPacket { playerTag = i }.ToBytes();
                        await AppState.Connection.Stream.WriteAsync(buffer, 0, buffer.Length);
                    }
                }
            }
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
