using System;
using Gunz;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            TestDecode();
        }

        static void Print(bool success) =>
            Console.WriteLine(success ? "Yay!" : "Boo!");

        static void Test() {
            Packet packet = new Packet();

            packet.SetPacketType(0x65);
            packet.SetPacketID(1);
            packet.SetCommandID(0x4C9);
            packet.WriteULong(0);
            packet.WriteULong(1);
            packet.WriteULong(0);
            packet.WriteULong(5);
            packet.WriteString("text");

            packet.Encrypt(Packet.DefaultKey);
            packet.Decrypt(Packet.DefaultKey);
            
            packet.ResetIndex();

            Print(packet.GetPacketType() == 0x65);
            Print(packet.GetPacketID() == 1);
            Print(packet.GetCommandID() == 0x4c9);
            Print(packet.GetULong() == 0);
            Print(packet.GetULong() == 1);
            Print(packet.GetULong() == 0);
            Print(packet.GetULong() == 5);
            Print(packet.GetString() == "text");
        }

        static void TestDecode() {
            var buffer = new byte[] { 0x0A, 0x00, 0x1A, 0x00, 0x48, 0x2B, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x12, 0x00, 0x00, 0x00, 0x31, 0x64, 0x18 };

            var packet = new Packet(buffer);
            var uid_server_low = packet.GetUInt();
            var uid_client_high = packet.GetUInt();
            var uid_client_low = packet.GetUInt();
            var time_stamp = packet.GetUInt();

            Packet.MadeSeedKey(new MUID(0, uid_server_low), new MUID(uid_client_high, uid_client_low), time_stamp);
        }
    }
}
