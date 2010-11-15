using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Net;

namespace Network
{
    public class Server
    {
        private bool running = false;
        private int maxClients;
		
        private TcpListener srv;

        private List<TcpClient> clients;
        private Semaphore clientsSem;
        
        private ServerAbstract sl;
        private Semaphore slSem;

        private Thread incomingThread, defaultThread;

        public Server(ServerAbstract listener, int port, int maxClients, String logFile)
            : this(listener, port, maxClients)
        {
            FileStream fout = new FileStream(logFile, FileMode.Create);
            StreamWriter sw = new StreamWriter(fout);
            Console.SetOut(sw);
     }

        public Server(ServerAbstract listener, int port, int maxClients)
        {
            this.sl = listener;
            this.maxClients = maxClients;

            clientsSem = new Semaphore(1, 1);
            slSem = new Semaphore(1, 1);

            clients = new List<TcpClient>();
            srv = new TcpListener(IPAddress.Any, port);
        }

        public void startServer()
        {
            if (running)
            {
                return;
            }

            Console.WriteLine("Starting server...");

            running = true;

            //run incomingWait and regular handle in seperate threads
            incomingThread = new Thread(new ThreadStart(this.incomingWait));
            defaultThread = new Thread(new ThreadStart(this.defaultHook));
            incomingThread.IsBackground = true;

            srv.Start();
            incomingThread.Start();
            defaultThread.Start();

            Console.WriteLine("Started.");
        }

        public bool isRunning()
        {
            return running;
        }

        public void stopServer()
        {
            if (!running)
            {
                return;
            }

            Console.WriteLine("Stopping server...");
            running = false;

            //stop running threads and force connections to close
            clientsSem.WaitOne();
            foreach (TcpClient cli in clients)
            {
                cli.Client.Close();
                cli.Close();
            }
            clientsSem.Release();
            Thread.Sleep(100);
            if (incomingThread.IsAlive)
            {
                incomingThread.Interrupt();
            }
            if (defaultThread.IsAlive)
            {
                defaultThread.Interrupt();
            }
            srv.Stop();

            Console.WriteLine("Stopped.");
        }

        private void incomingWait()
        {
            Console.WriteLine("Awaiting connections...");

            while (running)
            {
                if (srv.Pending())
                {
                    srv.BeginAcceptTcpClient(new AsyncCallback(incomingAccept), srv);
                }
                Thread.Sleep(50);
            }
        }

        private void incomingAccept(IAsyncResult arg)
        {
            TcpListener server = (TcpListener)arg.AsyncState;
            TcpClient cli = server.EndAcceptTcpClient(arg);

            String IP = IPAddress.Parse(((IPEndPoint)cli.Client.RemoteEndPoint).Address.ToString()).ToString();
            Console.WriteLine("Client connected: " + IP);
            NetworkStream io = cli.GetStream();
            if (clients.Count >= maxClients)
            {
                byte[] buf = {Common.PACKET_FAIL, 0}; //refuse to connect client due to client limit
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
            String nick = enc.GetString(bufin, Common.PACKET_HEADER_SIZE, 16).Trim(new char[] {' ', '\0'});
            byte avatar = bufin[18];
            Console.WriteLine(IP + " -> " + nick);

            byte[] packet = new byte[3];
            slSem.WaitOne(); //this has to be linear
            packet[Common.PACKET_HEADER_SIZE] = sl.newPlayerConnected(nick, avatar);
            slSem.Release();

            packet[1] = Common.checksum(packet);
            packet[0] = Common.PACKET_HANDSHAKE;

            io.Write(packet, 0, 3);

            clientsSem.WaitOne();
            clients.Add(cli);
            clientsSem.Release();

            Console.WriteLine(nick + " is ready.");
            sl.sendMessage(nick + " is ready.");
        }

        private void defaultHook()
        {
            Console.WriteLine("Listening to clients...");
            int pending = 0;

            while (running)
            {
                clientsSem.WaitOne();
                foreach (TcpClient cli in clients)
                {
                    if (cli.Connected && (pending = cli.Available) > 0)
                    {
                        byte[] packet = new byte[pending];
                        NetworkStream io = cli.GetStream();
                        io.Read(packet, 0, pending);

                        if (!Common.correctPacket(packet, Common.PACKET_COMMON | Common.PACKET_END))
                        {
                            Console.WriteLine("Incorrect packet: " + packet[0] + ", " + packet[1] + ".");
                            continue;
                        }

                        if (packet[0] == Common.PACKET_END)
                        {
                            slSem.WaitOne();
                            if (packet.Length > Common.PACKET_HEADER_SIZE)
                            {
                                sl.playerParted(packet[Common.PACKET_HEADER_SIZE]);
                            }
                            else
                            {
                                sl.playerParted(0);
                            }
                            slSem.Release();
                        }
                        else if (packet[0] == Common.PACKET_COMMON)
                        {
                            ParameterizedThreadStart ts = new ParameterizedThreadStart(sl.playerHandle);
                            new Thread(ts).Start(packet.Skip(Common.PACKET_HEADER_SIZE).ToArray());
                        }
                    }
                }
                clientsSem.Release();
                //Thread.Sleep(5); -- works fine without it :]
            }
        }

        internal void broadcast(byte type, byte[] data)
        {
            byte[] packet = new byte[data.Length + Common.PACKET_HEADER_SIZE];
            data.CopyTo(packet, Common.PACKET_HEADER_SIZE);
            packet[0] = type;
            packet[1] = Common.checksum(packet);
            

            clientsSem.WaitOne();
            foreach (TcpClient cli in clients)
            {
                NetworkStream io = cli.GetStream();
                io.BeginWrite(packet, 0, packet.Length, new AsyncCallback(Common.asyncWrite), io);
            }
            clientsSem.Release();
        }
    }
}
