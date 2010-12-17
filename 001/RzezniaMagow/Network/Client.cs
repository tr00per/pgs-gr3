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
        volatile private bool running;
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
        public void connect(string address, int port, string nick, byte avatar)
        {
            cli = new TcpClient();
            cli.NoDelay = true;

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
                return;
            }

            statusCallback("Starting client network thread...");
            ThreadStart starter = delegate { clientThread(cli, nick, avatar); };
            listener = new Thread(starter);
            listener.Start();
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

            Thread.Sleep(1500);
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
        private void clientThread(TcpClient cli, string nick, byte avatar)
        {
            statusCallback("Signing in...");
            cli.NoDelay = true;
            NetworkStream io = cli.GetStream();
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
                clientReady(id, nick, avatar);
                running = true;
                statusCallback("Ready!");
            }
            else
            {
                statusCallback("Holy cow...");
                //running is false
            }

            int pending = 0;
            bool enteredGame = false;
            while (running)
            {
                if (cli.Connected && (pending = cli.Available) > 0)
                {
                    packet = new byte[Common.PACKET_HEADER_SIZE];
                    io.Read(packet, 0, Common.PACKET_HEADER_SIZE);
					byte packetType = packet[0];
					byte packetSize = packet[1];

                    if (!Common.correctPacket(packet, Common.PACKET_COMMON | Common.PACKET_SRVMSG | Common.PACKET_END | Common.PACKET_BEGIN))
                    {
                        statusCallback("Incorrect packet: " + packet[0] + ", " + packet[1] + ".");
						if (pending > Common.PACKET_HEADER_SIZE)
						{
							packet = new byte[pending - Common.PACKET_HEADER_SIZE];
							io.Read(packet, 0, pending - Common.PACKET_HEADER_SIZE);
						}
                        continue;
                    }
					
					packet = new byte[packetSize];
					int bytesRead = io.Read(packet, 0, packetSize);
                    if (bytesRead != packetSize)
                    {
                        statusCallback("Incorrect packet size: " + packetType + ", " + packetSize + ".");
                        continue;
                    }
					
                    if (enteredGame && packetType == Common.PACKET_COMMON)
                    {
                        listenerSem.WaitOne();
                        updateArrived(packet);
                        listenerSem.Release();
                    }
                    else if (packetType == Common.PACKET_SRVMSG)
                    {
                        string msg = enc.GetString(packet, 0, packetSize);
                        statusCallback("from Server: " + msg.Trim());
                    }
                    else if (packetType == Common.PACKET_BEGIN)
                    {
                        listenerSem.WaitOne();
                        sendUpdate(new byte[1] { id }, Common.PACKET_OK);
						statusCallback("ROUND BEGUN!");
						sendUpdate(new byte[1] { id }, Common.PACKET_OK); //when code was doubled - it worked ;)
                        beginRound(packet);
                        enteredGame = true;
                        listenerSem.Release();
                    }
                    else if (packetType == Common.PACKET_END) //server shutdown or kicked out
                    {
                        statusCallback("Server disconnected.");
                        running = false;
                        cli.Client.Disconnect(true);
                    }
                }
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

        private void asyncWrite(IAsyncResult arg)
        {
            NetworkStream io = (NetworkStream)arg.AsyncState;
            io.EndWrite(arg);
        }

        /// <summary>
        /// Sends given data to the server asynchronously.
        /// </summary>
        /// <param name="data">Serialized data, without packet header.</param>
        public void sendUpdate(byte[] data)
        {
            sendUpdate(data, Common.PACKET_COMMON);
        }

        public void sendUpdate(byte[] data, byte type)
        {
            byte[] packet = new byte[data.Length + Common.PACKET_HEADER_SIZE];
            data.CopyTo(packet, Common.PACKET_HEADER_SIZE);
            packet[0] = type;
            packet[1] = Common.checksum(packet);

            if (running)
            {
                NetworkStream io = cli.GetStream();

                io.BeginWrite(packet, 0, packet.Length, new AsyncCallback(asyncWrite), io);
            }
        }

        /// <summary>
        /// Callback. When common packet arrives.
        /// </summary>
        abstract protected void updateArrived(byte[] data);

        /// <summary>
        /// Callback. When new round begins.
        /// </summary>
        abstract protected void beginRound(byte[] data);

        abstract protected void clientReady(byte id, string nick, byte avatar);
    }
}
