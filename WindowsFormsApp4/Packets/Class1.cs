using Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
        status,
        welcomeRequest,
        welcomeResponse,
        ready,
        disconnect,
        start,
        characterSelect,
        changeTagger,
        death,
        itemSpawn,
        itemRemove,
        itemPickUp,
        itemEffect,
        usingCurseItem
    }
    
    public interface pHeader
    {
        PacketType Type { get; }
        byte[] ToBytes();
    }

    public class MovePacket : pHeader
    {
        public PacketType Type => PacketType.move;

        public int packetLen = 13;
        public int playerId;
        public int x, y;

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
                x = BitConverter.ToInt32(buffer, 5),
                y = BitConverter.ToInt32(buffer, 9)
            };
        }
    }

    public class LoginRequestPacket : pHeader
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

    public class LoginResponsePacket : pHeader
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

    public class RegUsrRequestPacket : pHeader
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

    public class RegUsrResponsePacket : pHeader
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

    public class ChatPacket : pHeader
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

    public class WelcomeRequestPacket : pHeader
    {
        public PacketType Type => PacketType.welcomeRequest;
        private const int BodyLength = 1;

        public byte[] ToBytes()
        {
            int packetLen = 5;

            byte[] buffer = new byte[9];
            BitConverter.GetBytes(packetLen).CopyTo(buffer, 0);
            BitConverter.GetBytes(BodyLength).CopyTo(buffer, 5);
            buffer[4] = (byte)Type;
            return buffer;
        }

        public static WelcomeRequestPacket FromBytes(byte[] buffer)
        {
            return new WelcomeRequestPacket();
        }
    }

    public class WelcomeResponsePacket : pHeader
    {
        public PacketType Type => PacketType.welcomeResponse;
        public List<(string playerId, (int playerTag, int x, int y, int characterIndex))> Entries { get; } = new List<(string, (int, int, int, int))>();
        public byte[] ToBytes()
        {
            int count = Entries.Count;
            List<int> idLen = new List<int>();
            for (int i = 0; i < Entries.Count; i++)
                idLen.Add(Entries[i].Item1.Length);
            int bodyLen = 1 + 4 + count * 20 + idLen.Sum();
            byte[] buffer = new byte[4 + bodyLen];

            BitConverter.GetBytes(bodyLen).CopyTo(buffer, 0);
            buffer[4] = (byte)Type;
            BitConverter.GetBytes(count).CopyTo(buffer, 5);

            int offset = 9;
            foreach (var (id, (tag, x, y, charIdx)) in Entries)
            {
                byte[] idBytes = Encoding.UTF8.GetBytes(id);
                BitConverter.GetBytes(idBytes.Length).CopyTo(buffer, offset);
                offset += 4;
                idBytes.CopyTo(buffer, offset);
                offset += idBytes.Length;
                BitConverter.GetBytes(tag).CopyTo(buffer, offset);
                offset += 4;
                BitConverter.GetBytes(x).CopyTo(buffer, offset);
                offset += 4;
                BitConverter.GetBytes(y).CopyTo(buffer, offset);
                offset += 4;
                BitConverter.GetBytes(charIdx).CopyTo(buffer, offset);
                offset += 4;
            }
            return buffer;
        }

        public static WelcomeResponsePacket FromBytes(byte[] buffer)
        {
            var welcomePacket = new WelcomeResponsePacket();
            int count = BitConverter.ToInt32(buffer, 1);
            int offset = 5;
            for (int i = 0; i < count; i++)
            {
                int idLen = BitConverter.ToInt32(buffer, offset);
                offset += 4;
                string id = Encoding.UTF8.GetString(buffer, offset, idLen);
                offset += idLen;
                int tag = BitConverter.ToInt32(buffer, offset);
                offset += 4;
                int x = BitConverter.ToInt32(buffer, offset);
                offset += 4;
                int y = BitConverter.ToInt32(buffer, offset);
                offset += 4;
                int charIdx = BitConverter.ToInt32(buffer, offset);
                offset += 4;
                welcomePacket.Entries.Add((id, (tag, x, y, charIdx)));
                //offset += 16;
            }
            return welcomePacket;
        }
    }

    public class DisconnectPacket : pHeader
    {
        public PacketType Type => PacketType.disconnect;
        int packetLen = 5;
        public int playerTag;

        public byte[] ToBytes()
        {
            byte[] buffer = new byte[4 + packetLen];
            BitConverter.GetBytes(packetLen).CopyTo(buffer, 0);
            buffer[4] = (byte)PacketType.disconnect;

            BitConverter.GetBytes(playerTag).CopyTo(buffer, 5);

            return buffer;
        }

        public static DisconnectPacket FromBytes(byte[] buffer)
        {
            return new DisconnectPacket { playerTag = BitConverter.ToInt32(buffer, 1) };
        }
    }
    
    public class ReadyPacket : pHeader
    {
        public PacketType Type => PacketType.ready;
        int packetLen = 5;
        public int playerTag;

        public byte[] ToBytes()
        {
            byte[] buffer = new byte[4 + packetLen];
            BitConverter.GetBytes(packetLen).CopyTo(buffer, 0);
            buffer[4] = (byte)PacketType.ready;
            BitConverter.GetBytes(playerTag).CopyTo(buffer, 5);

            return buffer;
        }

        public static ReadyPacket FromBytes(byte[] buffer)
        {
            return new ReadyPacket { playerTag = BitConverter.ToInt32(buffer, 1) };
        }
    }
}

