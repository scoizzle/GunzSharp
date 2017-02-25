using System;
namespace Gunz {
    public partial class Packet {
        public class Uint8 : ParameterBuilder.Parameter {
            public byte Value { get; set; }

            public Uint8() {
                Type = 0x0C;
            }

            public override byte[] Serialize() {
                return new byte[] { Value };
            }

            public static implicit operator Uint8(byte val){
                return new Uint8 { Value = val };
            }

            public static implicit operator byte(Uint8 val){
                return val.Value;
            }
        }
    }
}