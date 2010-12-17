using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace RzezniaMagow
{
    abstract public class Common
    {
        public static byte PACKET_OK = 1;
        public static byte PACKET_FAIL = 2;
        public static byte PACKET_HANDSHAKE = 4; //new player connection
        public static byte PACKET_BEGIN = 8; //new round
        public static byte PACKET_COMMON = 16; //in-game packet
        public static byte PACKET_END = 32;
        public static byte PACKET_SRVMSG = 64;

        public static int PACKET_HEADER_SIZE = 3;

        public static bool correctPacket(byte[] data, byte packetType)
        {
            return (data[0] & packetType) > 0;
        }

        public static bool correctPacket(byte[] data, int packetType)
        {
            return correctPacket(data, (byte)packetType);
        }

        public static ushort checksum(byte[] data)
        {
            //skip header (packet type and checksum/size field)
			return (ushort)(data.Length - PACKET_HEADER_SIZE);
        }
    }
}
