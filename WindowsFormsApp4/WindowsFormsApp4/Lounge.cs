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
using System.Xml.Linq;

namespace WindowsFormsApp4
{
    public partial class Lounge : Form
    {
        private Main mainForm;
        private Panel chatBackgroundPanel;

        private int userId;
        // private TcpClient client;
        // private NetworkStream stream;

        //private Dictionary<int, PictureBox> player = new Dictionary<int, PictureBox>();
        private int playerX = 937, playerY = 270;
        private int moveSpeed = 7;

        // 캐릭터 애니메이션 이미지 (방향별)
       /* private List<Image> upFrames = new List<Image>();
        private List<Image> downFrames = new List<Image>();
        private List<Image> leftFrames = new List<Image>();
        private List<Image> rightFrames = new List<Image>();
        private int frameIndex = 0;
        private List<Image> frames;*/

        
        private HashSet<Keys> pressedKeys = new HashSet<Keys>();

        private Panel overlayPanel;

        private System.Media.SoundPlayer loungeBgmPlayer;
        private System.Media.SoundPlayer buttonSoundPlayer;
        private bool isBgmPlaying = false;
        private Label transitionLabel;

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
            this.Load += Lounge_Load;
            this.Shown += Lounge_Shown;

            overlayPanel = new Panel();
            overlayPanel.Dock = DockStyle.Fill;
            overlayPanel.BackColor = Color.FromArgb(150, 0, 0, 0); // 반투명 검정
            overlayPanel.Visible = false;
            overlayPanel.BringToFront();

            this.Controls.Add(overlayPanel);

            transitionLabel = new Label
            {
                AutoSize = true,
                Font = new Font("맑은 고딕", 24, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };
            overlayPanel.Controls.Add(transitionLabel);
            transitionLabel.Location = new Point((overlayPanel.Width - transitionLabel.Width) / 2, (overlayPanel.Height - transitionLabel.Height) / 2
);
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
                    //foreach (var (pid, x, y) in welcome.Entries)
                    //AddOrUpdateCharacter(pid, (int)x, (int)y, pid == AppState.CurrentUserId);
                    AddCharacter(welcome);
                    break;
                case PacketType.move:
                    var mv = MovePacket.FromBytes(body);
                    if(mv.playerId != AppState.CurrentUserId)
                        UpdateCharacter(mv.playerId, (int)mv.x, (int)mv.y, mv.playerId == AppState.CurrentUserId);
                    break;

                case PacketType.chat:
                    var chat = ChatPacket.FromBytes(body);
                    AppendChatLog($"[{Players.players[chat.playerId].Name}]: {chat.message}");
                    break;
                case PacketType.disconnect:
                    var disconnection = DisconnectPacket.FromBytes(body);
                    var d = Players.players[disconnection.playerTag];
                    if(d != null)
                    {
                        this.Controls.Remove(Players.players[disconnection.playerTag].Pbox);
                        this.Controls.Remove(Players.players[disconnection.playerTag].NameLabel);
                        this.Controls.Remove(Players.players[disconnection.playerTag].BubbleBox);
                        Players.players[disconnection.playerTag].Pbox.Dispose();
                        Players.players[disconnection.playerTag].NameLabel.Dispose();
                        Players.players[disconnection.playerTag].BubbleBox.Dispose();
                        Players.players[disconnection.playerTag] = null;
                        AppendChatLog($"[시스템] {d.Name}님이 퇴장하였습니다");
                    }
                    break;                  
                    
                case PacketType.ready:
                    var ready = ReadyPacket.FromBytes(body);
                    var c = Players.players[ready.playerTag];
                    c.isReady = !c.isReady;
                    c.BubbleBox.Visible = c.isReady;
                    c.SetPosition(c.X, c.Y);
                    break;
                case PacketType.start:
                    ShowTransitionAsync("술래 준비 중...", 3000).ContinueWith(_ =>
                    {
                        this.BeginInvoke(new Action(() => {
                            this.Hide();
                            foreach (var p in Players.players.Where(p => p != null))
                            {
                                p.isReady = false;
                                p.BubbleBox.Visible = false;
                            }
                            btn_ready.Text = "준비";
                            btn_ready.ForeColor = Color.White;
                            
                            AppState.Connection.PacketReceived -= OnPacketReceived;
                            using (var gameForm = new Map())
                            {
                                loungeBgmPlayer?.Stop();
                                gameForm.Owner = this;
                                gameForm.FormClosed += (s, e) => {
                                    this.Show();
                                    AppState.Connection.PacketReceived += OnPacketReceived;
                                };
                                gameForm.ShowDialog();
                            }
                        }));
                    });
                    break;
                case PacketType.characterSelect:
                    var sel = CharacterSelectPacket.FromBytes(body);
                    Players.players[sel.playerTag]?.SetCharacter(sel.characterIndex);
                    break;
                default:
                    break;
            }
        }

