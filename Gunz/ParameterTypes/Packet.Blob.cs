using System;
using System.Linq;
using System.Collections.Generic;

namespace Gunz {
    public partial class Packet {
        public class Blob<T> : ParameterBuilder.Parameter where T: ParameterBuilder.Parameter {
            public Blob() {
                Type = 0xA;
                Value = new List<T>();
            }
            
            public List<T> Value { get; set; }

            public override byte[] Serialize() {
                var serialKiller = Join_Blob(Value).ToArray();

                var elemCount = Value.Count;
                var elemSize = serialKiller.Length / elemCount;
                var totalSize = serialKiller.Length + 8;

                return Join(
                     BitConverter.GetBytes(totalSize),
                     BitConverter.GetBytes(elemSize),
                     BitConverter.GetBytes(elemCount),
                     serialKiller
                ).ToArray();
            }
        }
    }
}