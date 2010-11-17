﻿using System;
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
        public static byte PACKET_HANDSHAKE = 4;//dodanie nowego gracza
        public static byte PACKET_BEGIN = 8;//rozpoczecie rundy
        public static byte PACKET_COMMON = 16;//pakiety wysyłane w czasie gry
        public static byte PACKET_END = 32;
        public static byte PACKET_SRVMSG = 64;

        public static int PACKET_HEADER_SIZE = 2;

        public static bool correctPacket(byte[] data, byte packetType)
        {
            //return (data[0] & packetType) > 0 && (data[1] == checksum(data));
            return (data[0] & packetType) > 0;
        }

        public static bool correctPacket(byte[] data, int packetType)
        {
            return correctPacket(data, (byte)packetType);
        }

        public static byte checksum(byte[] data)
        {
            //skip header (packet type and checksum field)
            return rawChecksum(data, Common.PACKET_HEADER_SIZE);
        }

        internal static byte rawChecksum(byte[] data, int offset)
        {
            /*
            uint cs = 0;
            IEnumerable<byte> bytes = data.Skip(offset);
            foreach (byte b in bytes)
            {
                cs += b;
            }

            return (byte)(cs & 255);
            */
            return 0;
        }

        internal static void asyncWrite(IAsyncResult arg)
        {
            NetworkStream io = (NetworkStream)arg.AsyncState;
            io.EndWrite(arg);

        }
    }
}