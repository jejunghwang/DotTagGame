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
using Packets;
using System.IO;

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
            lbl_register.Text = "<u>아직 계정이 없으신가요?<u>";
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbl_register_Click(object sender, EventArgs e)
        {
            Resister register = new Resister();
            register.StartPosition = FormStartPosition.CenterParent;
            register.ShowDialog();
        }

        private void btn_enter_Click(object sender, EventArgs e)
         {
             string userId = txtId.Text.Trim();
             string password = txtPw.Text;

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

                 byte[] recv = new byte[6]; 
                 stream.Read(recv, 0, recv.Length);

                 var response = Packets.LoginResponsePacket.FromBytes(recv);

                 stream.Close();
                 client.Close();

                 if (response.successLogin)
                 {
                     // 로딩 폼 띄우기 
                     this.Hide();

                     using (Loading load = new Loading())
                     {
                         load.Show();
                         Application.DoEvents(); // UI 강제 업데이트
                         System.Threading.Thread.Sleep(2000); // 2초 대기
                         load.Close();
                     }

                     this.DialogResult = DialogResult.OK;
                     this.Close(); // 로그인 창 닫기
                 }
                 else
                 {
                     MessageBox.Show("로그인 실패");
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show("서버 연결 오류: " + ex.Message);
             }
         }

      /*  private void btn_enter_Click(object sender, EventArgs e)
        {
            // 로그인 체크 생략하고 바로 로딩 폼 실행
            this.Hide();

            using (Loading load = new Loading())
            {
                load.Show();
                Application.DoEvents(); // UI 강제 렌더링
                System.Threading.Thread.Sleep(5000); // 5초 보여주기
                load.Close();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
*/
    }
}
