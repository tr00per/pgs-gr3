using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Network;
using System.Threading;
using System.Timers;

namespace RzezniaMagow
{
    public class ServerLogic: Network.ServerAbstract
    {
        private Server server;

        private List<Gracz> players;
        private List<Pocisk> bullets;
        private byte nextPlayerID;

        private byte roundNumber;

        private System.Timers.Timer updateTimer;

        private SerwerProtocol prot;

        public ServerLogic()
        {
            roundNumber = 0;
            nextPlayerID = 0;
            prot = new SerwerProtocol();
            server = new Server(this, 20000, 5);
            bindServer(server);
            updateTimer = new System.Timers.Timer(30);
            updateTimer.Elapsed += new ElapsedEventHandler(updateTimerCB);
        }

        public override void serverStarted()
        {
            updateTimer.Start();
        }

        public override void serverStopped()
        {
            updateTimer.Stop();
        }

        /// <summary>
        /// New player connected. Give him his ID.
        /// </summary>
        public override byte newPlayerConnected(string nick, byte avatar)
        {
            Gracz p = new Gracz(nextPlayerID);
            p.getNick = nick;
            p.getTypAvatara = avatar;
            players.Add(p);
            return nextPlayerID++;
        }

        private void updateTimerCB(object o, ElapsedEventArgs args)
        {
            prot.createPackage(players, bullets, Common.PACKET_COMMON, roundNumber); //FIXME change arguments
            byte[] data = prot.getTablica;
            sendUpdate(Common.PACKET_COMMON, data);
        }

        public override void playerHandle(object data)
        {
            byte[] d = (byte[])data;
            byte id = d[0];
			Console.WriteLine(id + " send something to server.");
        }

        volatile byte _removeID;

        public override void playerParted(byte id)
        {
            _removeID = id;
            players.Remove(players.Find(new Predicate<Gracz>(playerRemover)));
        }

        private bool playerRemover(Gracz p)
        {
            return p.getID == _removeID;
        }
    }
}
