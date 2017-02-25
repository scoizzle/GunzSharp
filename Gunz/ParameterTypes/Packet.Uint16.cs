using System;
namespace Gunz {
    public partial class Packet {
        public class Uint16 : ParameterBuilder.Parameter {
            public ushort Value { get; set; }

            public Uint16() {
                Type = 0x0E;
            }

            public override byte[] Serialize() {
                return BitConverter.GetBytes(Value);
            }

            public static implicit operator Uint16(ushort val){
                return new Uint16 { Value = val };
            }

            public static implicit operator ushort(Uint16 val){
                return val.Value;
            }
        }
    }
}