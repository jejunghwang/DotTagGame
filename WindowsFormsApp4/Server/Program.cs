using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Packets;
using static CharacterSelectPacket;

namespace Server
{
    internal class Program
    {
        private const int MaxUsr = 100;
        private int curUserNum = 0;
        private static int charSize = 32;
        private static int curTagger = 0;
        private static ConcurrentDictionary<string, int> IpToUserTag = new ConcurrentDictionary<string, int>();
        private static ConcurrentDictionary<int, NetworkStream> SessionMap = new ConcurrentDictionary<int, NetworkStream>();
        private static bool[] UsedTag = new bool[MaxUsr];
        private static ConcurrentDictionary<int, (int x, int y)> Positions = new ConcurrentDictionary<int, (int x, int y)>();
        private static bool[] readyStatus = new bool[100];
        private static ConcurrentDictionary<int, string> TagToId = new ConcurrentDictionary<int, string>();
        private static ConcurrentDictionary<int, int> TagToCharIdx = new ConcurrentDictionary<int, int>();
        private static HashSet<(int, int)> boxes = new HashSet<(int, int)>();

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
                                TagToCharIdx[userTag] = 1;

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
                                    welcome.Entries.Add((TagToId[kv.Key], (kv.Key, kv.Value.x, kv.Value.y, TagToCharIdx[kv.Key])));
                                }

                                int startX = 937, startY = 270;
                                Positions[userTag] = (startX, startY);

                                welcome.Entries.Add((userId, (userTag, startX, startY, TagToCharIdx[userTag])));

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
                                if(curTagger == mv.playerId)
                                    await CheckCollision(mv.playerId);
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

                                bool isAllReady = false;
                                for(int i=0; i<100; i++)
                                {
                                    if (UsedTag[i])
                                    {
                                        if (readyStatus[i])
                                        {
                                            isAllReady = true;
                                        }
                                        else
                                        {
                                            isAllReady = false;
                                            break;
                                        }
                                    }
                                }
                                if (isAllReady)
                                {
                                    
                                    for(int i=0; i<100; i++)
                                    {
                                        readyStatus[i] = false;
                                    }
                                    
                                    await BroadCastAsync(new StartPacket().ToBytes());
                                    Random rand = new Random();
                                    do
                                    {
                                        curTagger = rand.Next(0, 100);
                                    } while(!UsedTag[curTagger]);
                                    Console.WriteLine($"[SERVER] Initial Tagger = user {curTagger}");
                                    await BroadCastAsync(new ChangeTaggerPacket { playerTag = curTagger }.ToBytes());
                                }
                                break;

                            case PacketType.disconnect:
                                throw new Exception($"[{ip}] disconnected");

                            case PacketType.characterSelect:
                                var cs = CharacterSelectPacket.FromBytes(packet);
                                TagToCharIdx[cs.playerTag] = cs.characterIndex;
                                Console.WriteLine($"[CHAR_SELECT] Tag={cs.playerTag} Index={cs.characterIndex}");

                                byte[] toSend = new byte[4 + packet.Length];
                                BitConverter.GetBytes(packet.Length).CopyTo(toSend, 0);
                                packet.CopyTo(toSend, 4);

                                await BroadCastAsync(toSend);
                                break;
                            case PacketType.death:
                                var death_packet = DeathPacket.FromBytes(packet);
                                Console.WriteLine($"[DEATH]: {death_packet.playerTag}");
                                await BroadCastAsync(new ChangeTaggerPacket { playerTag = curTagger }.ToBytes());

                                break;
                            case PacketType.itemRemove:
                                var rp = ItemRemovePacket.FromBytes(packet);
                                Console.WriteLine($"[{ip}] got item");
                                if(boxes != null)
                                {
                                    boxes.Remove((rp.x, rp.y));
                                }
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

        private static async Task CheckCollision(int playerTag)
        {
            int x1, y1;
            (x1, y1) = Positions[playerTag];
            foreach (var position in Positions)
            {
                int x2, y2;
                (x2, y2) = position.Value;
                if (playerTag == position.Key) continue;
                if (charSize > Math.Abs((x2 + charSize / 2) - (x1 + charSize / 2)) + Math.Abs((y2 + charSize / 2) - (y1 + charSize / 2)))
                {
                    Console.WriteLine($"[{playerTag}] Collision {position.Key}");
                    await BroadCastAsync(new ChangeTaggerPacket { playerTag = position.Key }.ToBytes());
                    curTagger = position.Key;
                }
            }
        }

