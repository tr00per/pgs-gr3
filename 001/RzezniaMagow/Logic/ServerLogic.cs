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

        public List<Gracz> getPlayers
        {
            get { return players; }
            set { players = value; }
        }
        private List<Pocisk> bullets;

        public List<Pocisk> getBullets
        {
            get { return bullets; }
            set { bullets = value; }
        }
        private byte nextPlayerID;
        private byte nextBulletID;

        private byte roundNumber;
        public bool flaga;

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
            nextPlayerID = 1;
            nextBulletID = 0;
            prot = new SerwerProtocol();
            server = new Server(this, 20000, 5);
            bindServer(server);
            players = new List<Gracz>();
            bullets = new List<Pocisk>();

          // updateTimer = new System.Timers.Timer(10);
           //updateTimer.Elapsed += new ElapsedEventHandler(updateTimerCB);
            flaga = true;
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
            Gracz p = new Gracz(nextPlayerID, nick,avatar);
            
            players.Add(p);

            if (players.Count > 1 && flaga)
            {
                Game.czyNowaRunda = true;
                flaga = false;
            }
            else
                flaga = true;


            return nextPlayerID++;
        }

        //private void updateTimerCB(object o, ElapsedEventArgs args)
        private void updateTimerCB()
        {
            if (Game.czyNowaRunda)
            {
                Game.map.bonusReset();
                removeAllBullets();
                Console.WriteLine("### Beginning new round!   " + roundNumber);
                //Game.czasPrzygotowania = 100;
                for (int i = 0; i < players.Count; i++)
                {
                    if (players.ElementAt(i).getZycie == 0)
                        players.ElementAt(i).getIloscZgonow++;
                }

                for (int i = 0; i < players.Count; i++)
                {
                    players.ElementAt(i).getZycie = 100;
                }

                roundNumber++;
                
                byte[] data = prot.createPackage(players, bullets,Game.map.getListaBonusow, Common.PACKET_BEGIN, roundNumber);
                sendUpdate(Common.PACKET_BEGIN, data);
                Game.czyNowaRunda = false;
            }
            else
            {
                removeBullets();
                byte[] data = prot.createPackage(players, bullets,Game.map.getListaBonusow, Common.PACKET_COMMON, roundNumber);
                sendUpdate(Common.PACKET_COMMON, data);
                
            }
            //if(players.Count>1)
            //startNewRound();
        }

        public override void playerHandle(object data)
        {
            byte[] d = (byte[])data;
            Gracz gracz = new Gracz();
                       
            gracz = prot.unpack(d);

            if(gracz.getListaPociskow.Count>0)
            gracz.getAktualnaBron = new Bron(gracz.getListaPociskow.First().getTypPocisku);

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
                    poc.getTrafienie = 0;
                    poc.calculateSpeed();
                    bullets.Add(poc);

                    nextBulletID++;
            }
            removeBullets();
            updateTimerCB();
            
            ///Console.WriteLine(id + " send something to server.");
        }

        volatile byte _removeID;

        public void removeBullets()
        {
            if (bullets.Count > 0)
            {
                for (int i = bullets.Count - 1; i > -1; i--)
                {
                    if (bullets.ElementAt(i).getPozycja.X < Game.map.getMapOffset || bullets.ElementAt(i).getPozycja.Y < Game.map.getMapOffset
                        || bullets.ElementAt(i).getPozycja.Y > Game.map.getTekstura.Width - Game.map.getMapOffset
                        || bullets.ElementAt(i).getPozycja.X > Game.map.getTekstura.Height - Game.map.getMapOffset)
                    {
                        bullets.RemoveAt(i);
                    }
                }
            }
        }
        public void removeAllBullets()
        {
            if (bullets.Count > 0)
            {
                for (int i = bullets.Count - 1; i > -1; i--)
                {
                    bullets.RemoveAt(i);
                }
            }
        }


        public void bulletsCollision()
        {
            if (bullets.Count > 0)
            {
                for (int i = bullets.Count - 1; i > -1; i--)
                {
                    for (int j = 0; j < Game.map.getListaPrzeszkod.Count; j++)
                    {
                        if (bullets.Count > 0)
                            if (CollisionDetection2D.BoundingRectangle(Game.map.getListaPrzeszkod.ElementAt(j).RectanglePoints, bullets.ElementAt(i).RectanglePoints))
                            {
                                bullets.ElementAt(i).getTrafienie = 1;
                                //bullets.RemoveAt(i);
                                break;
                            }
                    }
                }
            }
            
        }
       
        public void bulletsPlayersCollision()
        {
            if(bullets.Count>0)
            {
                for (int i = 0; i < bullets.Count; i++)
                {
                    for (int j = 0; j < players.Count; j++)
                    {
                        if (bullets.Count > 0 && players.ElementAt(j).getZycie > 0)
                        {
                            if (CollisionDetection2D.BoundingRectangle(players.ElementAt(j).RectanglePoints, bullets.ElementAt(i).RectanglePoints))
                            {
                                if (bullets.ElementAt(i).getTrafienie == 0 && bullets.ElementAt(i).getIDOwnera != players.ElementAt(j).getID)
                                {
                                    bullets.ElementAt(i).getTrafienie = 1;
                                    players.ElementAt(j).getZycie -= bullets.ElementAt(i).getDamage;
                                    if (players.ElementAt(j).getZycie > 200 || players.ElementAt(j).getZycie == 0)
                                    {
                                        players.ElementAt(j).getZycie = 0;
                                        //players.ElementAt(j).getIloscZgonow++;
                                    }

                                }
                            }
                        }
                    }

                }

            }


        }

        public void bonusPlayersCollision()
        {
            for (int i = 0; i <Game.map.getListaBonusow.Count; i++)
            {
                if (Game.map.getListaBonusow.ElementAt(i).getCzyZlapany == 0)
                    for (int j = 0; j < players.Count; j++)
                    {
                        if (CollisionDetection2D.BoundingRectangle(Game.map.getListaBonusow.ElementAt(i).RectanglePoints, players.ElementAt(j).RectanglePoints))
                        {
                             bonusFunction(Game.map.getListaBonusow.ElementAt(i),players.ElementAt(j));
                             Game.map.getListaBonusow.ElementAt(i).getCzyZlapany = 1;
                        }
                    }
            }

        }

        public void trapPlayersCollision()
        {
            for (int i = 0; i < Game.map.getListaPulapek.Count; i++)
            {
                    for (int j = 0; j < players.Count; j++)
                    {
                        if (players.ElementAt(j).getZycie < 101 || players.ElementAt(j).getZycie >0)
                        if (CollisionDetection2D.BoundingRectangle(Game.map.getListaPulapek.ElementAt(i).RectanglePoints, players.ElementAt(j).RectanglePoints))
                        {
                            players.ElementAt(j).getZycie -= 1;
                            if (players.ElementAt(j).getZycie > 200 || players.ElementAt(j).getZycie == 0)
                            {
                                players.ElementAt(j).getZycie = 0;
                                //players.ElementAt(j).getIloscZgonow++;
                            }

                        }
                    }


            }

        }

        public void bonusFunction(Bonus bon, Gracz gracz)
        {
            switch (bon.getTypBonusu)
            {
                case typBonusu.apteczka:
                    {
                        gracz.getZycie += 40;
                        if (gracz.getZycie > gracz.getZycieMaks)
                            gracz.getZycie = gracz.getZycieMaks;
                        break;
                    }
                case typBonusu.bron:
                    {
                        switch (bon.getID)
                        {
                            case 0:
                                {
                                    gracz.getAktualnaBron = new Bron("Fireball", 0, 20);
                                    break;
                                }
                            case 1:
                                {
                                    gracz.getAktualnaBron = new Bron("Ice Arrow", 1, 2);
                                    break;
                                }
                            case 2:
                                {
                                    gracz.getAktualnaBron = new Bron("Thunder", 2, 50);
                                    break;
                                }
                            default: break;
                        }
                        break;
                    }
                default: break;
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

