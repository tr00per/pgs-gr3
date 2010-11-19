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
        public bool dataInPending;
        public byte[] dataIn;

        public DataPool() { dataInPending = false; }
        public DataPool(byte[] data)
        {
            dataInPending = true;
            dataIn = new byte[data.Length];
            data.CopyTo(dataIn, 0);
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

            Console.WriteLine("Starting server...");

            running = true;

            //run incomingWait and regular handle in seperate threads
            defaultThread = new Thread(new ThreadStart(this.defaultWait));

            srv.Start();
            defaultThread.Start();
            sl.serverStarted();

            Console.WriteLine("Started.");
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

            Console.WriteLine("Stopping server...");
            sl.serverStopped();

            Console.WriteLine("Disconnecting clients...");
            broadcast(Common.PACKET_END, new byte[] { 0 });

            running = false;

            Console.WriteLine("Killing threads...");
            Thread.Sleep(100);
            if (defaultThread.IsAlive)
            {
                defaultThread.Interrupt();
            }
            foreach (Thread t in threadPool)
            {
                if (t.IsAlive)
                {
                    t.Interrupt();
                }
            }
            srv.Stop();

            Console.WriteLine("Stopped.");
        }


        private void defaultWait()
        {
            Console.WriteLine("Listening to clients...");

            while (running)
            {
                if (srv.Pending())
                {
                    Console.WriteLine("New connection!");
                    TcpClient cli = srv.AcceptTcpClient();
                    cli.NoDelay = true;

                    ThreadStart starter = delegate { clientHandle(cli); };
                    Thread t = new Thread(starter);
                    t.Start();
                    threadPool.Add(t);
                }
                Thread.Sleep(5);
            }
        }

        /// <summary>
        /// Child thread of defaultWait.
        /// Accepts connection, does handshaking and the rest of processing.
        /// </summary>
        private void clientHandle(TcpClient cli)
        {
            String IP = IPAddress.Parse(((IPEndPoint)cli.Client.RemoteEndPoint).Address.ToString()).ToString();
            Console.WriteLine("Client connected: " + IP);
            NetworkStream io = cli.GetStream();
            if (pools.Count >= maxClients)
            {
                byte[] buf = { Common.PACKET_FAIL, 0 }; //refuse to connect client due to client limit
                io.Write(buf, 0, Common.PACKET_HEADER_SIZE);
                io.Close();
                cli.Client.Close();
                cli.Close();
                Console.WriteLine("Client limit reached! " + IP + " disconnected.");
                return;
            }

            Console.WriteLine("Handshaking...");
            byte[] bufin = new byte[19];
            do
            {
                io.Read(bufin, 0, 19);
            } while (!Common.correctPacket(bufin, Common.PACKET_HANDSHAKE));

            //TODO do the proper conversion w/check
            Encoding enc = new UTF8Encoding();
            String nick = enc.GetString(bufin, Common.PACKET_HEADER_SIZE, 16).Trim(new char[] { ' ', '\0' });
            byte avatar = bufin[18];
            Console.WriteLine(IP + " -> " + nick);

            byte[] packet = new byte[3];
            slSem.WaitOne(); //this has to be linear
            byte ID = sl.newPlayerConnected(nick, avatar);
            slSem.Release();

            packet[Common.PACKET_HEADER_SIZE] = ID;
            packet[1] = Common.checksum(packet);
            packet[0] = Common.PACKET_HANDSHAKE;

            io.Write(packet, 0, 3);

            poolSem.WaitOne();
            pools.Add(ID, new DataPool());
            poolSem.Release();

            Console.WriteLine(nick + " (" + ID + ") is ready.");
            sl.sendMessage(nick + " (" + ID + ") is ready.");
            Game.czyNowaRunda = true;

            int pending = 0;

            bool connected = true;

            while (running && connected)
            {
                if (pools[ID].dataInPending)
                {
                    io.Write(pools[ID].dataIn, 0, pools[ID].dataIn.Length);
                    pools[ID] = new DataPool();
                }

                if (cli.Connected && (pending = cli.Available) > 0)
                {
                    packet = new byte[pending];
                    io.Read(packet, 0, pending);

                    if (!Common.correctPacket(packet, Common.PACKET_COMMON | Common.PACKET_END))
                    {
                        Console.WriteLine("Incorrect packet: " + packet[0] + ", " + packet[1] + ".");
                        continue;
                    }

                    //client says goodbye
                    if (packet[0] == Common.PACKET_END)
                    {
                        slSem.WaitOne(); //this has to be linear
                        sl.playerParted(ID);
                        slSem.Release();
                        connected = false;
                    }
                    else if (packet[0] == Common.PACKET_COMMON)
                    {
                        sl.playerHandle(packet.Skip(Common.PACKET_HEADER_SIZE).ToArray());
                    }
                }
            }

            cli.Client.Close();
            cli.Close();
            Console.WriteLine(nick + " (" + ID +  ") disconnected.");
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
            packet[1] = Common.checksum(packet);

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
