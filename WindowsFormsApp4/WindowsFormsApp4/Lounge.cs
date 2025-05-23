using Guna.UI2.WinForms;
using Packets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Lounge : Form
    {
        private Main mainForm;
        private Panel chatBackgroundPanel;

        private int userId;
        // private TcpClient client;
        // private NetworkStream stream;

        private Dictionary<int, PictureBox> player = new Dictionary<int, PictureBox>();
        private int playerX = 937, playerY = 270;
        private int moveSpeed = 7;

        // 캐릭터 애니메이션 이미지 (방향별)
        private List<Image> upFrames = new List<Image>();
        private List<Image> downFrames = new List<Image>();
        private List<Image> leftFrames = new List<Image>();
        private List<Image> rightFrames = new List<Image>();
        private int frameIndex = 0;
        private List<Image> frames;

        
        private HashSet<Keys> pressedKeys = new HashSet<Keys>();

        private Panel overlayPanel;
        public Lounge(Main mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.KeyPreview = true;
            this.KeyDown += Lounge_KeyDown;

            // 배경 패널 (반투명 효과)
            chatBackgroundPanel = new Panel();
            chatBackgroundPanel.Size = new Size(400, 200);
            chatBackgroundPanel.Location = new Point(this.ClientSize.Width - 430, this.ClientSize.Height - 230);
            chatBackgroundPanel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            chatBackgroundPanel.BackColor = Color.Transparent;
            chatBackgroundPanel.Paint += (s, e) =>
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
                {
                    e.Graphics.FillRectangle(brush, chatBackgroundPanel.ClientRectangle);
                }
            };

            // 채팅 로그 RichTextBox
            chatLogBox.ReadOnly = true;
            chatLogBox.BorderStyle = BorderStyle.None;
            chatLogBox.Font = new Font("맑은 고딕", 9);
            // chatLogBox.Size = new Size(380, 300);
            chatLogBox.ScrollBars = RichTextBoxScrollBars.Vertical;

            // 입력 박스
            inputBox.PlaceholderText = "메시지를 입력하세요...";
            inputBox.Font = new Font("맑은 고딕", 9);
            // inputBox.Size = new Size(380, 40);
            inputBox.BorderThickness = 0;
            // inputBox.FillColor = Color.FromArgb(30, 30, 30);
            inputBox.ForeColor = Color.White;
            inputBox.BorderRadius = 5;

            this.KeyUp += Lounge_KeyUp;
            this.DoubleBuffered = true;
            /* userId = id;
            client = tcp;
            stream = network;
            receiveThread = new Thread(ReceiveMessages);
            receiveThread.IsBackground = true;
            receiveThread.Start();*/
            this.Load += Lounge_Load;
            //AppState.Connection.PacketReceived += OnPacketReceived;
            this.Shown += Lounge_Shown;
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
                    foreach (var (pid, x, y) in welcome.Entries)
                        AddOrUpdateCharacter(pid, (int)x, (int)y, pid == AppState.CurrentUserId);
                    break;
                case PacketType.move:
                    var mv = MovePacket.FromBytes(body);
                    if(mv.playerId != AppState.CurrentUserId)
                        AddOrUpdateCharacter(mv.playerId, (int)mv.x, (int)mv.y, mv.playerId == AppState.CurrentUserId);
                    break;

                case PacketType.chat:
                    var chat = ChatPacket.FromBytes(body);
                    AppendChatLog($"[{chat.playerId}]: {chat.message}");
                    break;
                case PacketType.disconnect:
                    var disconnection = DisconnectPacket.FromBytes(body);
                    MessageBox.Show($"{disconnection.playerTag} disconnected");
                    break;
                default:
                    break;
            }
        }


        private async void Lounge_Shown(object sender, EventArgs e)
        {
            AppState.Connection.PacketReceived+=OnPacketReceived;
            var req = new WelcomeRequestPacket();
            var buf = req.ToBytes();
            await AppState.Connection.Stream.WriteAsync(buf, 0, buf.Length);
            this.ActiveControl = null; // 실행 시 캐릭터 안움직임 해결
            this.Focus();

        }
        private void Lounge_Load(object sender, EventArgs e)
        {
            if (mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.bgm.Stop();   // BGM 종료
                //mainForm.Hide();      // MainForm 숨김
            }

            LoadCharacterFrames();
            frames = downFrames; // 기본 방향
            AddOrUpdateCharacter(AppState.CurrentUserId, playerX, playerY, true);
/*            var req = new WelcomeRequestPacket();
            var buf = req.ToBytes();
            AppState.Connection.Stream.Write(buf, 0, buf.Length);*/

            inputBox.TabStop = false; // 처음에 채팅 입력 박스 포커싱 비활성화


            animationTimer.Interval = 16; // 밀리초 단위: 100ms마다 프레임 변경
            animationTimer.Tick += AnimateCharacter;
            animationTimer.Start();

            overlayPanel = new Panel();
            overlayPanel.Dock = DockStyle.Fill;
            overlayPanel.BackColor = Color.FromArgb(150, 0, 0, 0); // 반투명 검정
            overlayPanel.Visible = false;
            overlayPanel.BringToFront();

            this.Controls.Add(overlayPanel);
            //btn_start.Enabled = false;
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

        // -------------------- 채팅 -----------------------
        private void SendMessage(string message)
        {
            try
            {
                var packet = new ChatPacket
                {
                    playerId = AppState.CurrentUserId,
                    message = message
                };

                byte[] data = packet.ToBytes();
                //AppendChatLog($"[{AppState.CurrentUserName}]: {message}"); 이렇게 하면 일관성 깨짐.
                AppState.Connection.Stream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"메시지 전송 오류: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AppendChatLog(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => chatLogBox.AppendText(message + "\n")));
            }
            else
            {
                chatLogBox.AppendText(message + "\n");
            }
        }
        // ----------------------------------------------------------------------

        // --------------------- 채팅창+이동 관련 -------------------------------
        private void inputBox_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(inputBox.Text))
            {
                SendMessage(inputBox.Text);
                inputBox.Clear();
                this.ActiveControl = null;
                this.Focus();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        

        private void Lounge_KeyUp(object sender, KeyEventArgs e)
        {
            pressedKeys.Remove(e.KeyCode);
        }
        private void Lounge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // t를 누르면 채팅창으로 포커스 이동
            {
                inputBox.Focus();
                return;
            }
            if (inputBox.Focused) return;

            // W, A, S, D 키만 추가
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.A ||
                e.KeyCode == Keys.S || e.KeyCode == Keys.D)
            {
                if (!pressedKeys.Contains(e.KeyCode))
                    pressedKeys.Add(e.KeyCode);
            }
        }

        // ---------------------------------------------------------------------


        // ---------------------------- 캐릭터 ---------------------------------
        //서버로부터 나오는 패킷을 받는 부분은 어디?
        private void AnimateCharacter(object sender, EventArgs e)
        {
            if (pressedKeys.Count == 0) return;

            int dx = 0, dy = 0;
            frames = downFrames; // 기본

            if (pressedKeys.Contains(Keys.W)) { dy -= moveSpeed; frames = upFrames; }
            if (pressedKeys.Contains(Keys.S)) { dy += moveSpeed; frames = downFrames; }
            if (pressedKeys.Contains(Keys.A)) { dx -= moveSpeed; frames = leftFrames; }
            if (pressedKeys.Contains(Keys.D)) { dx += moveSpeed; frames = rightFrames; }

            playerX += dx;
            playerY += dy;

            AddOrUpdateCharacter(AppState.CurrentUserId, playerX, playerY, true);

            if (dx != 0 || dy != 0)
                SendPlayerPosition(playerX, playerY);

            if (player.TryGetValue(AppState.CurrentUserId, out var pic))
            {
                frameIndex = (frameIndex + 1) % frames.Count;
                pic.Image = frames[frameIndex];
            }
        }

        private void AddOrUpdateCharacter(int playerId, int x, int y, bool isLocal = false)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)(() => AddOrUpdateCharacter(playerId, x, y, isLocal)));
                return;
            }

            if (!player.ContainsKey(playerId))
            {
                var pic = new PictureBox {
                    Size = new Size(100, 100),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = Properties.Resources.pang1_front_1,
                    Location = new Point(x, y),
                    BackColor = Color.Transparent
                };
                player[playerId] = pic;
                this.Controls.Add(pic);  
            }
            else
            {
                player[playerId].Location = new Point(x, y);
            }

            if (isLocal)
            {
                var pic = player[playerId];
                frameIndex = (frameIndex + 1) % frames.Count;
                pic.Image = frames[frameIndex];
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

        private void btn_start_Click(object sender, EventArgs e)
        {
            AppState.Connection.PacketReceived -= OnPacketReceived; // 먼저 끊어주기
            Map game_start = new Map();
            game_start.Owner = this;
            game_start.FormClosed += (s, k) => {
                this.Show();
                AppState.Connection.PacketReceived += OnPacketReceived; // 다시 연결
            };
            game_start.ShowDialog();

        }

        private void Lounge_FormClosed(object sender, FormClosedEventArgs e)
        {
            byte[] disconnection = new DisconnectPacket { playerTag = AppState.CurrentUserId }.ToBytes();
            AppState.Connection.Stream.Write(disconnection, 0, disconnection.Length);
        }

        // --------------------------------------------------

        private void btn_select_Click(object sender, EventArgs e)
        {
            overlayPanel.Visible = true;
            overlayPanel.BringToFront();

            Pick pick = new Pick();
            pick.Owner = this;
            pick.StartPosition = FormStartPosition.CenterParent;
            pick.ShowDialog();

            overlayPanel.Visible = false;
        }

    }
}