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
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x37, 0x04, 0x5D, 0x2E, 0x43, COMMAND_VERSION, 0x49, 0x53,
            0x50, 0x05, 0x13, 0xC9, 0x28, 0xA4, 0x4D, 0x05
        };

        private static byte ShiftLeft(int b, int n) {
            if (n == 0) return (byte)(b);
            if (n > 8) n = n % 8;

            int value = b;

            while (n-- > 0)
                value *= 2;

            return (byte)(value);
        }

        private static byte ShiftRight(int b, int n) {
            if (n == 0) return (byte)(b);
            if (n > 8) n = n % 8;

            int value = b;

            while (n-- > 0)
                value /= 2;

            return (byte)(value);
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