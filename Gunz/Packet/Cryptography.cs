using System;
using System.Linq;
using System.Text;

namespace Gunz {
    public partial class Packet {
        const byte COMMAND_VERSION = 0x38;
        
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
        
        private byte Encrypt(byte b, byte k) {
            var x = ShiftRight(b ^ k, 5);
            var y = ShiftLeft(b ^ k, 3);
            var z = x | y;

            return (byte)(z ^ 0xF0);
        }

        public void Encrypt(byte[] key) {
            buffer[2] = Encrypt(buffer[2], key[0]);
            buffer[3] = Encrypt(buffer[3], key[1]);

            for (var i = HEADER_SIZE; i < GetPacketSize(); i++)
                buffer[i] = Encrypt(buffer[i], key[i % 0x20]);
        }

        private byte Decrypt(byte b, byte k) {
            var x = ShiftRight(b ^ 0xf0, 3);
            var y = b ^ 0xf0 & 7;
            var z = x | ShiftLeft(y, 5);

            return (byte)(z ^ k);
        }

        public void Decrypt(byte[] key) {
            buffer[2] = Decrypt(buffer[2], key[0]);
            buffer[3] = Decrypt(buffer[3], key[1]);

            for (var i = HEADER_SIZE; i < GetPacketSize(); i++)
                buffer[i] = Decrypt(buffer[i], key[i % 0x20]);
        }
    }
}