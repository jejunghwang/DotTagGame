using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using Guna.UI2.WinForms;
//using Packets;
namespace WindowsFormsApp4
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtId.Focus();
        }

        private void txtPw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string userId = txtId.Text.Trim();
                string password = txtPw.Text;

                /*try
                {
                    TcpClient client = new TcpClient("127.0.0.1", 9000); 
                    NetworkStream stream = client.GetStream();

                    // 로그인 요청 패킷 생성
                    var loginPacket = new Packets.LoginRequestPacket
                    {
                        id = userId,
                        pw = password
                    };

                    byte[] buffer = loginPacket.ToBytes();
                    stream.Write(buffer, 0, buffer.Length);

                    // 응답 수신
                    byte[] recv = new byte[2]; // loginResponse는 2바이트
                    stream.Read(recv, 0, recv.Length);

                    var response = Packets.LoginResponsePacket.FromBytes(recv);

                    if (response.successLogin)
                    {
                        MessageBox.Show("로그인 성공!");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("로그인 실패");
                    }

                    stream.Close();
                    client.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("서버 연결 오류: " + ex.Message);
                }*/
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
