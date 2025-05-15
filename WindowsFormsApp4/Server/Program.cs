using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Packets;

namespace Server
{
    internal class Program
    {
        private const int MaxUsr = 100;
        private static ConcurrentDictionary<string, int> IpToUserId = new ConcurrentDictionary<string, int>();
        private static ConcurrentDictionary<int, NetworkStream> SessionMap = new ConcurrentDictionary<int, NetworkStream>();
        private static bool[] UsedId = new bool[MaxUsr];


        static async Task Main(string[] args) => await RunAsync();

        private static async Task RunAsync()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 9999);
            listener.Start();
            Console.WriteLine("Server started");

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                _ = HandleClientAsync(client);
            }
        }

        private static async Task HandleClientAsync(TcpClient client)
        {
            string ip = ((IPEndPoint)client.Client.RemoteEndPoint!).Address.ToString();
            Console.WriteLine($"[{ip}] connected.");
            int userId = -1;

            using (client)
            using (NetworkStream stream = client.GetStream())
            {
                try
                {
                    var loginPacket = await ReadPacketAsync(stream);
                    byte[] buffer;

                    switch ((PacketType)loginPacket[0])
                    {
                        case PacketType.loginRequest:
                            LoginRequestPacket loginRequest = LoginRequestPacket.FromBytes(loginPacket);
                            if (isValidCredential(loginRequest))
                            {
                                userId = RegisterClient(ip);
                                SessionMap[userId] = stream;
                                buffer = new LoginResponsePacket
                                {
                                    successLogin = true,
                                    userId = userId

                                }.ToBytes();
                            }
                            else
                            {
                                buffer = new LoginResponsePacket { successLogin = false }.ToBytes();
                            }
                            await stream.WriteAsync(buffer, 0, buffer.Length);
                            Console.WriteLine($"{ip} sent login response packet.");
                            break;
                        case PacketType.RegUsrRequest:
                            RegUsrRequestPacket regRequest = RegUsrRequestPacket.FromBytes(loginPacket);
                            buffer = createRegisterResponsePacket(regRequest).ToBytes();
                            await stream.WriteAsync(buffer, 0, buffer.Length);
                            Console.WriteLine($"{ip} sent register response packet.");
                            break;
                        default:
                            return;
                    }

                    while (true)
                    {
                        var packet = await ReadPacketAsync(stream);
                        if (packet == null) break;

                        switch ((PacketType)packet[0])
                        {
                            case PacketType.move:

                                break;
                            case PacketType.chat:

                                break;


                            default:
                                Console.WriteLine($"{ip} unknown packet.");
                                break;
                        }
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine($"[{ip}] session error: {e.Message}");
                }
                finally
                {
                    ReleaseClient(ip);
                    if (SessionMap.TryRemove(userId, out var s)) s.Close();
                    Console.WriteLine($"[{ip}] disconnected");
                }
            }
        }

        private static int RegisterClient(string ip)
        {
            lock (UsedId)
            {
                for(int i=0; i<MaxUsr; i++)
                {
                    if (!UsedId[i])
                    {
                        UsedId[i] = true;
                        IpToUserId[ip] = i;
                        return i;
                    }
                }
            }
            throw new InvalidOperationException("User limit reached");
        }

        private static void ReleaseClient(string ip)
        {
            if (!IpToUserId.TryRemove(ip, out int id)) return;
            lock (UsedId) UsedId[id] = false;
        }

        private static async Task<byte[]> ReadPacketAsync(NetworkStream stream)
        {
            byte[] temp = new byte[4];
            int n = await stream.ReadAsync(temp, 0, 4);
            if (n == 0) return null;

            int len = BitConverter.ToInt32(temp, 0);
            byte[] packet = new byte[len];

            int offset = 0;

            while(offset < len)
            {
                int read = await stream.ReadAsync(packet, offset, len - offset);
                if (read == 0) return null;
                offset += read;
            }

            return packet;
        }

        static bool isValidCredential(LoginRequestPacket packet)
        {
            StreamReader rStream = new StreamReader("loginInfo.csv");

            string line;
            while ((line = rStream.ReadLine()) != null)
            {
                if (line == packet.id + ',' + packet.pw)
                {
                    rStream.Close();
                    return true;
                }
            }

            rStream.Close();
            return false;
        }

        static RegUsrResponsePacket createRegisterResponsePacket(RegUsrRequestPacket packet)
        {
            StreamReader rStream = new StreamReader("loginInfo.csv");

            string line;
            while((line = rStream.ReadLine()) != null)
            {
                if (line.Split(',')[0] == packet.id)
                {
                    rStream.Close();
                    return new RegUsrResponsePacket { successReg = false };
                }
            }
            rStream.Close();
            StreamWriter wStream = new StreamWriter("loginInfo.csv", true);
            wStream.WriteLine(packet.id + ',' + packet.pw);
            wStream.Close();

            return new RegUsrResponsePacket { successReg = true };
        }
    }
}
