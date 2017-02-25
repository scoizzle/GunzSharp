using System;
using System.Linq;
using System.Text;

namespace Gunz {
    public partial class Packet {
        public class String : ParameterBuilder.Parameter {
            public string Value { get; set; }

            public String() {
                Type = 0x04;
            }

            public override byte[] Serialize() {
                return Join(
                    BitConverter.GetBytes(Value.Length),
                    Encoding.UTF8.GetBytes(Value)
                ).ToArray();
            }
            
            public override byte[] Serialize_Blob() {
                return Encoding.UTF8.GetBytes(Value);
            }

            public static implicit operator String(string val){
                return new String { Value = val };
            }

            public static implicit operator string(String val){
                return val.Value;
            }
        }
    }
}