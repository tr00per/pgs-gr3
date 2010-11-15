using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NetTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerLogic sl = new ServerLogic();
            ClientLogic cl = new ClientLogic();

            sl.server.startServer();
            Console.WriteLine("SERVER RUNNING: " + sl.server.isRunning().ToString());
            cl.connect("127.0.0.1", 20000, "tr00per", 1);
            Console.WriteLine("CLIENT RUNNING: " + cl.isRunning().ToString());
            Console.WriteLine("WAITING...");
            Thread.Sleep(1000);
            Console.WriteLine("SENDING MSG...");
            sl.spam();
            Console.WriteLine("DONE.");
            Console.WriteLine("SENDING MSG...");
            sl.spam();
            Console.WriteLine("DONE.");
            Console.WriteLine("DOING RADNOM STUFF...");
            for (int i = 0; i < 100; ++i)
            {
                int c = i * 2 / 4;
                Thread.Sleep(Math.Max(c, 5));
            }
            Console.WriteLine("DONE.");
            //cl.disconnect();
            sl.server.stopServer();
            Console.WriteLine("CLIENT RUNNING: " + cl.isRunning().ToString());
            Console.WriteLine("SERVER RUNNING: " + sl.server.isRunning().ToString());

            Console.WriteLine("DONE!");
        }
    }
}
