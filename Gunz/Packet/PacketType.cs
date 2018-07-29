using System;
using System.Collections.Generic;
using System.Text;

namespace Gunz
{
    public class PacketType
    {
        public const ushort Encrypted = 0x65;
        public const ushort Decrypted = 0x64;
        public const ushort KeyExchange = 0x0A;
    }
}
