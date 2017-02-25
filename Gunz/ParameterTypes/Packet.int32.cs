using System;
namespace Gunz {
    public partial class Packet {
        public class int32 : ParameterBuilder.Parameter {
            public int32() {
                Type = 0x00;
            }
            
            public int Value { get; set; }

            public override byte[] Serialize() {
                return BitConverter.GetBytes(Value);
            }

            public static implicit operator int32(int val) {
                return new int32 { Value = val };
            }

            public static implicit operator int(int32 val){
                return val.Value;
            }
        }
    }
}