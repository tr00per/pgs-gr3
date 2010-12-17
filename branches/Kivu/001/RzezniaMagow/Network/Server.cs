using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Net;

namespace RzezniaMagow
{
    internal class DataPool
    {
        volatile public bool dataInPending;
        volatile public byte[] dataIn;

        public DataPool() { dataInPending = false; }
        public DataPool(byte[] data)
        {
            dataIn = data;
            dataInPending = true;
        }
    };

    public class Server
    {
        private bool running = false;
        private int maxClients;

        private TcpListener srv;

        private Dictionary<byte, DataPool> pools;
        private Semaphore poolSem;

        private ServerAbstract sl;
        private Semaphore slSem;

        private Thread defaultThread;
        private List<Thread> threadPool;

        /// <summary>
        /// Creates network server. Server has to be started by startServer().
        /// </summary>
        /// <param name="logFile">External file for server logs.</param>
        public Server(ServerAbstract listener, int port, int maxClients, String logFile)
            : this(listener, port, maxClients)
        {
            FileStream fout = new FileStream(logFile, FileMode.Create);
            StreamWriter sw = new StreamWriter(fout);
            Console.SetOut(sw);
        }

        /// <summary>
        /// Creates server object. Server has to be started by startServer().
        /// </summary>
        public Server(ServerAbstract listener, int port, int maxClients)
        {
            this.sl = listener;
            this.maxClients = maxClients;

            slSem = new Semaphore(1, 1);
            srv = new TcpListener(IPAddress.Any, port);

            pools = new Dictionary<byte, DataPool>();
            poolSem = new Semaphore(1, 1);
            threadPool = new List<Thread>();
        }

        /// <summary>
        /// Start server.
        /// </summary>
        public void startServer()
        {
            if (running)
            {
                return;
            }

            Console.WriteLine("Server: Starting server...");

            running = true;

            //run incomingWait and regular handle in seperate threads
            defaultThread = new Thread(new ThreadStart(this.defaultWait));

            srv.Start();
            defaultThread.Start();
            sl.serverStarted();

            Console.WriteLine("Server: Started.");
        }

        public bool isRunning()
        {
            return running;
        }

        /// <summary>
        /// Stop server.
        /// Disconnect all remaining clients, then kill thread.
        /// </summary>
        public void stopServer()
        {
            if (!running)
            {
                return;
            }

            Console.WriteLine("Server: Stopping server...");
            sl.serverStopped();

            Console.WriteLine("Server: Disconnecting clients...");
            broadcast(Common.PACKET_END, new byte[] { 0 });

            running = false;

            Thread.Sleep(1500);

            Console.WriteLine("Server: Killing threads...");
            foreach (Thread t in threadPool)
            {
                if (t.IsAlive)
                {
                    t.Interrupt();
                }
            }
            if (defaultThread.IsAlive)
            {
                defaultThread.Interrupt();
            }
            srv.Stop();

            Console.WriteLine("Server: Stopped.");
        }


        private void defaultWait()
        {
            Console.WriteLine("Server: Listening to clients...");
            int nextTreadId = 100;

            while (running)
            {
                if (srv.Pending())
                {
                    Console.WriteLine("Server: New connection!");
                    TcpClient cli = srv.AcceptTcpClient();
                    
                    cli.NoDelay = true;

                    ThreadStart starter = delegate { clientHandle(nextTreadId++, cli); };
                    Thread t = new Thread(starter);
                    t.Start();
                    threadPool.Add(t);
                }
            }
        }

