using System;
using System.Linq;
using System.Text;

namespace Gunz {
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

    public class Packet {
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

        public unsafe ushort GetPacketType() {
            fixed(byte* ptr = buffer)
                return ((PacketHeader*)(ptr))->Type;
        }

        public unsafe ushort GetPacketSize() {
            fixed(byte* ptr = buffer)
                return ((PacketHeader*)(ptr))->Size;
        }


        public unsafe ushort GetChecksum() {
            fixed(byte* ptr = buffer)
                return ((PacketHeader*)(ptr))->Checksum;
        }

        public unsafe ushort GetDataSize() {
            fixed(byte* ptr = buffer)
                return ((DataHeader*)(ptr + HEADER_SIZE))->DataSize;
        }

        public unsafe ushort GetCommandID() {
            fixed(byte* ptr = buffer)
                return ((DataHeader*)(ptr + HEADER_SIZE))->CommandID;
        }

        public unsafe byte GetPacketID() {
            fixed(byte* ptr = buffer)
                return ((DataHeader*)(ptr + HEADER_SIZE))->PacketID;
        }

        public unsafe bool GetBool() {
            var value = default(bool);
            var size = sizeof(bool);

            fixed(byte* ptr = buffer) 
                value = *(bool*)(ptr + index);

            index += (ushort)(size);
            return value;
        }

        public byte GetByte() =>
            buffer[index++];
            
        public sbyte GetSByte() =>
            (sbyte)(buffer[index++]);

        public unsafe short GetShort() {
            var value = default(short);
            var size = sizeof(short);

            fixed(byte* ptr = buffer) 
                value = *(short*)(ptr + index);

            index += (ushort)(size);
            return value;
        }

        public unsafe ushort GetUShort() {
            var value = default(ushort);
            var size = sizeof(ushort);

            fixed(byte* ptr = buffer) 
                value = *(ushort*)(ptr + index);

            index += (ushort)(size);
            return value;
        }

        public unsafe int GetInt() {
            var value = default(int);
            var size = sizeof(int);

            fixed(byte* ptr = buffer) 
                value = *(int*)(ptr + index);

            index += (ushort)(size);
            return value;
        }

        public unsafe uint GetUInt() {
            var value = default(uint);
            var size = sizeof(uint);

            fixed(byte* ptr = buffer) 
                value = *(uint*)(ptr + index);

            index += (ushort)(size);
            return value;
        }

        public unsafe long GetLong() {
            var value = default(long);
            var size = sizeof(long);

            fixed(byte* ptr = buffer) 
                value = *(long*)(ptr + index);

            index += (ushort)(size);
            return value;
        }

        public unsafe ulong GetULong() {
            var value = default(ulong);
            var size = sizeof(ulong);

            fixed(byte* ptr = buffer) 
                value = *(ulong*)(ptr + index);

            index += (ushort)(size);
            return value;
        }
        
        public unsafe float GetFloat() {
            var value = default(float);
            var size = sizeof(float);

            fixed(byte* ptr = buffer) 
                value = *(float*)(ptr + index);

            index += (ushort)(size);
            return value;
        }
        
        public unsafe double GetDouble() {
            var value = default(double);
            var size = sizeof(double);

            fixed(byte* ptr = buffer) 
                value = *(double*)(ptr + index);

            index += (ushort)(size);
            return value;
        }

        public unsafe byte[] GetBlob(ushort size) {
            var value = new byte[size];
            Array.Copy(buffer, index, value, 0, size);

            index += size;
            return value;
        }

        public unsafe string GetString() {
            var length = GetUShort() - 2;
            var str = Encoding.ASCII.GetString(buffer, index, length);

            index += (ushort)(length);
            return str;
        }
    }
}