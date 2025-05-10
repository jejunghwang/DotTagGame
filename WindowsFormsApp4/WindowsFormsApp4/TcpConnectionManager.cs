using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4
{
    public class TcpConnectionManager
    { 
        public TcpClient Client { get; private set; }
        public NetworkStream Stream => Client?.GetStream();

        public void Connect(string ip, int port)
        {
            if (Client != null && Client.Connected)
                return; // 이미 연결되어 있음

            Client = new TcpClient();
            Client.Connect(ip, port);
        }

        public void Close()
        {
            Stream?.Close();
            Client?.Close();
        }
    }
}
