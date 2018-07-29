using System;
using System.Linq;
using System.Text;

namespace Gunz {
    public partial class Client {
        public Packet Match_Login(string username, string password, ulong checksum, string md5) {
            var packet = new Packet();

            packet.Type = PacketType.Encrypted;
            packet.CommandID = Command.MATCH_LOGIN;
            packet.PacketID = NextPacketID;
            
            packet.WriteString(username);
            packet.WriteString(password);
            packet.WriteInt(Packet.COMMAND_VERSION);
            packet.WriteULong(checksum);
            packet.WriteBlob(Encoding.ASCII.GetBytes(md5));

            packet.Encrypt(Packet.DefaultKey);
            packet.Checksum = packet.CalculateChecksum(packet.Size);

            return packet;
        }
    }
}