using System;
using System.Collections.Generic;

namespace Gunz {
    public partial class Packet {
        public partial class ParameterBuilder {
            public class Parameter {
                public byte Type;

                public virtual byte[] Serialize() { return null; }

                public virtual byte[] Serialize_Blob() { return Serialize(); }

                public static IEnumerable<byte> Join(params byte[][] items) {
                    foreach (var array in items)
                        foreach (var b in array)
                            yield return b;
                }

                public static IEnumerable<byte> Join(IEnumerable<Parameter> items) {
                    foreach (var parameter in items)
                        foreach (var b in parameter.Serialize())
                            yield return b;
                }

                public static IEnumerable<byte> Join_Blob(IEnumerable<Parameter> items) {
                    foreach (var parameter in items)
                        foreach (var b in parameter.Serialize_Blob())
                            yield return b;
                }
            }

            List<Parameter> Params;

            public ParameterBuilder() {
                Params = new List<Parameter>();
            }

            public int Count { 
                get { return Params.Count; }
            }

            public Parameter this[int Index] {
                get { 
                    if (Index > -1 && Index < Count) return Params[Index];
                    else return null;
                }
            }

            public void Number(byte value) {
                Params.Add(
                    new Uint8 { Value = value }
                );
            }

            public void Number(ushort value) {
                Params.Add(
                    new Uint16 { Value = value }
                );
            }

            public void Number(uint value) {
                Params.Add(
                    new Uint32 { Value = value }
                );
            }
            
            public void Number(sbyte value) {
                Params.Add(
                    new int8 { Value = value }
                );
            }

            public void Number(short value) {
                Params.Add(
                    new int16 { Value = value }
                );
            }

            public void Number(int value) {
                Params.Add(
                    new int32 { Value = value }
                );
            }

            public void String(string value) {
                Params.Add(
                    new String { Value = value }
                );
            }

            public void Bool(bool value) {
                Params.Add(
                    new Bool { Value = value }
                );
            }

            public void Float(float value) {
                Params.Add(
                    new Float { Value = value }
                );
            }

            public void FloatTuple(Tuple<float, float, float> value) {
                Params.Add(
                    new FloatTuple { Value = value }
                );
            }

            public static implicit operator byte[](ParameterBuilder This) {
                var stream = new List<byte>();

                foreach (var p in This.Params) {
                    var Value = p.Serialize();
                    stream.Add(p.Type);
                    stream.AddRange(Value);
                }

                return stream.ToArray();
            }
        }
    }
}