        private async void Lounge_Shown(object sender, EventArgs e)
        {
            foreach (var c in Players.players)
            {
                if (c != null)
                {
                    this.Controls.Remove(c.Pbox);
                    this.Controls.Remove(c.NameLabel);
                    this.Controls.Remove(c.BubbleBox);
                }
            }
            Array.Clear(Players.players, 0, Players.players.Length);

            AppState.Connection.PacketReceived += OnPacketReceived;
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

            loungeBgmPlayer = new System.Media.SoundPlayer(Properties.Resources.lounge_bgm);
            loungeBgmPlayer.PlayLooping();
            isBgmPlaying = true;

            //LoadCharacterFrames();
            //frames = downFrames; // 기본 방향
            //UpdateCharacter(AppState.CurrentUserId, playerX, playerY, true);
            /*            var req = new WelcomeRequestPacket();
                        var buf = req.ToBytes();
                        AppState.Connection.Stream.Write(buf, 0, buf.Length);*/

            inputBox.TabStop = false; // 처음에 채팅 입력 박스 포커싱 비활성화


            animationTimer.Interval = 16; // 밀리초 단위: 100ms마다 프레임 변경
            animationTimer.Tick += AnimateCharacter;
            animationTimer.Start();

            
            //btn_start.Enabled = false;
        }

 /*       private void LoadCharacterFrames()
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
        }*/

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
            //frames = downFrames; // 기본

            if (pressedKeys.Contains(Keys.W)) { dy = -1; }
            if (pressedKeys.Contains(Keys.S)) { dy = 1; }
            if (pressedKeys.Contains(Keys.A)) { dx = -1; }
            if (pressedKeys.Contains(Keys.D)) { dx = 1; }

            if (dx == 0 && dy == 0) return; // 눌린 키가 없으면 return

            var me = Players.players[AppState.CurrentUserId];
            me.Move(dx, dy);
            SendPlayerPosition(me.X, me.Y);
            //me.SetPosition(me.X, me.Y);

            // UpdateCharacter(AppState.CurrentUserId, dx, dy, true);

