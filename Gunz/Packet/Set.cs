using System;
using System.Linq;
using System.Text;

namespace Gunz {
    public partial class Packet {
        public unsafe void SetPacketType(ushort type) {
            fixed(byte* ptr = buffer) {
                PacketHeader* hdr = (PacketHeader*)(ptr);

                hdr->Type = type;
            }
        }

        private unsafe void SetPacketSize(ushort size) {
            fixed(byte* ptr = buffer) {
                PacketHeader* hdr = (PacketHeader*)(ptr);

                hdr->Size = size;
            }
        }
        
        public unsafe void SetChecksum(ushort checksum) {
            fixed(byte* ptr = buffer) {
                PacketHeader* hdr = (PacketHeader*)(ptr);

                hdr->Checksum = checksum;
            }
        }

        public ushort CalculateChecksum(ushort packet_size) {
            ushort checksum = 0;

            for (var i = 0; i < 4; i++)
                checksum -= buffer[i];

            for (var i = 6; i < packet_size; i++)
                checksum += buffer[i];

            return checksum;
        }

        private unsafe void SetDataSize(ushort data_size) {
            fixed(byte* ptr = buffer) {
                DataHeader* hdr = (DataHeader*)(ptr + HEADER_SIZE);

                hdr->DataSize = data_size;
            }
        }

        public unsafe void SetCommandID(ushort command_id) {
            fixed(byte* ptr = buffer) {
                DataHeader* hdr = (DataHeader*)(ptr + HEADER_SIZE);

                hdr->CommandID = command_id;
            }
        }

        public unsafe void SetPacketID(byte packet_id) {
            fixed(byte* ptr = buffer) {
                DataHeader* hdr = (DataHeader*)(ptr + HEADER_SIZE);

                hdr->PacketID = packet_id;
            }
        }

        public void WriteBlob(byte[] blob) {
            Array.Copy(blob, 0, buffer, index, blob.Length);
            UpdateSize((ushort)(blob.Length));
        }

        public void WriteBlob(byte[] blob, ushort update_size) {
            Array.Copy(blob, 0, buffer, index, blob.Length);
            UpdateSize(update_size);
        }

        public void WriteBool(bool value) {
            buffer[index] = (byte)(value ? 1 : 0);
            UpdateSize(1);
        }

        public void WriteByte(byte value) {
            buffer[index] = value;
            UpdateSize(1);
        }

        public void WriteSByte(sbyte value) {
            buffer[index] = (byte)(value);
            UpdateSize(1);
        }

        public void WriteDouble(double value) =>
            WriteBlob(BitConverter.GetBytes(value));

        public void WriteFloat(float value) =>
            WriteBlob(BitConverter.GetBytes(value));

        public void WriteShort(short value) =>
            WriteBlob(BitConverter.GetBytes(value));

        public void WriteUShort(ushort value) =>
            WriteBlob(BitConverter.GetBytes(value));

        public void WriteInt(int value) =>
            WriteBlob(BitConverter.GetBytes(value));

        public void WriteUInt(uint value) =>
            WriteBlob(BitConverter.GetBytes(value));
            
        public void WriteLong(long value) =>
            WriteBlob(BitConverter.GetBytes(value));
            
        public void WriteULong(ulong value) =>
            WriteBlob(BitConverter.GetBytes(value));

        public void WriteString(string value) {
            byte[] bytes = Encoding.ASCII.GetBytes(value);
            ushort length = (ushort)(bytes.Length + 2);

            WriteUShort(length);
            WriteBlob(bytes, length);
        }

        private unsafe void UpdateSize(ushort size) {
            index += size;
            
            fixed(byte* ptr = buffer) {
                PacketHeader* pkt = (PacketHeader*)(ptr);
                DataHeader* dt = (DataHeader*)(ptr + HEADER_SIZE);

                dt->DataSize += size;
                pkt->Size = (ushort)(HEADER_SIZE + dt->DataSize);
            }
        }
    }
}