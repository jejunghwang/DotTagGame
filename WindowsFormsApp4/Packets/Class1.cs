using System;
using System.Text;

namespace Packets
{

    public enum PacketType
    {
        move,
        loginRequest,
        loginResponse,
        RegUsrRequest,
        RegUsrResponse,
        chat,
        skill,
        status
    }

    public interface Header
    {
        PacketType Type { get; }
        byte[] ToBytes();
    }

    public class MovePacket : Header
    {
        public PacketType Type => PacketType.move;

        public int packetLen = 13;
        public int playerId;
        public float x, y;

        public byte[] ToBytes()
        {
            byte[] buffer = new byte[17];
            BitConverter.GetBytes(packetLen).CopyTo(buffer, 0);
            buffer[4] = (byte)Type;
            BitConverter.GetBytes(playerId).CopyTo(buffer, 5);
            BitConverter.GetBytes(x).CopyTo(buffer, 9);
            BitConverter.GetBytes(y).CopyTo(buffer, 13);

            return buffer;
        }

        public static MovePacket FromBytes(byte[] buffer)
        {
            return new MovePacket
            {
                playerId = BitConverter.ToInt32(buffer, 1),
                x = BitConverter.ToSingle(buffer, 5),
                y = BitConverter.ToSingle(buffer, 9)
            };
        }
    }

    public class LoginRequestPacket : Header
    {
        public PacketType Type => PacketType.loginRequest;

        public int packetLen;
        public string id, pw;

        public byte[] ToBytes()
        {
            byte[] idBytes = Encoding.UTF8.GetBytes(id);
            byte[] pwBytes = Encoding.UTF8.GetBytes(pw);
            
            packetLen = 9 + idBytes.Length + pwBytes.Length;
            byte[] buffer = new byte[4 + packetLen];

            BitConverter.GetBytes(packetLen).CopyTo(buffer, 0);
            buffer[4] = (byte)Type;

            BitConverter.GetBytes(idBytes.Length).CopyTo(buffer, 5);
            idBytes.CopyTo(buffer, 9);

            BitConverter.GetBytes(pwBytes.Length).CopyTo(buffer, 9 + idBytes.Length);
            pwBytes.CopyTo(buffer, 13 + idBytes.Length);

            return buffer;
        }

        public static LoginRequestPacket FromBytes(byte[] buffer)
        {
            int idLength = BitConverter.ToInt32(buffer, 1);
            string id = Encoding.UTF8.GetString(buffer, 5, idLength);

            int pwLength = BitConverter.ToInt32(buffer, 5 + idLength);
            string pw = Encoding.UTF8.GetString(buffer, 9 + idLength, pwLength);

            return new LoginRequestPacket
            {
                id = id,
                pw = pw
            };
        }
    }

    public class LoginResponsePacket : Header
    {
        public PacketType Type => PacketType.loginResponse;

        int packetLen = 6;
        public bool successLogin;
        public int userId;
        public byte[] ToBytes()
        {
            byte[] buffer = new byte[10];
            BitConverter.GetBytes(packetLen).CopyTo(buffer, 0);
            buffer[4] = (byte)Type;
            BitConverter.GetBytes(successLogin).CopyTo(buffer, 5);
            BitConverter.GetBytes(userId).CopyTo(buffer, 6);

            return buffer;
        }

        public static LoginResponsePacket FromBytes(byte[] buffer)
        {
            return new LoginResponsePacket
            {
                successLogin = BitConverter.ToBoolean(buffer, 1),
                userId = BitConverter.ToInt32(buffer, 2)
            };
        }
    }

    public class RegUsrRequestPacket : Header
    {
        public PacketType Type => PacketType.RegUsrRequest;

        int packetLen;
        public string id, pw;

        public byte[] ToBytes()
        {
            byte[] idBytes = Encoding.UTF8.GetBytes(id);
            byte[] pwBytes = Encoding.UTF8.GetBytes(pw);

            packetLen = idBytes.Length + pwBytes.Length + 9;
            byte[] buffer = new byte[4 + packetLen];

            BitConverter.GetBytes(packetLen).CopyTo(buffer, 0);
            buffer[4] = (byte)Type;

            BitConverter.GetBytes(idBytes.Length).CopyTo(buffer, 5);
            idBytes.CopyTo(buffer, 9);

            BitConverter.GetBytes(pwBytes.Length).CopyTo(buffer, 9 + idBytes.Length);
            pwBytes.CopyTo(buffer, 13 + idBytes.Length);

            return buffer;
        }

        public static RegUsrRequestPacket FromBytes(byte[] buffer)
        {
            int idLength = BitConverter.ToInt32(buffer, 1);
            string id = Encoding.UTF8.GetString(buffer, 5, idLength);

            int pwLength = BitConverter.ToInt32(buffer, 5 + idLength);
            string pw = Encoding.UTF8.GetString(buffer, 9 + idLength, pwLength);

            return new RegUsrRequestPacket
            {
                id = id,
                pw = pw
            };
        }
    }

    public class RegUsrResponsePacket : Header
    {
        public PacketType Type => PacketType.RegUsrResponse;

        public int packetLen = 2;
        public bool successReg;

        public byte[] ToBytes()
        {
            byte[] buffer = new byte[6];

            BitConverter.GetBytes(packetLen).CopyTo(buffer, 0);
            buffer[4] = (byte)Type;
            BitConverter.GetBytes(successReg).CopyTo(buffer, 5);

            return buffer;
        }

        public static RegUsrResponsePacket FromBytes(byte[] buffer)
        {
            return new RegUsrResponsePacket { successReg = BitConverter.ToBoolean(buffer, 1) };
        }
    }

    public class ChatPacket : Header
    {
        public PacketType Type => PacketType.chat;

        int packetLen;
        public int playerId;
        public string message;

        public byte[] ToBytes()
        {
            // byte[] playerIdBytes = Encoding.UTF8.GetBytes(playerId);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

            int packetLen = 9 + messageBytes.Length;
            byte[] buffer = new byte[4 + packetLen];

            // 패킷 길이 (4 bytes)
            BitConverter.GetBytes(packetLen).CopyTo(buffer, 0);

            // 패킷 타입 (1 byte)
            buffer[4] = (byte)Type;

            BitConverter.GetBytes(playerId).CopyTo(buffer, 5);
            BitConverter.GetBytes(messageBytes.Length).CopyTo(buffer, 9);
            messageBytes.CopyTo(buffer, 13);

/*          // 플레이어 ID 길이 (4 bytes)
            BitConverter.GetBytes(playerIdBytes.Length).CopyTo(buffer, 5);

            // 플레이어 ID
            playerIdBytes.CopyTo(buffer, 9);

            // 메시지 본문
            messageBytes.CopyTo(buffer, 9 + playerIdBytes.Length);*/

            return buffer;
        }

        public static ChatPacket FromBytes(byte[] buffer)
        {
            int messageLength = BitConverter.ToInt32(buffer, 5);
            string message = Encoding.UTF8.GetString(buffer, 9, messageLength);
            return new ChatPacket
            {
                playerId = BitConverter.ToInt32(buffer, 1),
                message = message
            };

/*            int playerIdLength = BitConverter.ToInt32(buffer, 5);
            playerId = Encoding.UTF8.GetString(buffer, 9, playerIdLength);

            int messageStart = 9 + playerIdLength;
            int messageLength = buffer.Length - messageStart;
            message = Encoding.UTF8.GetString(buffer, messageStart, messageLength);

            return this;*/
        }
    }
}
