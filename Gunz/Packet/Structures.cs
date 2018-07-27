using System;
using System.Linq;
using System.Text;

namespace Gunz {
    public partial class Packet {
        struct PacketHeader {
            public ushort Type;
            public ushort Size;
            public ushort Checksum;
        }

        struct DataHeader {
            public ushort DataSize;
            public ushort CommandID;
            public byte PacketID;
        }
    }
}