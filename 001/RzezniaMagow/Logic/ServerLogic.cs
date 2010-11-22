﻿using System;
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

        public List<Pocisk> getBullets
        {
            get { return bullets; }
            set { bullets = value; }
        }
        private byte nextPlayerID;
        private byte nextBulletID;

        private byte roundNumber;

        private SerwerProtocol prot;
        //private System.Timers.Timer updateTimer;

        private int predkoscWysylania;

        public  int getPredkoscWysylania
        {
            get { return predkoscWysylania; }
            set { predkoscWysylania = value; }
        }

        public ServerLogic()
        {
            predkoscWysylania = 20;
            roundNumber = 0;
            nextPlayerID = 13;
            nextBulletID = 0;
            prot = new SerwerProtocol();
            server = new Server(this, 20000, 5);
            bindServer(server);
            players = new List<Gracz>();
            bullets = new List<Pocisk>();

            //updateTimer = new System.Timers.Timer(10);
           //updateTimer.Elapsed += new ElapsedEventHandler(updateTimerCB);

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
            //updateTimer.Start();
        }
        public override void serverStopped() {}

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
                Console.WriteLine("### Beginning new round!");
                ++roundNumber;
                byte[] data = prot.createPackage(players, bullets, Common.PACKET_BEGIN, roundNumber);
                sendUpdate(Common.PACKET_BEGIN, data);
                Game.czyNowaRunda = false;
            }
            else
            {
                byte[] data = prot.createPackage(players, bullets, Common.PACKET_COMMON, roundNumber);
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

          
            for (int i = 0; i < gracz.getListaPociskow.Count; i++)
            {
                    Pocisk poc = new Pocisk(gracz.getPozycja.X,gracz.getPozycja.Y,gracz.getPozycjaKursora.X,gracz.getPozycjaKursora.Y,
                                            nextBulletID,gracz.getAktualnaBron.getTypBroni,gracz.getID);

                    //poc.calculateSpeed();
                    //bullets.Add(poc);
                   
                    nextBulletID++;
                
            }


            updateTimerCB();

            ///Console.WriteLine(id + " send something to server.");
        }

        volatile byte _removeID;

        public void removeBullets()
        {
            for (int i = bullets.Count - 1; i > -1; i--)
            {
                if (bullets.ElementAt(i).getPozycja.X < 0 || bullets.ElementAt(i).getPozycja.Y < 0 || bullets.ElementAt(i).getPozycja.Y > Game.map.getTekstura.Width || bullets.ElementAt(i).getPozycja.X > Game.map.getTekstura.Height)

                    bullets.RemoveAt(i);
            }

        }





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

