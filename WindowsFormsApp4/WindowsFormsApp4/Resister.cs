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
using Packets;

namespace WindowsFormsApp4
{
    public partial class Resister : Form
    {
        public Resister()
        {
            InitializeComponent();
            AppState.Connection.PacketReceived += OnPacketReceived;
        }

        private void Resister_Shown(object sender, EventArgs e)
        {
            txtNewId.Focus();
        }

        private void txtNewPw_KeyDown(object sender, KeyEventArgs e)
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
            await AppState.Connection.ConnectAsync("127.0.0.1", 9999);

            var req = new RegUsrRequestPacket { id = txtNewId.Text, pw = txtNewPw.Text };
            var buf = req.ToBytes();
            AppState.Connection.Stream.Write(buf, 0, buf.Length);
        }

        private void OnPacketReceived(byte[] body)
        {
            if ((PacketType)body[0] != PacketType.RegUsrResponse) return;

            var res = RegUsrResponsePacket.FromBytes(body);
            this.Invoke(new MethodInvoker(() => HandleRegResult(res)));
        }

        private void HandleRegResult(RegUsrResponsePacket res)
        {
            if (res.successReg)
            {
                AppState.Connection.PacketReceived -= OnPacketReceived;
                MessageBox.Show("회원가입 성공");
            }
            else
            {
                btn_enter.Enabled = true;
                MessageBox.Show("회원가입 실패");
            }
        }
    }
}
