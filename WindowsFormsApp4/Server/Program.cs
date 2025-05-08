using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Packets;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = null;
            IPAddress addr = IPAddress.Parse("127.0.0.1");
            int port = 9999;

            try
            {
                server = new TcpListener(addr, port);
                server.Start();

                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");


                }
            }
            catch(SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }

            Console.WriteLine("\n서버가 종료됩니다.");
        }
    }
}
