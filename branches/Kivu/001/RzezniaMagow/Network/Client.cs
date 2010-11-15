using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace RzezniaMagow
{
    public delegate void StatusCallback(string msg);

    abstract public class Client
    {
        private StatusCallback statusCallback;
        private TcpClient cli;
        private Thread listener;
        protected Semaphore listenerSem;
        private bool running;
        private byte id;

        /// <summary>
        /// Create new network client. It has to be connected manually.
        /// </summary>
        /// <param name="statusCB">Callback for client status reports and server messages.</param>
        public Client(StatusCallback statusCB)
        {
            statusCallback = statusCB;
            listenerSem = new Semaphore(1, 1);
            id = 0;
        }

        /// <summary>
        /// Get id returnet by server during handshaking.
        /// It's immutable and can't be accessed otherwise.
        /// </summary>
        protected byte getID()
        {
            return id;
        }

        /// <summary>
        /// Connect and handshake with given server.
        /// </summary>
        /// <param name="nick">Screen name of a player. Max 16 chars.</param>
        /// <param name="avatar">ID of chosen avatar.</param>
        /// <returns>If client si successfuly connected.</returns>
        public bool connect(string address, int port, string nick, byte avatar)
        {
            cli = new TcpClient();

            IPAddress ip = IPAddress.Parse(address);
            statusCallback("Connecting to " + address + ":" + port.ToString());
            cli.Connect(ip, port);
            
            statusCallback("Connected. Handshaking...");
            NetworkStream io = cli.GetStream();
            Thread.Sleep(500);
            if (cli.Available > 0)
            {
                byte[] pckt = new byte[2];
                io.Read(pckt, 0, 2);
                if (Common.correctPacket(pckt, Common.PACKET_FAIL))
                {
                    statusCallback("Failed. Probably server is full...");
                }
                else
                {
                    statusCallback("Failed! Something went terribly wrong...");
                }
                return false;
            }
            
            //TODO do the proper conversion w/check
            Encoding enc = new UTF8Encoding();
            byte[] packet = new byte[19];
            enc.GetBytes(nick, 0, nick.Length, packet, Common.PACKET_HEADER_SIZE);
            packet[18] = avatar;
            packet[0] = Common.PACKET_HANDSHAKE;
            packet[1] = Common.checksum(packet);
            io.Write(packet, 0, 19);

            packet = new byte[3];
            io.Read(packet, 0, 3);
            if (Common.correctPacket(packet, Common.PACKET_HANDSHAKE))
            {
                id = packet[Common.PACKET_HEADER_SIZE];
                Game.zawodnik = new Gracz(8, nick, avatar);
               
            }
            else
            {
                statusCallback("Holy cow...");
                return false;
            }

            statusCallback("Done. Starting broadcast listener...");
            running = true;
            listener = new Thread(new ThreadStart(listen));
            listener.Start();

            statusCallback("Ready!");

            return true;
        }

        public bool isRunning()
        {
            return running;
        }

        /// <summary>
        /// Say goodbye to the server and disconnect.
        /// </summary>
        public void disconnect()
        {
            running = false;

            sayGoodbye();

            Thread.Sleep(10);
            if (listener.IsAlive)
            {
                listener.Interrupt();
            }

            cli.Client.Close();
            cli.Close();
        }

        /// <summary>
        /// This runs in a seperate thread.
        /// Listen for incoming packets from server and run appropriate callback.
        /// Incorrect packets are silently ignored.
        /// </summary>
        private void listen()
        {
            int pending = 0;

            while (running)
            {
                if (cli.Connected && (pending = cli.Available) > 0)
                {
                    byte[] packet = new byte[pending];
                    NetworkStream io = cli.GetStream();
                    io.Read(packet, 0, pending);

                    if (!Common.correctPacket(packet, Common.PACKET_COMMON | Common.PACKET_SRVMSG | Common.PACKET_END | Common.PACKET_BEGIN))
                    {
                        statusCallback("Incorrect packet: " + packet[0] + ", " + packet[1] + ".");
                        continue;
                    }

                    if (packet[0] == Common.PACKET_COMMON)
                    {
                        listenerSem.WaitOne();
                        updateArrived(packet.Skip(Common.PACKET_HEADER_SIZE).ToArray());
                        listenerSem.Release();
                    }
                    else if (packet[0] == Common.PACKET_SRVMSG)
                    {
                        //TODO do the proper conversion w/check
                        Encoding enc = new UTF8Encoding();
                        string msg = enc.GetString(packet, Common.PACKET_HEADER_SIZE, pending - Common.PACKET_HEADER_SIZE);
                        statusCallback(msg.Trim());
                    }
                    else if (packet[0] == Common.PACKET_BEGIN)
                    {
                        listenerSem.WaitOne();
                        beginRound(packet.Skip(Common.PACKET_HEADER_SIZE).ToArray());
                        listenerSem.Release();
                    }
                    else if (packet[0] == Common.PACKET_END) //server shutdown or kicked out
                    {
                        statusCallback("Server disconnected.");
                        running = false;
                        cli.Client.Disconnect(true);
                    }
                }
                //Thread.Sleep(5); -- works fine without it :]
            }
        }

        /// <summary>
        /// Helper. Send Goodbye to server.
        /// </summary>
        private void sayGoodbye()
        {
            byte[] goodbye = new byte[Common.PACKET_HEADER_SIZE + 1];
            goodbye[Common.PACKET_HEADER_SIZE] = id;
            goodbye[0] = Common.PACKET_END;
            goodbye[1] = Common.checksum(goodbye);
            cli.GetStream().Write(goodbye, 0, Common.PACKET_HEADER_SIZE);
        }

        /// <summary>
        /// Sends given data to the server asynchronously.
        /// </summary>
        /// <param name="data">Serialized data, without packet header.</param>
        public void sendUpdate(byte[] data)
        {
            byte[] packet = new byte[data.Length + Common.PACKET_HEADER_SIZE];
            data.CopyTo(packet, Common.PACKET_HEADER_SIZE);
            packet[0] = Common.PACKET_COMMON;
            packet[1] = Common.checksum(packet);
            //System.Console.WriteLine(packet[14].ToString());
            NetworkStream io = cli.GetStream();
            io.BeginWrite(packet, 0, packet.Length, new AsyncCallback(Common.asyncWrite), io);
        }

        /// <summary>
        /// Callback. When common packet arrives.
        /// </summary>
        abstract protected void updateArrived(byte[] data);

        /// <summary>
        /// Callback. When new round begins.
        /// </summary>
        abstract protected void beginRound(byte[] data);
    }
}
