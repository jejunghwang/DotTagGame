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

        public event Action<byte[]> PacketReceived;
        public void Connect(string ip, int port)
        {
            if (Client != null && Client.Connected)
                return; // 이미 연결되어 있음

            Client = new TcpClient();
            Client.Connect(ip, port);

            maintainConnection();
        }

        private void maintainConnection()
        {
            Task.Run(() =>
            {
                try
                {
                    var stream = Stream;
                    while (Client.Connected)
                    {
                        var header = new byte[4];
                        int read = stream.Read(header, 0, 4);
                        if (read == 0) break;

                        int bodyLen = BitConverter.ToInt32(header, 0);
                        var body = new byte[bodyLen];
                        int offset = 0;
                        while (offset < bodyLen)
                            offset += stream.Read(body, offset, bodyLen - offset);

                        PacketReceived?.Invoke(body);
                    }
                }
                catch
                {
                }
            });
        }
        public void Close()
        {
            Stream?.Close();
            Client?.Close();
        }
    }
}