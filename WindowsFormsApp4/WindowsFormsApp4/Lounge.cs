using Guna.UI2.WinForms;
using Packets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        private Thread receiveThread;

        private Dictionary<int, PictureBox> player = new Dictionary<int, PictureBox>();
        private int playerX = 937, playerY = 270;
        private int moveSpeed = 7;

        // 캐릭터 애니메이션 이미지 (방향별)
        private List<Image> upFrames = new List<Image>();
        private List<Image> downFrames = new List<Image>();
        private List<Image> leftFrames = new List<Image>();
        private List<Image> rightFrames = new List<Image>();
        private int frameIndex = 0;



        private HashSet<Keys> pressedKeys = new HashSet<Keys>();


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
            chatLogBox.BackColor = Color.Black; // 실제 배경은 안 보이지만 대비를 위해
            chatLogBox.ForeColor = Color.White;
            chatLogBox.BorderStyle = BorderStyle.None;
            chatLogBox.Font = new Font("맑은 고딕", 9);
            // chatLogBox.Size = new Size(380, 300);
            chatLogBox.ScrollBars = RichTextBoxScrollBars.Vertical;

            // 입력 박스
            inputBox.PlaceholderText = "메시지를 입력하세요...";
            inputBox.Font = new Font("맑은 고딕", 9);
            // inputBox.Size = new Size(380, 40);
            inputBox.BorderThickness = 0;
            inputBox.FillColor = Color.FromArgb(30, 30, 30);
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

            AppState.Connection.PacketReceived += OnPacketReceived;
        }

        // ① PacketReceived 이벤트 핸들러
        private void OnPacketReceived(byte[] body)
        {
            if (InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate { ProcessPacket(body); });
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
                case PacketType.move:
                    var mv = MovePacket.FromBytes(body);
                    UpdatePlayerPosition(mv.playerId, mv.x, mv.y);
                    break;

                case PacketType.chat:
                    var chat = ChatPacket.FromBytes(body);
                    AppendChatLog($"[{chat.playerId}]: {chat.message}");
                    break;

            }
        }

        private void UpdatePlayerPosition(int playerId, float x, float y)
        {
            AddOrUpdateCharacter(playerId, (int)x, (int)y, playerId == AppState.CurrentUserId);
        }

        private void Lounge_Load(object sender, EventArgs e)
        {
            if (mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.bgm.Stop();   // BGM 종료
                //mainForm.Hide();      // MainForm 숨김
            }

            LoadCharacterFrames();
            AddOrUpdateCharacter(AppState.CurrentUserId, playerX, playerY, true);

            receiveThread = new Thread(ReceiveMessages);
            receiveThread.IsBackground = true;
            receiveThread.Start();

            inputBox.TabStop = false; // 처음에 채팅 입력 박스 포커싱 비활성화


            animationTimer.Interval = 30; // 밀리초 단위: 100ms마다 프레임 변경
            animationTimer.Tick += AnimateCharacter;
            animationTimer.Start();
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

        private void ReceiveMessages()
        {
            try
            {
                var stream = AppState.Connection.Stream;
                byte[] buffer = new byte[1024];     //패킷 읽는 방법 완전히 잘못됨.

                while (AppState.Connection.Client.Connected)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        var receivedPacket = ChatPacket.FromBytes(buffer);
                        string receivedMessage = $"[{receivedPacket.playerId}]: {receivedPacket.message}";
                        AppendChatLog(receivedMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                AppendChatLog($"연결이 끊어졌습니다: {ex.Message}");
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
                e.SuppressKeyPress = true;
                this.ActiveControl = null;
                this.Focus();
            }
        }

        private void Lounge_KeyUp(object sender, KeyEventArgs e)
        {
            pressedKeys.Remove(e.KeyCode);
        }
        private void Lounge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.T) // t를 누르면 채팅창으로 포커스 이동
            {
                inputBox.Focus();
                return;
            }
            if (inputBox.Focused) return;

            // 한 번만 추가
            if (!pressedKeys.Contains(e.KeyCode))
                pressedKeys.Add(e.KeyCode);
        }

        // ---------------------------------------------------------------------


        // ---------------------------- 캐릭터 ---------------------------------
        //서버로부터 나오는 패킷을 받는 부분은 어디?
        private void AnimateCharacter(object sender, EventArgs e)
        {
            if (pressedKeys.Count == 0) return;

            int dx = 0, dy = 0;
            List<Image> frames = downFrames; // 기본 방향

            if (pressedKeys.Contains(Keys.W)) { dy -= moveSpeed; frames = upFrames; }
            if (pressedKeys.Contains(Keys.S)) { dy += moveSpeed; frames = downFrames; }
            if (pressedKeys.Contains(Keys.A)) { dx -= moveSpeed; frames = leftFrames; }
            if (pressedKeys.Contains(Keys.D)) { dx += moveSpeed; frames = rightFrames; }

            // 실제 위치 이동
            playerX += dx;
            playerY += dy;

            AddOrUpdateCharacter(AppState.CurrentUserId, playerX, playerY, true);
            SendPlayerPosition(playerX, playerY);

            // 애니메이션
            if (player.TryGetValue(AppState.CurrentUserId, out var pic))
            {
                frameIndex = (frameIndex + 1) % frames.Count;
                pic.Image = frames[frameIndex];
            }
        }

        private void AddOrUpdateCharacter(int playerId, int x, int y, bool isLocal = false)
        {
            if (!player.ContainsKey(playerId))
            {
                var pic = new PictureBox();
                pic.Focus();
                pic.Size = new Size(100, 100);
                pic.SizeMode = PictureBoxSizeMode.StretchImage;
                pic.Image = Properties.Resources.pang1_front_1; // 고정 캐릭터 이미지
                pic.Location = new Point(x, y);
                pic.BackColor = Color.Transparent;
                player[playerId] = pic;

                this.Invoke(new MethodInvoker(() => this.Controls.Add(pic)));
            }
            else
            {
                this.Invoke(new MethodInvoker(() => player[playerId].Location = new Point(x, y)));
            }
        }

        private void SendPlayerPosition(int x, int y)
        {
            try
            {
                var movePacket = new MovePacket
                {
                    playerId = AppState.CurrentUserId,
                    x = x,
                    y = y
                };
                byte[] data = movePacket.ToBytes();
                AppState.Connection.Stream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                AppendChatLog("위치 전송 실패: " + ex.Message);
            }
        }
        // --------------------------------------------------
    }
}