            //if (player.TryGetValue(AppState.CurrentUserId, out var pic))
         /*   if (Players.players[AppState.CurrentUserId] != null)
            {
                frameIndex = (frameIndex + 1) % frames.Count;
                Players.players[AppState.CurrentUserId].Pbox.Image = frames[frameIndex];
            }*/
        }

        private void AddCharacter(WelcomeResponsePacket packet)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)(() => AddCharacter(packet)));
                return;
            }

            foreach (var (playerId, (playerTag, x, y, charIdx)) in packet.Entries)
            {
                bool isNew = (Players.players[playerTag] == null);

                if (isNew)
                {
                    Players.add_player(playerTag, playerId, charIdx);
                    this.Controls.Add(Players.players[playerTag].Pbox);
                    this.Controls.Add(Players.players[playerTag].NameLabel);
                    this.Controls.Add(Players.players[playerTag].BubbleBox);
                  
                    AppendChatLog($"[시스템] {Players.players[playerTag].Name}님이 입장하였습니다");
                }
                else
                {
                    Players.players[playerTag].SetCharacter(charIdx);
                }
                    Players.players[playerTag].SetPosition(x, y);

               /* if (playerTag == AppState.CurrentUserId)
                {
                    frameIndex = (frameIndex + 1) % frames.Count;
                    Players.players[playerTag].Pbox.Image = frames[frameIndex];
                }*/
            }
        }

        private void UpdateCharacter(int playerTag, int x, int y, bool isLocal = false)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)(() => UpdateCharacter(playerTag, x, y, isLocal)));
                return;
            }
            var ch = Players.players[playerTag];
            if (ch == null)
            {
                Debug.WriteLine($"[UpdateCharacter] Unknown playerTag: {playerTag}");
                return; // 널이면 무시
            }
            //if (!player.ContainsKey(playerTag))
            //{
            //var newPlayer = new Character(playerId);
            //Players.add_player(playerTag, playerId);
            /*
            var pic = new PictureBox {
                Size = new Size(100, 100),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Properties.Resources.pang1_front_1,
                Location = new Point(x, y),
                BackColor = Color.Transparent
            };
            player[playerTag] = pic;
            this.Controls.Add(pic);  
            */
            //this.Controls.Add(Players.players[playerTag].Pbox);
            //}
            //else
            //{
            //player[playerTag].Location = new Point(x, y);
            // Players.players[playerTag].Move(x, y);
            //}

            if (isLocal)
            {
                ch.Move(x - ch.X, y - ch.Y);
                /*//var pic = player[playerTag];
                //frameIndex = (frameIndex + 1) % frames.Count;
                //pic.Image = frames[frameIndex];
                frameIndex = (frameIndex + 1) % frames.Count;
                Players.players[playerTag].Pbox.Image = frames[frameIndex];*/
                //return;
            }

            //Players.players[playerTag].SetPosition(x, y);
            //ch.SetPosition(x, y);
            else
            {
                ch.MoveAbsolute(x, y);
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

        /*private void btn_start_Click(object sender, EventArgs e)
        {


        }*/

        // --------------------------------------------------

        private void Lounge_FormClosed(object sender, FormClosedEventArgs e)
        {
            AppState.Connection.PacketReceived -= OnPacketReceived;

            byte[] disconnection = new DisconnectPacket { playerTag = AppState.CurrentUserId }.ToBytes();
            AppState.Connection.Stream.Write(disconnection, 0, disconnection.Length);

            AppState.Connection.Stream.Close();

            loungeBgmPlayer?.Stop();
        }

        private void btn_ready_Click(object sender, EventArgs e)
        {
            loungeBgmPlayer?.Stop();
            isBgmPlaying = false;
            PlayButtonSound();

            if (Players.players[AppState.CurrentUserId].isReady)
            {
                btn_ready.Text = "준비";
                btn_ready.ForeColor = Color.White;
            }
            else
            {
                btn_ready.Text = "준비 완료";
                btn_ready.ForeColor = Color.Crimson;
            }
                
            try
            {
                var packet = new ReadyPacket
                {
                    playerTag = AppState.CurrentUserId
                };

                byte[] data = packet.ToBytes();
                AppState.Connection.Stream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"전송 오류: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_select_Click(object sender, EventArgs e)
        {
            loungeBgmPlayer?.Stop();
            isBgmPlaying = false;
            PlayButtonSound();

            animationTimer.Stop();
            pressedKeys.Clear();
            overlayPanel.Visible = true;
            overlayPanel.BringToFront();

            using (var pick = new Pick())
            {
                pick.Owner = this;
                pick.StartPosition = FormStartPosition.CenterParent;
                if (pick.ShowDialog() == DialogResult.OK)
                {
                    int idx = pick.SelectedCharacter;
                    // ① 서버로 전송
                    var packet = new CharacterSelectPacket
                    {
                        playerTag = AppState.CurrentUserId,
                        characterIndex = idx
                    };
                    //AppState.Connection.Stream.Write(packet.ToBytes(), 0, packet.ToBytes().Length);
                    await AppState.Connection.Stream.WriteAsync(packet.ToBytes(), 0, packet.ToBytes().Length);

                    // 로컬 즉시 반영
                    ApplyCharacterSelection(AppState.CurrentUserId, idx);
                }
            }
            overlayPanel.Visible = false;
            pressedKeys.Clear();
            animationTimer.Start();

            loungeBgmPlayer?.PlayLooping();
            isBgmPlaying = true;
        }

        public void ApplyCharacterSelection(int playerTag, int newIdx)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)(() => ApplyCharacterSelection(playerTag, newIdx)));
                return;
            }

            var ch = Players.players[playerTag];
            if (ch != null)
                ch.SetCharacter(newIdx);
        }

        private async void PlayButtonSound()
        {
            var stream = new MemoryStream();
            Properties.Resources.buttonClick.CopyTo(stream);
            stream.Position = 0;

            var clickSound = new System.Media.SoundPlayer(stream);
            clickSound.Play();

            await Task.Delay(500);

            stream.Dispose();
        }

        private void btn_setting_Click(object sender, EventArgs e)
        {
            PlayButtonSound();

            animationTimer.Stop();
            pressedKeys.Clear();
            overlayPanel.Visible = true;
            overlayPanel.BringToFront();

            using (var set = new Setting(isBgmPlaying))
            {
                set.Owner = this;  // Owner가 Lounge임을 알려주고
                set.StartPosition = FormStartPosition.CenterParent;
                set.ShowDialog();
            }

            overlayPanel.Visible = false;
            pressedKeys.Clear();
            animationTimer.Start();

        }

        private Task ShowTransitionAsync(string message, int delayMs)
        {
            int dotCount = 0;
            var dotTimer = new System.Windows.Forms.Timer { Interval = 300 };
            dotTimer.Tick += (s, e) =>
            {
                dotCount = (dotCount + 1) % 4;  // 0,1,2,3
                string dots = new string('.', dotCount);
                this.BeginInvoke(new Action(() =>
                {
                    transitionLabel.Text = message + dots;
                    int cw = this.ClientSize.Width;
                    int ch = this.ClientSize.Height;
                    int lw = transitionLabel.PreferredWidth;
                    int lh = transitionLabel.PreferredHeight;

                    transitionLabel.Location = new Point(
                        (cw - lw) / 2,
                        (ch - lh) / 2
                    );
                }));
            };

            this.BeginInvoke(new Action(() =>
            {
                overlayPanel.BringToFront();
                transitionLabel.BringToFront();

                transitionLabel.Text = message;
                overlayPanel.Visible = true;
                transitionLabel.Visible = true;
                int cw = this.ClientSize.Width;
                int ch = this.ClientSize.Height;
                int lw = transitionLabel.PreferredWidth;
                int lh = transitionLabel.PreferredHeight;

                transitionLabel.Location = new Point(
                    (cw - lw) / 2,
                    (ch - lh) / 2
                );
                dotTimer.Start();
            }));

            return Task.Delay(delayMs).ContinueWith(_ =>
            {
                dotTimer.Stop();
                dotTimer.Dispose();

                this.BeginInvoke(new Action(() =>
                {
                    overlayPanel.Visible = false;
                    transitionLabel.Visible = false;
                }));
            });
        }

        public void ToggleBgm(bool play)
        {
            if (play)
                loungeBgmPlayer?.PlayLooping();
            else
                loungeBgmPlayer?.Stop();
            isBgmPlaying = play;
        }
    }
}