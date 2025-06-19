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
            AppState.Connection.PacketReceived += OnPacketReceived;
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

        private async void btn_enter_Click(object sender, EventArgs e)
        {
            btn_enter.Enabled = false;
            await AppState.Connection.ConnectAsync("223.194.46.59", 9999);

            var req = new LoginRequestPacket { id = txtId.Text, pw = txtPw.Text };
            var buf = req.ToBytes();
            AppState.Connection.Stream.Write(buf, 0, buf.Length);
        }

        private void OnPacketReceived(byte[] body)
        {
            if ((PacketType)body[0] != PacketType.loginResponse) return;

            var res = LoginResponsePacket.FromBytes(body);
            this.Invoke(new MethodInvoker(() => HandleLoginResult(res)));
        }
       
        private void HandleLoginResult(LoginResponsePacket res)
        {
            if (res.successLogin)
            {
                AppState.Connection.PacketReceived -= OnPacketReceived;
                AppState.CurrentUserId = res.userId;
                AppState.CurrentUserName = txtId.Text.Trim();
                this.Hide();
                new Loading(mainForm).ShowDialog();
            }
            else
            {
                btn_enter.Enabled = true;
                MessageBox.Show("로그인 실패");
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