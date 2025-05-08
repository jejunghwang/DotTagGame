using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using Packets;

namespace WindowsFormsApp4
{
    public partial class Resister : Form
    {
        public Resister()
        {
            InitializeComponent();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_enter_Click(object sender, EventArgs e)
        {
            string userId = txtNewId.Text.Trim();
            string password = txtNewPw.Text;

            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 9999);
                NetworkStream stream = client.GetStream();

                var loginPacket = new Packets.LoginRequestPacket
                {
                    id = userId,
                    pw = password
                };

                byte[] buffer = loginPacket.ToBytes();
                stream.Write(buffer, 0, buffer.Length);

                MessageBox.Show("회원가입 요청이 전송되었습니다!");

                stream.Close();
                client.Close();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("서버 연결 실패: " + ex.Message);
            }
        }
    }
}
