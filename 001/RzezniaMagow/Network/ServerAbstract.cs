using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RzezniaMagow
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
        /// Invoked when server has started.
        /// </summary>
        abstract public void serverStarted();

        /// <summary>
        /// Invoked when server is stopping.
        /// </summary>
        abstract public void serverStopped();

        /// <summary>
        /// Handshake callback.
        /// </summary>
        /// <param name="nick">New player's nick.</param>
        /// <param name="avatar">New player's avatar.</param>
        /// <returns>Unique player ID.</returns>
        abstract public byte newPlayerConnected(string nick, byte avatar);

        /// <summary>
        /// Common communication callback. Run asynchronously.
        /// </summary>
        /// <param name="data">byte[] for deserialization.</param>
        abstract public void playerHandle(object data);

        /// <summary>
        /// Player said goodbye.
        /// </summary>
        /// <param name="id">ID of disconnecting client.</param>
        abstract public void playerParted(byte id);

        /// <summary>
        /// Common comunication update.
        /// This should be run periodically to provide synchronization.
        /// </summary>
        /// <param name="data">Content.</param>
        public void sendUpdate(byte type, byte[] data)
        {
            srv.broadcast(type, data);
        }

        /// <summary>
        /// Send broadcast text message to everyone.
        /// </summary>
        /// <param name="msg">Message, max 200 chars.</param>
        public void sendMessage(string msg)
        {
            if (msg.Length > 200)
            {
                msg = msg.Substring(0, 200);
            }
            else
            {
                msg.PadRight(200);
            }

            //TODO do the proper conversion w/check
            Encoding enc = new UTF8Encoding();
            srv.broadcast(Common.PACKET_SRVMSG, enc.GetBytes(msg));
        }
    }
}

