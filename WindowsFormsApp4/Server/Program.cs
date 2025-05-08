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
            RunServerAsync().GetAwaiter().GetResult();
        }

        static async Task RunServerAsync()
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
                    TcpClient client = await server.AcceptTcpClientAsync();
                    Console.WriteLine("Connected!");

                    _ = Task.Run(async () =>
                    {
                        byte[] buffer = new byte[4];
                        NetworkStream stream = client.GetStream();
                        await stream.ReadAsync(buffer, 0, 4);


                        byte[] readBuffer = new byte[100];
                        stream.Read(readBuffer, 0, readBuffer.Length);
                    });

                }
            }
            catch (SocketException e)
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
