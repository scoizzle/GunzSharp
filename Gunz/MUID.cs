using System;
using System.Linq;
using System.Text;

namespace Gunz {
    public class MUID {
        public uint High, Low;

        public MUID() : this(0, 0) { }
        public MUID(uint high, uint low) {
            High = high;
            Low = low;
        }
    }
}