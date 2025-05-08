using System;
using System.Text;

namespace Packets
{

    public enum PacketType
    {
        move,
        loginRequest,
        loginResponse,
        addUsrRequest,
        addUsrResponse,
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

        public int playerId;
        public float x, y;

        public byte[] ToBytes()
        {
            byte[] buffer = new byte[13];
            buffer[0] = (byte)Type;
            BitConverter.GetBytes(playerId).CopyTo(buffer, 1);
            BitConverter.GetBytes(x).CopyTo(buffer, 5);
            BitConverter.GetBytes(y).CopyTo(buffer, 9);

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

        public string id, pw;

        public byte[] ToBytes()
        {
            byte[] buffer = new byte[9 + id.Length + pw.Length];
            buffer[0] = (byte)Type;
            byte[] idBytes = Encoding.UTF8.GetBytes(id);
            byte[] pwBytes = Encoding.UTF8.GetBytes(pw);

            BitConverter.GetBytes(idBytes.Length).CopyTo(buffer, 1);
            idBytes.CopyTo(buffer, 5);

            BitConverter.GetBytes(pwBytes.Length).CopyTo(buffer, 5 + idBytes.Length);
            pwBytes.CopyTo(buffer, 9 + idBytes.Length);

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

        bool successLogin;
        public byte[] ToBytes()
        {
            byte[] buffer = new byte[2];
            buffer[0] = (byte)Type;
            BitConverter.GetBytes(successLogin).CopyTo(buffer, 1);
            return buffer;
        }

        public static LoginResponsePacket FromBytes(byte[] buffer)
        {
            return new LoginResponsePacket
            {
                successLogin = BitConverter.ToBoolean(buffer, 1)
            };
        }
    }

    public class ChatPacket : Header
    {
        public PacketType Type => PacketType.chat;
        int playerId;
        string message;

        public byte[] ToBytes()
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] buffer = new byte[9 + messageBytes.Length];

            BitConverter.GetBytes(playerId).CopyTo(buffer, 1);
            BitConverter.GetBytes(messageBytes.Length).CopyTo(buffer, 5);
            messageBytes.CopyTo(buffer, 9);

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
