using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Network;

namespace Logic
{
    public class ClientLogic: Client
    {
        public ClientLogic()
            : base(status)
        {
        }

        override protected void updateArrived(byte[] data)
        {
        }

        protected override void beginRound(byte[] data)
        {
        }

        public static void status(string msg)
        {
            Console.WriteLine("Client: " + msg);
        }
    }
}
