using System;
namespace Gunz {
    public partial class Packet {
        public class Float : ParameterBuilder.Parameter {
            public Float() {
                Type = 0x02;
            }

            public float Value { get; set; }

            public override byte[] Serialize() {
                return BitConverter.GetBytes(Value);
            }
        }
    }
}