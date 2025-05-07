namespace Packets
{

    public enum PacketType
    {
        move,
        chat,
        skill,
        login,
        status,
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
}