public class StartPacket : pHeader
{
    public PacketType Type => PacketType.start;
    int packetLen = 1;
    public byte[] ToBytes()
    {
        byte[] buffer = new byte[4 + packetLen];
        BitConverter.GetBytes(packetLen).CopyTo(buffer, 0);
        buffer[4] = (byte)PacketType.start;
        return buffer;
    }
}

public class CharacterSelectPacket : pHeader
{
    public PacketType Type => PacketType.characterSelect;
    int packetLen = 9;
    public int playerTag;        // 누가 선택했는지
    public int characterIndex;   // 1~4 중 어떤 캐릭터인지

    public byte[] ToBytes()
    {
        byte[] buffer = new byte[4 + packetLen];
        BitConverter.GetBytes(packetLen).CopyTo(buffer, 0);
        buffer[4] = (byte)Type;
        BitConverter.GetBytes(playerTag).CopyTo(buffer, 5);
        BitConverter.GetBytes(characterIndex).CopyTo(buffer, 9);
        return buffer;
        /*        var buf = new List<byte> { (byte)PacketType.characterSelect };
                buf.AddRange(BitConverter.GetBytes(playerTag));
                buf.AddRange(BitConverter.GetBytes(characterIndex));
                return buf.ToArray();*/
    }
    public static CharacterSelectPacket FromBytes(byte[] data)
    {
        return new CharacterSelectPacket {
            playerTag = BitConverter.ToInt32(data, 1),
            characterIndex = BitConverter.ToInt32(data, 5)
        };
    }

    public class ChangeTaggerPacket : pHeader
    {
        public PacketType Type => PacketType.changeTagger;
        int packetLen = 5;
        public int playerTag;

        public byte[] ToBytes()
        {
            byte[] buffer = new byte[4 + packetLen];
            BitConverter.GetBytes(packetLen).CopyTo(buffer, 0);
            buffer[4] = (byte)Type;
            BitConverter.GetBytes(playerTag).CopyTo(buffer, 5);

            return buffer;
        }

        public static ChangeTaggerPacket FromBytes(byte[] buffer)
        {
            return new ChangeTaggerPacket { playerTag = BitConverter.ToInt32(buffer, 1) };
        }
    }

    public class DeathPacket : pHeader
    {
        public PacketType Type => PacketType.death;
        int packetLen = 5;
        public int playerTag;

        public byte[] ToBytes()
        {
            byte[] buffer = new byte[4 + packetLen];
            BitConverter.GetBytes(packetLen).CopyTo(buffer, 0);
            buffer[4] = (byte)Type;
            BitConverter.GetBytes(playerTag).CopyTo(buffer, 5);

            return buffer;
        }

        public static DeathPacket FromBytes(byte[] buffer)
        {
            return new DeathPacket { playerTag = BitConverter.ToInt32(buffer, 1) };
        }
    }

    public class UseCurseItemPacket : pHeader
    {
        public PacketType Type => PacketType.usingCurseItem;

        int packetLen = 5;
        public int playerTag;

        public byte[] ToBytes()
        {
            byte[] buffer = new byte[4 + packetLen];
            BitConverter.GetBytes(packetLen).CopyTo(buffer, 0);
            buffer[4] = (byte)Type;
            BitConverter.GetBytes(playerTag).CopyTo(buffer, 5);
            return buffer;
        }

