using System;
using System.Linq;

namespace Gunz {
    public partial class Packet {
        // *** HEADER ***
        public ushort Version,
               FullSize, // Encrypted
               Checksum,
        // *** PAYLOAD ***
               DataSize, // Encrypted
               CommandID; // Encrypted
               
        public byte PacketID; // Encrypted

        public ParameterBuilder Parameters; // Encrypted

        public Packet() {
        }

        public bool IsEncrypted {
            get { return Version == 0x65; }
            set { 
                if (value) Version = 0x65;
                else Version = 0x64; 
            }
        }

        internal void set_checksum(Packet packet) {
            var buffer = (byte[])Parameters;
            var sum = buffer.Sum();

            buffer = BitConverter.GetBytes(DataSize);
            sum += buffer.Sum();

            buffer = BitConverter.GetBytes(CommandID);
            sum += buffer.Sum();

            buffer = BitConverter.GetBytes(PacketID);
            sum += buffer.Sum();

            buffer = BitConverter.GetBytes(Version);
            sum -= buffer.Sum();

            buffer = BitConverter.GetBytes(FullSize);
            sum -= buffer.Sum();

            sum = (sum >> 0x10) + sum;

            packet.Checksum = Convert.ToUInt16(sum);
        }

        public static void Encrypt(byte[] buffer, int index, int length, byte[] key) {
            int a;
            byte b;

            while (index < length && index < buffer.Length) {
                b = buffer[index];
                a = (b ^ key[index & 0x1F]) << 5;
                b = (byte)(a >> 8);
                buffer[index] = (byte)((b | a) ^ 0xF0);
                index++;
            }
        }

        public static void Decrypt(byte[] buffer, int index, int length, byte[] key) {
            int a;
            byte b;

            while (index < length && index < buffer.Length) {
                b = buffer[index];
                a = b ^ 0xF0;
                b = (byte)(a & 0x1F);
                buffer[index] = (byte)((a >> 5) | (b << 3) ^ key[index & 0x1F]);
                index++;
            }
        }        
    }
}