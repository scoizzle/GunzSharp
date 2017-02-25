namespace System {
    static class ByteArrayExtensions {
        public static uint Sum(this byte[] items) {
            uint sum = 0;
            foreach (var b in items) {
                sum += b;
            }
            return sum;
        }
    }
}