using System;
namespace Gunz {
    public partial class Packet {
        public class int16 : ParameterBuilder.Parameter {
            public short Value { get; set; }

            public int16() {
                Type = 0x0D;
            }

            public override byte[] Serialize() {
                return BitConverter.GetBytes(Value);
            }

            public static implicit operator int16(short val){
                return new int16 { Value = val };
            }

            public static implicit operator short(int16 val){
                return val.Value;
            }
        }
    }
}