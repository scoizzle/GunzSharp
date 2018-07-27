using System;
using System.Linq;
using System.Text;

namespace Gunz {
    public partial class Packet {
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