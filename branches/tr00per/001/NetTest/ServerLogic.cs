using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Network;

namespace NetTest
{
    public class ServerLogic: Network.ServerAbstract
    {
        public Server server;

        public ServerLogic()
        {
            server = new Server(this, 20000, 5);
            bindServer(server);
        }

        override protected byte newPlayerConnected(string nick, byte avatar)
        {
            //add player to list, return his uniqe id
            return 0;
        }

        override protected void playerHandle(object data)
        {
            byte[] d = (byte[])data;
            byte id = d[0];
			Console.WriteLine(id + " send something to server.");
        }

        override protected void playerParted(byte id)
        {
            //remove player from list
            //release his id for future use (with nonzero retention time)
        }

        public void spam()
        {
            sendMessage("SPAM, SPAM, SPAM, SPAM, SPAM...");
        }
    }
}
