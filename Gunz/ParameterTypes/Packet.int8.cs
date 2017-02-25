using System;
namespace Gunz {
    public partial class Packet {
        public class int8 : ParameterBuilder.Parameter {
            public sbyte Value { get; set; }

            public int8() {
                Type = 0x0B;
            }

            public override byte[] Serialize() {
                return BitConverter.GetBytes(Value);
            }

            public static implicit operator int8(sbyte val){
                return new int8 { Value = val };
            }

            public static implicit operator sbyte(int8 val){
                return val.Value;
            }
        }
    }
}