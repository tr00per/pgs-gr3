using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Timers;

namespace RzezniaMagow
{
    public class ServerLogic : ServerAbstract
    {
        private Server server;

        private List<Gracz> players;
        private List<Pocisk> bullets;
        private byte nextPlayerID;
        private byte nextBulletID;

        private byte roundNumber;

        private System.Timers.Timer updateTimer;

        private SerwerProtocol prot;

        public ServerLogic()
        {
            roundNumber = 0;
            nextPlayerID = 13;
            nextBulletID = 0;
            prot = new SerwerProtocol();
            server = new Server(this, 20000, 5);
            bindServer(server);
           // updateTimer = new System.Timers.Timer(10);
          // updateTimer.Elapsed += new ElapsedEventHandler(updateTimerCB);
            players = new List<Gracz>();
            bullets = new List<Pocisk>();
            server.startServer();
        }

        public void fuckinStop()
        {
            if (server.isRunning())
            {
                server.stopServer();
            }
        }

        public override void serverStarted()
        {
          //  updateTimer.Start();
        }

        public override void serverStopped()
        {
           // updateTimer.Stop();
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

        //private void updateTimerCB(object o, ElapsedEventArgs args)
        private void updateTimerCB()
        {
            if (Game.czyNowaRunda)
            {
                byte[] data = prot.createPackage(players, bullets, Common.PACKET_BEGIN, roundNumber); //FIXME change arguments
                // byte[] data = prot.getTablica;
                sendUpdate(Common.PACKET_BEGIN, data);
            }
            else
            {
                byte[] data = prot.createPackage(players, bullets, Common.PACKET_COMMON, roundNumber); //FIXME change arguments
                // byte[] data = prot.getTablica;
                sendUpdate(Common.PACKET_COMMON, data);
            }
        }

        public override void playerHandle(object data)
        {
            byte[] d = (byte[])data;
            Gracz gracz = new Gracz();
            
            
            gracz = prot.unpack(d);

            for (int i = 0; i < players.Count; i++)
            {
                if (players.ElementAt(i).getID == gracz.getID)
                {
                    players.ElementAt(i).getPozycja = gracz.getPozycja;
                    players.ElementAt(i).getPozycjaKursora = gracz.getPozycjaKursora;
                }
            }

            if (gracz.getListaPociskow.Count > 1)
                System.Console.WriteLine("sfvsvs");

            for (int i = 0; i < gracz.getListaPociskow.Count; i++)
            {
                    bullets.Add(new Pocisk(gracz.getPozycja.X,gracz.getPozycja.Y,nextBulletID,gracz.getAktualnaBron.getTypBroni,gracz.getID));
                   
                    nextBulletID++;
                
            }

            updateTimerCB();

            ///Console.WriteLine(id + " send something to server.");
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

