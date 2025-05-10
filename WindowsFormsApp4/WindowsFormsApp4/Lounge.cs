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
        private string userId;
        private TcpClient client;
        private NetworkStream stream;
        private Thread receiveThread;

        public Lounge(Main mainForm,string id,TcpClient tcp,NetworkStream network)
        {
            InitializeComponent();
            this.mainForm = mainForm;

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
         //   inputBox.Size = new Size(380, 40);
            inputBox.BorderThickness = 0;
            inputBox.FillColor = Color.FromArgb(30, 30, 30);
            inputBox.ForeColor = Color.White;
            inputBox.BorderRadius = 5;
            userId = id;
            client = tcp;
            stream = network;
            receiveThread = new Thread(ReceiveMessages);
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }

        private void Lounge_Load(object sender, EventArgs e)
        {
            if (mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.bgm.Stop();   // BGM 종료
                //mainForm.Hide();      // MainForm 숨김
            }
            

            
        }

        private void SendMessage(string message)
        {
            try
            {

                var packet = new ChatPacket
                {
                    playerId = userId,
                    message = message
                };
                byte[] data = packet.ToBytes();
                AppendChatLog($"[{userId}]: {message}");
                stream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"메시지 전송 오류: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void inputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter&&!string.IsNullOrWhiteSpace(inputBox.Text))
            {
                SendMessage(inputBox.Text);
                inputBox.Clear();
                e.SuppressKeyPress = true;
            }
        }
        private void ReceiveMessages()
        {
            try
            {
                byte[] buffer = new byte[1024];
                while (client.Connected)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        var receivedPacket = new ChatPacket().FromBytes(buffer);
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

        
    }
}
