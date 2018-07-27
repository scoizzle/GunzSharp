using System;
using System.Linq;
using System.Text;

namespace Gunz {
    public partial class Packet {
		private ushort index;
        private byte[] buffer;

        public const ushort HEADER_SIZE = 0x06;
        public const ushort DATA_HEADER_SIZE = 0x05;

        public Packet() {
            buffer = new byte[ushort.MaxValue];

            ResetIndex();
            SetDataSize(HEADER_SIZE);
            SetPacketSize(HEADER_SIZE + DATA_HEADER_SIZE);
        }

        public void ResetIndex() {
            index = HEADER_SIZE + DATA_HEADER_SIZE;
        }
    }
}