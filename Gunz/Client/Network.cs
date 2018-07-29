using System;
using System.Linq;
using System.Text;

namespace Gunz {
    public partial class Client {
        private byte packet_id;

        private byte PacketID => packet_id;
        private byte NextPacketID => ++packet_id == 0xFF ? packet_id = 0 : packet_id;
    }
}