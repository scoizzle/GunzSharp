using System;
using Gunz;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
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

        static void Print(bool success) =>
            Console.WriteLine(success ? "Yay!" : "Boo!");
    }
}
