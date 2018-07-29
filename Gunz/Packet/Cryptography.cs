using System;
using System.Linq;
using System.Text;

namespace Gunz {
    public partial class Packet {
        const byte COMMAND_VERSION = 0x38;

        private static int nSHL;
        private static int shlMask;

        static Packet() {
            nSHL = (COMMAND_VERSION % 6) + 1;

            for (var i = 0; i < nSHL; i++)
                shlMask += (1 << i);
        }
        
        public static readonly byte[] DefaultKey = {
            00, 00, 00, 00, 00, 00, 00, 00,
            00, 00, 00, 00, 00, 00, 00, 00,
            55, 04, 93, 46, 67, COMMAND_VERSION, 73, 83,
            80, 05, 19, 201, 40, 164, 77, 05
        };

        private static readonly byte[] XORTable = {
            87, 2, 91, 4, 52, 6, 1, 8, 55, 10, 18, 105, 65, 56, 15, 120
        };
        
        public static unsafe void MadeSeedKey(MUID server, MUID client, uint time_stamp) {
            var ts_bytes = BitConverter.GetBytes(time_stamp);
            var sv_bytes = BitConverter.GetBytes(server.Low);
            var ch_bytes = BitConverter.GetBytes(client.High);
            var cl_bytes = BitConverter.GetBytes(client.Low);
            
            Array.Copy(ts_bytes, 0, DefaultKey, 0,  4);
            Array.Copy(sv_bytes, 0, DefaultKey, 4,  4);
            Array.Copy(ch_bytes, 0, DefaultKey, 8,  4);
            Array.Copy(cl_bytes, 0, DefaultKey, 12, 4);

            for (var i = 0; i < XORTable.Length; i++)
                DefaultKey[i] ^= XORTable[i];
        }

        private byte Encrypt(byte s, byte k) {
            ushort w;
            byte b, bh;

            b  = (byte)  (s ^ k);
            w  = (ushort)(b << nSHL);
            bh = (byte)  ((w & 0xFF00) >> 8);
            b  = (byte)  (w & 0xFF);

            b  = (byte)  (b | bh);
            bh = (byte)  (b ^ 0xF0);
            return bh;
        }

        public void Encrypt(byte[] key) {
            buffer[2] = Encrypt(buffer[2], key[0]);
            buffer[3] = Encrypt(buffer[3], key[1]);

            for (var i = HEADER_SIZE; i < GetPacketSize(); i++)
                buffer[i] = Encrypt(buffer[i], key[i % 0x20]);
        }

        private byte Decrypt(byte s, byte k) {
            byte b, bh, d;

            b  = (byte)(s ^ 0xF0);
            bh = (byte)(b & shlMask);

            bh = (byte)(bh << (8 - nSHL));
            b  = (byte)(b >> nSHL);

            d  = (byte)(bh | b);
            d  = (byte)(d ^ k);
            
            return d;
        }

        public void Decrypt(byte[] key) {
            buffer[2] = Decrypt(buffer[2], key[0]);
            buffer[3] = Decrypt(buffer[3], key[1]);

            for (var i = HEADER_SIZE; i < GetPacketSize(); i++)
                buffer[i] = Decrypt(buffer[i], key[i % 0x20]);
        }
    }
}