        /// <summary>
        /// Child thread of defaultWait.
        /// Accepts connection, does handshaking and the rest of processing.
        /// </summary>
        private void clientHandle(int threadID, TcpClient cli)
        {
            String IP = IPAddress.Parse(((IPEndPoint)cli.Client.RemoteEndPoint).Address.ToString()).ToString();
            Console.WriteLine("Server (" + threadID + "): Client connected: " + IP);
            cli.NoDelay = true;
            NetworkStream io = cli.GetStream();
            if (pools.Count >= maxClients)
            {
                byte[] buf = { Common.PACKET_FAIL, 0, 0 }; //refuse to connect client due to client limit
                io.Write(buf, 0, Common.PACKET_HEADER_SIZE);
                io.Close();
                cli.Client.Close();
                cli.Close();
                Console.WriteLine("Server (" + threadID + "): Client limit reached! " + IP + " disconnected.");
                return;
            }

            Console.WriteLine("Server (" + threadID + "): Handshaking...");
            byte[] bufin = new byte[Common.PACKET_HEADER_SIZE + 17];
            do
            {
                io.Read(bufin, 0, Common.PACKET_HEADER_SIZE + 17);
            } while (!Common.correctPacket(bufin, Common.PACKET_HANDSHAKE));

            //TODO do the proper conversion w/check
            Encoding enc = new UTF8Encoding();
            String nick = enc.GetString(bufin, Common.PACKET_HEADER_SIZE, 16).Trim(new char[] { ' ', '\0' });
            byte avatar = bufin[bufin.Length-1];
            Console.WriteLine("Server (" + threadID + "): " + IP + " -> " + nick);

            byte[] packet = new byte[Common.PACKET_HEADER_SIZE+1];
            slSem.WaitOne(); //this has to be linear
            byte ID = sl.newPlayerConnected(nick, avatar);
            slSem.Release();

            packet[Common.PACKET_HEADER_SIZE] = ID;
            packet[0] = Common.PACKET_HANDSHAKE;
            BitConverter.GetBytes(Common.checksum(packet)).CopyTo(packet, 1);

            io.Write(packet, 0, packet.Length);

            poolSem.WaitOne();
            pools.Add(ID, new DataPool());
            poolSem.Release();

            Console.WriteLine("Server (" + threadID + "): " + nick + " (" + ID + ") is ready.");
            sl.sendMessage(nick + " (" + ID + ") is ready.");

            int pending = 0;
            bool connected = true;
            while (running && connected)
            {
                if (pools[ID].dataInPending)
                {
                    if (pools[ID].dataIn[0] == Common.PACKET_BEGIN)
                    {
                        DataPool dp = pools[ID];
                        bool received = false;
                        byte[] buf = new byte[Common.PACKET_HEADER_SIZE+1];

                        Console.WriteLine("Server (" + threadID + "): Round begins...");
                        while (!received)
                        {
                            Console.WriteLine("Server (" + threadID + "): Sending...");
                            io.Write(dp.dataIn, 0, dp.dataIn.Length);
                            io.Read(buf, 0, buf.Length);
                            if (buf[0] == Common.PACKET_OK)
                            {
                                Console.WriteLine("Server (" + threadID + "): OK!");
                                received = true;
                            }
                            else
                            {
                                Thread.Sleep(20);
                            }
                        }
                        Console.WriteLine("Server (" + threadID + "): Round begin!");
                    }
                    else
                    {
                        io.Write(pools[ID].dataIn, 0, pools[ID].dataIn.Length);
                    }
                    pools[ID] = new DataPool();
                }

                if (cli.Connected && (pending = cli.Available) > 0)
                {
                    packet = new byte[Common.PACKET_HEADER_SIZE];
                    io.Read(packet, 0, Common.PACKET_HEADER_SIZE);
					byte packetType = packet[0];
					ushort packetSize = BitConverter.ToUInt16(packet, 1);

                    if (!Common.correctPacket(packet, Common.PACKET_COMMON | Common.PACKET_END))
                    {
                        Console.WriteLine("Server (" + threadID + "): Incorrect packet: " + packet[0] + ", " + packet[1] + ".");
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
                        Console.WriteLine("Server (" + threadID + "): Incorrect packet size: " + packetType + ", " + packetSize + ".");
                        continue;
                    }

                    //client says goodbye
                    if (packetType == Common.PACKET_END)
                    {
                        slSem.WaitOne(); //this has to be linear
                        sl.playerParted(ID);
                        slSem.Release();
                        connected = false;
                    }
                    else if (packetType == Common.PACKET_COMMON)
                    {
                        sl.playerHandle(packet);
                    }
                }
            }

            cli.Client.Close();
            cli.Close();
            Console.WriteLine("Server (" + threadID + "): " + nick + " (" + ID + ") disconnected.");
        }

        /// <summary>
        /// Sends concurrently broadcast message.
        /// </summary>
        /// <param name="type">Packet type</param>
        /// <param name="data">Content of the packet (without header)</param>
        public void broadcast(byte type, byte[] data)
        {
            byte[] packet = new byte[data.Length + Common.PACKET_HEADER_SIZE];
            data.CopyTo(packet, Common.PACKET_HEADER_SIZE);
            packet[0] = type;
            BitConverter.GetBytes(Common.checksum(packet)).CopyTo(packet, 1);

            poolSem.WaitOne();
            List<byte> Keys = new List<byte>(pools.Keys);
            foreach (byte id in Keys)
            {
                pools[id] = new DataPool(packet);
            }
            poolSem.Release();
        }
    }
}
