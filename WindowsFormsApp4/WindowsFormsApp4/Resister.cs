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

                var regPacket = new Packets.RegUsrRequestPacket
                {
                    id = userId,
                    pw = password
                };

                byte[] writeBuffer = regPacket.ToBytes();
                stream.Write(writeBuffer, 0, writeBuffer.Length);

                byte[] readBuffer = new byte[4];
                stream.Read(readBuffer, 0, 4);

                int packetLength = BitConverter.ToInt32(readBuffer, 0);
                readBuffer = new byte[packetLength];
                stream.Read(readBuffer, 0, packetLength);

                var response = Packets.RegUsrResponsePacket.FromBytes(readBuffer);
                if (response.successReg)
                    MessageBox.Show("회원가입 성공!");
                else
                    MessageBox.Show("다른 아이디로 시도하세요!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("서버 연결 실패: " + ex.Message);
            }
        }
    }
}
