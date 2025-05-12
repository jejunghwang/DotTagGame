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
        private Main mainForm;

        public Login(Main mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            lbl_register.Text = "<u>아직 계정이 없으신가요?<u>";
        }

        private void Login_Shown(object sender, EventArgs e)
        {
            txtId.Focus();
        }

        private void txtPw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_enter.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_enter_Click(object sender, EventArgs e)
         {
             string userId = txtId.Text.Trim();
             string password = txtPw.Text;

             try
             {
                if (AppState.Connection.Client == null || !AppState.Connection.Client.Connected)
                    AppState.Connection.Connect("127.0.0.1", 9999);

                NetworkStream stream = AppState.Connection.Stream;

                var loginPacket = new Packets.LoginRequestPacket
                 {
                     id = userId,
                     pw = password
                 };

                 byte[] writeBuffer = loginPacket.ToBytes();
                 stream.Write(writeBuffer, 0, writeBuffer.Length);

                 byte[] readBuffer = new byte[4];
                 stream.Read(readBuffer, 0, 4);

                 int packetLength = BitConverter.ToInt32(readBuffer, 0);

                 readBuffer = new byte[packetLength];
                 stream.Read(readBuffer, 0, packetLength);

                 var response = Packets.LoginResponsePacket.FromBytes(readBuffer);

                 // stream.Close();
                 // client.Close();

                 if (response.successLogin)
                 {
                    AppState.CurrentUserId = response.userId;  // int
                    AppState.CurrentUserName = userId;  // string

                    // 로딩 폼 띄우기 
                    this.Hide();
                 
                    Loading load = new Loading(mainForm);
                    load.ShowDialog();
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

        private void lbl_register_Click_1(object sender, EventArgs e)
        {
            Resister register = new Resister();
            register.StartPosition = FormStartPosition.CenterParent;
            register.ShowDialog();
        }

    }
}
