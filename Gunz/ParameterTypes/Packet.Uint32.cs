using System;
namespace Gunz {
    public partial class Packet {
        public class Uint32 : ParameterBuilder.Parameter {
            public uint Value { get; set; }

            public Uint32() {
                Type = 0x01;
            }

            public override byte[] Serialize() {
                return BitConverter.GetBytes(Value);
            }

            public static implicit operator Uint32(uint val){
                return new Uint32 { Value = val };
            }

            public static implicit operator uint(Uint32 val){
                return val.Value;
            }
        }
    }
}