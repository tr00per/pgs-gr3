using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Network
{
    abstract public class ServerAbstract
    {
        private Server srv;

        /// <summary>
        /// Give server, which has instance of ServerAbstract given, to this instance.
        /// </summary>
        /// <param name="server">Initialized server.</param>
        protected void bindServer(Server server)
        {
            srv = server;
        }

        /// <summary>
        /// Handshake callback.
        /// </summary>
        /// <param name="nick">New player's nick.</param>
        /// <param name="avatar">New player's avatar.</param>
        /// <returns>Unique player ID.</returns>
        abstract protected internal byte newPlayerConnected(string nick, byte avatar);

        /// <summary>
        /// Common communication callback. Run asynchronously.
        /// </summary>
        /// <param name="data">byte[] for deserialization.</param>
        abstract protected internal void playerHandle(object data);

        /// <summary>
        /// Player said goodbye.
        /// </summary>
        /// <param name="id">ID of disconnecting client.</param>
        abstract protected internal void playerParted(byte id);

        /// <summary>
        /// Common comunication update.
        /// This should be run periodically to provide synchronization.
        /// </summary>
        /// <param name="data">Content.</param>
        protected void sendUpdate(byte[] data)
        {
            srv.broadcast(Common.PACKET_COMMON, data);
        }

        /// <summary>
        /// Send broadcast text message to everyone.
        /// </summary>
        /// <param name="msg">Message, max 200 chars.</param>
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
