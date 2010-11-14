using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Network
{
    abstract public class ServerAbstract
    {
        private Server srv;

        protected void bindServer(Server server)
        {
            srv = server;
        }

        abstract protected internal byte newPlayerConnected(string nick, byte avatar);
        abstract protected internal void playerHandle(object data);
        abstract protected internal void playerParted(byte id);

        protected void sendUpdate(byte[] data)
        {
            srv.broadcast(Common.PACKET_COMMON, data);
        }

        protected internal void sendMessage(string msg)
        {
            if (msg.Length > 200)
            {
                msg = msg.Substring(0, 200);
            }

            //TODO do the proper conversion w/check
            Encoding enc = new UTF8Encoding();
            srv.broadcast(Common.PACKET_SRVMSG, enc.GetBytes(msg));
        }
    }
}
