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
        private int curUserNum = 0;
        private static ConcurrentDictionary<string, int> IpToUserTag = new ConcurrentDictionary<string, int>();
        private static ConcurrentDictionary<int, NetworkStream> SessionMap = new ConcurrentDictionary<int, NetworkStream>();
        private static bool[] UsedTag = new bool[MaxUsr];
        private static ConcurrentDictionary<int, (int x, int y)> Positions = new ConcurrentDictionary<int, (int x, int y)>();
        private static bool[] readyStatus = new bool[100];
        private static ConcurrentDictionary<int, string> TagToId = new ConcurrentDictionary<int, string>();
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
            string ip = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
            Console.WriteLine($"[{ip}] connected.");
            int userTag = -1;
            string userId;

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
                                userTag = RegisterClient(ip);
                                userId = loginRequest.id;
                                SessionMap[userTag] = stream;
                                TagToId[userTag] = userId;
                                buffer = new LoginResponsePacket
                                {
                                    successLogin = true,
                                    userId = userTag
                                }.ToBytes();
                                await stream.WriteAsync(buffer, 0, buffer.Length);
                                Console.WriteLine($"[{ip}] logged in.");
                            }
                            else
                            {
                                buffer = new LoginResponsePacket { successLogin = false }.ToBytes();
                                await stream.WriteAsync(buffer, 0, buffer.Length);
                                Console.WriteLine($"[{ip}] sent login response packet.");
                                return;
                            }
                            break;
                        case PacketType.RegUsrRequest:
                            RegUsrRequestPacket regRequest = RegUsrRequestPacket.FromBytes(loginPacket);
                            buffer = createRegisterResponsePacket(regRequest).ToBytes();
                            await stream.WriteAsync(buffer, 0, buffer.Length);
                            Console.WriteLine($"[{ip}] sent register response packet.");
                            return;
                        default:
                            return;
                    }

                    while (true)
                    {
                        var packet = await ReadPacketAsync(stream);
                        if (packet == null || (PacketType)packet[0] == PacketType.disconnect) break;
                        byte[] wBuffer = new byte[4 + packet.Length];
                        BitConverter.GetBytes(packet.Length).CopyTo(wBuffer, 0);
                        switch ((PacketType)packet[0])
                        {
                            case PacketType.welcomeRequest:

                                var welcome = new WelcomeResponsePacket();
                                foreach (var kv in Positions)
                                {
                                    if (kv.Key == userTag) continue;
                                    welcome.Entries.Add((TagToId[kv.Key], (kv.Key, kv.Value.x, kv.Value.y)));
                                }

                                int startX = 937, startY = 270;
                                Positions[userTag] = (startX, startY);

                                welcome.Entries.Add((userId, (userTag, startX, startY)));

                                Console.WriteLine($"[Server] Sending WelcomeResponse to user {userTag}, entries={welcome.Entries.Count}");
                                //await stream.WriteAsync(welcome.ToBytes(), 0, welcome.ToBytes().Length);
                                await BroadCastAsync(welcome.ToBytes());
                                

                                //var welcomePacket = new MovePacket
                                //{
                                //    playerId = userTag,
                                //    x = startX,
                                //    y = startY
                                //}.ToBytes();

                                //await BroadCastAsync(welcomePacket);
                                break;
                            case PacketType.move:
                                var mv = MovePacket.FromBytes(packet);
                                Positions[mv.playerId] = (mv.x, mv.y);
                                Console.WriteLine($"[MOVE] {mv.playerId}: ({Positions[mv.playerId].x},{Positions[mv.playerId].y})");
                                packet.CopyTo(wBuffer, 4);
                                await BroadCastAsync(wBuffer);
                                break;

                            case PacketType.chat:
                                var chat = ChatPacket.FromBytes(packet);
                                Console.WriteLine($"[CHAT] {chat.playerId}: {chat.message}");
                                packet.CopyTo(wBuffer, 4);
                                await BroadCastAsync(wBuffer);
                                break;

                            case PacketType.ready:
                                var ready = ReadyPacket.FromBytes(packet);
                                Console.WriteLine($"[READY] {ready.playerTag}");

                                packet.CopyTo(wBuffer, 4);
                                await BroadCastAsync(wBuffer);

                                readyStatus[ready.playerTag] = !readyStatus[ready.playerTag];
                                
                                bool dp = readyStatus[0] ^ UsedTag[0];
                                for(int i=1; i<100; i++)
                                {
                                    dp = dp | (readyStatus[i] ^ UsedTag[i]);
                                }
                                if (!dp)
                                {
                                    
                                    for(int i=0; i<100; i++)
                                    {
                                        readyStatus[i] = false;
                                    }
                                    
                                    await BroadCastAsync(new StartPacket().ToBytes());
                                }
                                break;

                            case PacketType.disconnect:
                                throw new Exception($"[{ip}] disconnected");

                            case PacketType.characterSelect:
                                var cs = CharacterSelectPacket.FromBytes(packet);
                                Console.WriteLine($"[CHAR_SELECT] Tag={cs.playerTag} Index={cs.characterIndex}");

                                byte[] toSend = new byte[4 + packet.Length];
                                BitConverter.GetBytes(packet.Length).CopyTo(toSend, 0);
                                packet.CopyTo(toSend, 4);

                                await BroadCastAsync(toSend);
                                break;
                            default:
                                Console.WriteLine($"[{ip}] unknown packet.");
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
                    byte[] buffer = new DisconnectPacket { playerTag = userTag }.ToBytes();
                    ReleaseClient(ip);
                    if (SessionMap.TryRemove(userTag, out var s)) s.Close();
                    Console.WriteLine($"[{ip}] disconnected");
                    await BroadCastAsync(buffer);
                }
            }
        }

        private static int RegisterClient(string ip)
        {
            lock (UsedTag)
            {
                for(int i=0; i<MaxUsr; i++)
                {
                    if (!UsedTag[i])
                    {
                        UsedTag[i] = true;
                        IpToUserTag[ip] = i;
                        return i;
                    }
                }
            }
            throw new InvalidOperationException("User limit reached");
        }

        private static void ReleaseClient(string ip)
        {
            if (!IpToUserTag.TryRemove(ip, out int id)) return;
            lock (UsedTag) UsedTag[id] = false;
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
                if(packet.id.Split(',').Length > 1 || packet.pw.Split(',').Length > 1)
                {
                    rStream.Close();
                    return false;
                }
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
                if (line.Split(',')[0] == packet.id || packet.id.Split(',').Length > 1 || packet.id.Split(',').Length > 1)
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

        public static async Task BroadCastAsync(byte[] packet)
        {
            byte[] buffer = new byte[packet.Length];
            packet.CopyTo(buffer, 0);

            foreach (var session in SessionMap)
            {
                NetworkStream stream = session.Value;
                try
                {
                    await stream.WriteAsync(buffer, 0, buffer.Length);
                }
                catch
                {
                    if (SessionMap.TryRemove(session.Key, out var s)) s.Close();
                }
            }
        }


    }
}