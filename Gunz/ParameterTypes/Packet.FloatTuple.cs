using System;
using System.Linq;

namespace Gunz {
    public partial class Packet {
        public class FloatTuple : ParameterBuilder.Parameter {
            public FloatTuple() {
                Type = 0x02;
            }

            public FloatTuple(float x, float y, float z) : this() {
                Value = new Tuple<float, float, float>(x, y, z);
            }
            
            public Tuple<float, float, float> Value { get; set; }

            public override byte[] Serialize() {
                return Join(
                    BitConverter.GetBytes(Value.Item1),
                    BitConverter.GetBytes(Value.Item2),
                    BitConverter.GetBytes(Value.Item3)
                ).ToArray();
            }
        }
    }
}