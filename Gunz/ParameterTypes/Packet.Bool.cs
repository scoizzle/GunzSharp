using System;
namespace Gunz {
    public partial class Packet {
        public class Bool : ParameterBuilder.Parameter {
            public Bool() {
                Type = 0x03;
            }
            
            public bool Value { get; set; }

            public override byte[] Serialize() {
                return BitConverter.GetBytes(Value);
            }
        }
    }
}