        public static UseCurseItemPacket FromBytes(byte[] buffer)
        {
            return new UseCurseItemPacket { playerTag = BitConverter.ToInt32((byte[])buffer, 1) };
        }
    }

    public class ItemSpawnPacket : pHeader
    {
        public PacketType Type => PacketType.itemSpawn;
        public int packetLen = 13;
        public int ItemId;
        public int x, y;
        public byte[] ToBytes()
        {
            byte[] buffer = new byte[4 + packetLen];
            BitConverter.GetBytes(packetLen).CopyTo(buffer, 0);
            buffer[4] = (byte)Type;
            BitConverter.GetBytes(ItemId).CopyTo(buffer, 5);
            BitConverter.GetBytes(x).CopyTo(buffer, 9);
            BitConverter.GetBytes(y).CopyTo(buffer, 13);

            return buffer;
        }


        public static ItemSpawnPacket FromBytes(byte[] buffer)
        {
            return new ItemSpawnPacket
            {
                ItemId = BitConverter.ToInt32(buffer, 1),
                x = BitConverter.ToInt32(buffer, 5),
                y = BitConverter.ToInt32(buffer, 9)
            };
        }
    }
    public class ItemRemovePacket : pHeader
    {
        public PacketType Type => PacketType.itemRemove;
        public int packetLen = 9;
        public int x, y;
        public byte[] ToBytes()
        {

            byte[] buffer = new byte[4 + packetLen];

            BitConverter.GetBytes(packetLen).CopyTo(buffer, 0);

            buffer[4] = (byte)Type;
            BitConverter.GetBytes(x).CopyTo(buffer, 5);
            BitConverter.GetBytes(y).CopyTo(buffer, 9);

            return buffer;
        }

        public static ItemRemovePacket FromBytes(byte[] buffer)
        {
            return new ItemRemovePacket
            {
                x = BitConverter.ToInt32(buffer, 1),
                y = BitConverter.ToInt32(buffer, 5)
            };
        }
    }

    public class ItemPickupPacket : pHeader
    {
        public PacketType Type => PacketType.itemPickUp;

        public int playerId;
        public int x, y;

        public byte[] ToBytes()
        {
            int packetLen = 13;
            byte[] buffer = new byte[4 + packetLen];

            BitConverter.GetBytes(packetLen).CopyTo(buffer, 0);
            buffer[4] = (byte)Type;
            BitConverter.GetBytes(playerId).CopyTo(buffer, 5);
            BitConverter.GetBytes(x).CopyTo(buffer, 9);
            BitConverter.GetBytes(y).CopyTo(buffer, 13);

            return buffer;
        }

        public static ItemPickupPacket FromBytes(byte[] buffer)
        {
            return new ItemPickupPacket
            {
                playerId = BitConverter.ToInt32(buffer, 1),
                x = BitConverter.ToInt32(buffer, 5),
                y = BitConverter.ToInt32(buffer, 9)
            };
        }
    }

    public class ItemEffectPacket : pHeader
    {
        public PacketType Type => PacketType.itemEffect;
        public int packetLen = 21;
        public int playerId;
        public int itemType;
        public int x, y;
        public int dir = -1;

        public byte[] ToBytes()
        {
            byte[] buffer = new byte[packetLen + 4];
            BitConverter.GetBytes(packetLen).CopyTo(buffer, 0);
            buffer[4] = (byte)Type;
            BitConverter.GetBytes(playerId).CopyTo(buffer, 5);
            BitConverter.GetBytes(itemType).CopyTo(buffer, 9);
            BitConverter.GetBytes(x).CopyTo(buffer, 13);
            BitConverter.GetBytes(y).CopyTo(buffer, 17);
            BitConverter.GetBytes(dir).CopyTo(buffer, 21);
            return buffer;
        }

        public static ItemEffectPacket FromBytes(byte[] buffer)
        {
            return new ItemEffectPacket
            {
                playerId = BitConverter.ToInt32(buffer, 1),
                itemType = (int)BitConverter.ToInt32(buffer, 5),
                x = BitConverter.ToInt32(buffer, 9),
                y = BitConverter.ToInt32(buffer, 13),
                dir = BitConverter.ToInt32(buffer, 17)
            };
        }
    }
}