        private async Task spawn_items()
        {
            int[,] map = {
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,-7,-7,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-7,-7},
                {1,1,2,2,2,2,3,3,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6},
                {1,1,2,2,2,2,3,3,2,2,2,2,1,1,2,2,3,3,3,3,3,3,3,3,3,3,2,2,2,2,1,1,-7,-7,-6,-6,-6,-8,-9,-9,-9,-9,-9,-10,-7,-7},
                {1,1,2,2,2,2,3,3,2,2,2,2,1,1,2,2,3,3,3,3,3,3,3,3,3,3,2,2,2,2,1,1,-6,-6,-6,-6,-6,-15,3,3,3,3,3,-11,-6,-6},
                {1,1,2,2,2,2,3,3,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,-7,-7,-6,-6,-6,-15,3,-16,-16,-16,3,-11,-7,-7},
                {1,1,2,2,2,2,3,3,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,-6,-6,-1,-1,-1,-15,-16,-16,-16,-16,-16,-11,-6,-6},
                {1,1,2,2,2,2,3,3,2,2,2,2,1,1,2,2,2,2,2,2,2,2,3,3,3,3,2,2,2,2,1,1,-7,-7,-5,-5,-5,-15,-16,-16,-16,-16,-16,-11,-7,-7},
                {1,1,2,2,2,2,3,3,2,2,2,2,1,1,2,2,2,2,2,2,2,2,3,3,3,3,2,2,2,2,1,1,-6,-6,-2,-2,-2,-15,-16,-16,-16,-16,-16,-11,-6,-6},
                {1,1,2,2,2,2,3,3,2,2,2,2,1,1,2,2,2,2,2,2,2,2,3,3,3,3,2,2,2,2,1,1,-7,-7,-3,5,-4,-14,-13,-13,-13,-13,-13,-12,-7,-7 },
                {1,1,2,2,2,2,3,3,2,2,2,6,7,7,7,-3,5,-4,7,7,8,1,1,1,1,1,1,1,1,2,1,1,-6,-6,-3,5,-4,-6,-6,-6,-6,-3,5,-4,-6,-6},
                {1,1,2,2,2,2,3,3,2,2,2,13,4,4,4,-3,5,-4,4,4,9,1,1,1,1,1,1,1,1,2,1,1,-7,-7,-3,5,-4,-6,-6,-6,-6,-3,5,-4,-7,-7},
                {1,1,2,2,2,2,3,3,2,2,2,13,4,4,4,-3,5,-4,4,4,9,2,2,2,2,2,2,2,2,2,1,1,-6,-6,-3,5,-4,-6,-6,-6,-6,-3,5,-4,-6,-6},
                {1,1,2,2,2,2,3,3,2,2,2,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,2,2,3,3,2,2,2,2,2,1,1,-7,-7,-3,5,-4,-6,-6,-6,-6,-3,5,-4,-7,-7},
                {1,1,2,2,2,2,3,3,2,2,2,-5,-5,-5,-5,-5,-5,-5,-5,-5,-5,2,2,3,3,2,2,2,2,2,1,1,-6,-6,-3,5,-4,-6,-6,-6,-6,-3,5,-4,-6,-6},
                {1,1,2,2,2,2,2,2,2,2,2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,2,2,3,3,2,2,2,2,2,-1,-1,-1,-1,-1,-1,-1,-1,-1,-6,-6,-3,5,-4,-7,-7},
                {1,1,2,2,2,2,2,2,2,2,2,13,4,4,-3,5,-4,4,4,4,9,2,2,3,3,2,2,2,2,2,-5,-5,-5,-5,-5,-5,-5,-5,-5,-6,-6,-3,5,-4,-6,-6},
                {1,1,6,7,7,7,8,2,2,2,2,13,4,4,-3,5,-4,4,4,4,9,2,2,3,3,2,2,2,2,2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-6,-6,-3,5,-4,-7,-7},
                {1,1,13,-1,-1,-1,-1,2,2,2,2,13,4,4,-3,5,-4,4,4,4,9,2,2,3,3,2,2,2,2,2,1,1,-6,-6,-6,-6,-3,5,-4,-6,-6,-3,5,-4,-6,-6},
                {1,1,13,-5,-5,-5,-5,2,2,2,2,13,4,4,-3,5,-4,4,4,4,9,2,2,3,3,2,2,2,2,2,1,1,-7,-7,-6,-6,-3,5,-4,-6,-6,-3,5,-4,-7,-7},
                {1,1,13,-2,-2,-2,-2,2,2,2,2,12,11,11,-3,5,-4,11,11,11,10,2,2,3,3,2,2,2,2,2,1,1,-6,-6,-6,-6,-3,5,-4,-6,-6,-3,5,-4,-6,-6},
                {1,1,13,-3,5,-4,9,3,3,3,2,2,2,2,2,2,2,2,2,2,2,2,2,3,3,2,2,2,2,2,1,1,-7,-7,-6,-6,-3,5,-4,-6,-6,-3,5,-4,-7,-7},
                {1,1,13,-3,5,-4,9,3,3,3,2,2,2,2,2,2,2,2,2,2,2,2,2,3,3,2,2,2,2,2,1,1,-6,-6,-6,-6,-3,5,-4,-6,-6,-3,5,-4,-6,-6},
                {1,1,13,-3,5,-4,9,3,3,3,2,2,2,2,2,2,2,2,2,2,2,2,2,3,3,2,2,2,2,2,1,1,-7,-7,-6,-6,-3,5,-4,-6,-6,-3,5,-4,-7,-7},
                {1,1,13,-3,5,-4,9,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,3,3,2,2,2,2,2,1,1,-6,-6,-6,-6,-3,5,-4,-6,-6,-3,5,-4,-6,-6},
                {1,1,13,-1,-1,-1,-1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,3,3,2,2,2,2,2,1,1,-7,-7,-6,-6,-3,5,-4,-1,-1,-1,-1,-1,-7,-7},
                {1,1,13,-5,-5,-5,-5,3,3,3,3,3,3,3,3,3,3,2,2,2,2,2,2,2,2,6,7,7,7,8,1,1,-6,-6,-6,-6,-3,5,-4,-5,-5,-5,-5,-5,-6,-6},
                {1,1,13,-2,-2,-2,-2,3,3,3,3,3,3,3,3,3,3,2,2,2,2,2,2,2,2,13,4,4,4,9,1,1,-7,-7,-6,-6,-3,5,-4,-2,-2,-2,-2,-2,-7,-7},
                {1,1,13,4,4,4,9,3,3,3,3,3,3,3,3,3,3,2,2,2,2,2,2,2,2,13,4,4,4,9,1,1,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6},
                {1,1,12,11,11,11,10,3,3,3,3,3,3,3,3,3,3,2,2,2,2,2,2,2,2,12,11,11,11,10,1,1,-7,-7,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-7,-7},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6,-6},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7,-7}
            };
            int tileSize = 32, row = map.GetLength(0), col = map.GetLength(1);
            HashSet<int> canPlaceTile = new HashSet<int> { 5, 2, -1, -2, -3, -4, -5, -8, -9, -10, -11, -12, -13, -14, -15, -16 };
            
            Random rand = new Random();
            while (true)
            {
                foreach(var prevItem in boxes)
                {
                    ItemRemovePacket pk = new ItemRemovePacket { x = prevItem.Item1, y = prevItem.Item2 };
                    await BroadCastAsync(pk.ToBytes());
                }
                for(int i=0; i<5; i++)
                {
                    int x, y, itemType;
                    do
                    {
                        y = rand.Next(0, row);
                        x = rand.Next(0, col);

                    } while (!canPlaceTile.Contains(map[y, x]) || boxes.Contains((x, y)));
                    boxes.Add((x, y));
                    itemType = rand.Next(0, 1);
                    ItemSpawnPacket item = new ItemSpawnPacket { y = y, x = x, ItemId = itemType };
                    await BroadCastAsync(item.ToBytes());
                }
                await Task.Delay(10000);
            }
        }
    }
}