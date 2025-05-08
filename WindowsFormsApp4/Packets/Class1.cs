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
            byte[] buffer = new byte[packetLen];

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
        int userId;
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

    public class RegUsrRequest : Header
    {
        public PacketType Type => PacketType.RegUsrRequest;

        int packetLen;
        string id, pw;

        public byte[] ToBytes()
        {
            byte[] idBytes = Encoding.UTF8.GetBytes(id);
            byte[] pwBytes = Encoding.UTF8.GetBytes(pw);

            packetLen = idBytes.Length + pwBytes.Length + 13;
            byte[] buffer = new byte[packetLen];

            BitConverter.GetBytes(packetLen).CopyTo(buffer, 0);
            buffer[4] = (byte)Type;

            BitConverter.GetBytes(idBytes.Length).CopyTo(buffer, 5);
            idBytes.CopyTo(buffer, 9);

            BitConverter.GetBytes(pwBytes.Length).CopyTo(buffer, 9 + idBytes.Length);
            pwBytes.CopyTo(buffer, 13 + idBytes.Length);

            return buffer;
        }

        public static RegUsrRequest FromBytes(byte[] buffer)
        {
            int idLength = BitConverter.ToInt32(buffer, 1);
            string id = Encoding.UTF8.GetString(buffer, 5, idLength);

            int pwLength = BitConverter.ToInt32(buffer, 5 + idLength);
            string pw = Encoding.UTF8.GetString(buffer, 9 + idLength, pwLength);

            return new RegUsrRequest
            {
                id = id,
                pw = pw
            };
        }
    }

    public class RegUsrResponse : Header
    {
        public PacketType Type => PacketType.RegUsrResponse;

        int packetLen;
        bool successReg;
        string id, pwd;

        public byte[] ToBytes()
        {
            byte[] idBytes = Encoding.UTF8.GetBytes(id);
            byte[] pwdBytes = Encoding.UTF8.GetBytes(pwd);

            packetLen = 14 + idBytes.Length + pwdBytes.Length;
            byte[] buffer = new byte[packetLen];

            buffer[0] = (byte)Type;
            BitConverter.GetBytes(successReg).CopyTo(buffer, 1);
            BitConverter.GetBytes(packetLen).CopyTo(buffer, 2);
            BitConverter.GetBytes(idBytes.Length).CopyTo(buffer, 6);
            idBytes.CopyTo(buffer, 10);

            BitConverter.GetBytes(pwdBytes.Length).CopyTo(buffer, 10 + idBytes.Length);
            pwdBytes.CopyTo(buffer, 14 + idBytes.Length);

            return buffer;
        }

        public static RegUsrResponse FromBytes(byte[] buffer)
        {
            bool successReg = BitConverter.ToBoolean(buffer, 1);
            int idLength = BitConverter.ToInt32(buffer, 2);
            int packetLen = BitConverter.ToInt32(buffer, 6);

            string id = Encoding.UTF8.GetString(buffer, 10, idLength);

            int pwdLength = BitConverter.ToInt32(buffer, 10 + idLength);
            string pwd = Encoding.UTF8.GetString(buffer, 14 + idLength, pwdLength);

            return new RegUsrResponse
            {
                successReg = successReg,
                packetLen = packetLen,
                id = id,
                pwd = pwd
            };
        }
    }

    public class ChatPacket : Header
    {
        public PacketType Type => PacketType.chat;

        int packetLen;
        int playerId;
        string message;

        public byte[] ToBytes()
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

            packetLen = 13 + messageBytes.Length;
            byte[] buffer = new byte[packetLen];

            BitConverter.GetBytes(packetLen).CopyTo(buffer, 0);
            buffer[4] = (byte)Type;

            BitConverter.GetBytes(playerId).CopyTo(buffer, 5);
            BitConverter.GetBytes(messageBytes.Length).CopyTo(buffer, 9);
            messageBytes.CopyTo(buffer, 13);

            return buffer;
        }

        public ChatPacket FromBytes(byte[] buffer)
        {
            int messageLength = BitConverter.ToInt32(buffer, 5);
            string message = Encoding.UTF8.GetString(buffer, 9, messageLength);
            return new ChatPacket
            {
                playerId = BitConverter.ToInt32(buffer, 1),
                message = message
            };
        }
    }
}
