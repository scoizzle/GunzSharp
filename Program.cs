using Gunz;

namespace ConsoleApplication {
    public class Program {
        public static void Main(string[] args) {
            var packet = new Gunz.Packet {
                IsEncrypted = true
            };
            
            var blob = new Gunz.Packet.Blob<Packet.Uint8>();
            var items = blob.Value;
            
            items.AddRange(new Packet.Uint8[] {
                1, 2, 3, 4, 5, 6
            });

            var buffer = blob.Serialize();
            
            
        }
    }
}
