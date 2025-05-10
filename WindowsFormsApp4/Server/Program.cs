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
        static bool[] usrId = new bool[100];
        
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

                        int packetLength = BitConverter.ToInt32(buffer, 0);

                        buffer = new byte[packetLength];
                        await stream.ReadAsync(buffer, 0, packetLength);

                        byte[] writeBuffer;
                        switch ((PacketType)buffer[0])
                        {
                            case PacketType.loginRequest:
                                Console.WriteLine("request login.");
                                LoginRequestPacket loginPacket = LoginRequestPacket.FromBytes(buffer);

                                writeBuffer = createLoginResponsePacket(loginPacket).ToBytes();

                                stream.Write(writeBuffer, 0, writeBuffer.Length);
                                Console.WriteLine("sent response packet.");
                                break;
                            case PacketType.RegUsrRequest:
                                Console.WriteLine("request register user.");
                                
                                try
                                {
                                    RegUsrRequestPacket registerPacket = RegUsrRequestPacket.FromBytes(buffer);
                                    writeBuffer = createRegisterResponsePacket(registerPacket).ToBytes();

                                    stream.Write(writeBuffer, 0, writeBuffer.Length);
                                    Console.WriteLine("sent response packet.");

                                }catch(Exception ex)
                                {
                                    Console.WriteLine("파싱 실패" + ex.Message);
                                }

                                break;
                            case PacketType.move:
                                {
                                    MovePacket movePacket = MovePacket.FromBytes(buffer);
                                    Console.WriteLine($"PlayerId: {movePacket.playerId} \nLocation: ({movePacket.x}, {movePacket.y})");

                                    byte[] moveResponse = movePacket.ToBytes();
                                    stream.Write(moveResponse, 0, moveResponse.Length);
                                    break;
                                }

                            case PacketType.chat:
                                {
                                    ChatPacket chatPacket = new ChatPacket().FromBytes(buffer);
                                    Console.WriteLine($"[Chat] {chatPacket.playerId}: {chatPacket.message}");

                                    byte[] chatResponse = chatPacket.ToBytes();
                                    stream.Write(chatResponse, 0, chatResponse.Length);
                                    break;
                                }

                        }
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

        static LoginResponsePacket createLoginResponsePacket(LoginRequestPacket packet)
        {
            StreamReader rStream = new StreamReader("loginInfo.csv");

            int newId = 0;
            while (usrId[newId]) { newId++; }


            string line;
            while ((line = rStream.ReadLine()) != null)
            {
                if (line == packet.id + ',' + packet.pw)
                {
                    rStream.Close();
                    return new LoginResponsePacket
                    {
                        userId = newId,
                        successLogin = true
                    };
                }
            }

            rStream.Close();
            return new LoginResponsePacket
            {
                userId = 0,
                successLogin = false
            };

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
