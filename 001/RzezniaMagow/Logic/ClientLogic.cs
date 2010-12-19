using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;

namespace RzezniaMagow
{
    public class ClientLogic: Client
    {
        public List<Gracz> listaGraczy;
        public List<Pocisk> listaPociskow;
        private short nrRundy;
        private bool czyGra = false;

        public ClientProtokol clientProtocol;
        private System.Timers.Timer updateTimer;

        public ClientLogic()
            : base(status)
        {
            clientProtocol = new ClientProtokol();
            listaGraczy = new List<Gracz>();
            listaPociskow = new List<Pocisk>();
            updateTimer = new System.Timers.Timer(5);
            updateTimer.Elapsed += new ElapsedEventHandler(updateTimerCB);
        }

        protected override void clientReady(byte id, string nick, byte avatar)
        {
            Game.zawodnik = new Gracz(id, nick, avatar);
           
            listaGraczy.Add(Game.zawodnik);
            //Game.client.getCzyGra = true;
			updateTimer.Enabled = false;
            updateTimer.Start();
        }

        private void updateTimerCB(object o, ElapsedEventArgs args)
        //private void updateTimerCB()
        {
            sendUpdate(clientProtocol.createPackage(ref Game.zawodnik));
        }

        override protected void updateArrived(byte[] data)
        {
            //tutaj cuda wianki o tym co sie dzieje po otrzymaniu pakietu
            clientProtocol.unpack(data, Common.PACKET_COMMON);
        }

        protected override void beginRound(byte[] data)
        {
            //tutaj cuda wianki o tym co sie dzieje przed poczatkiem rundy
            listaGraczy = new List<Gracz>();
            clientProtocol.unpack(data, Common.PACKET_BEGIN);
            Game.message = " Beginning new round!   ";
            //Game.czasPrzygotowania = 50;
			Game.czyNowaRunda = true;
			czyGra = true;
			updateTimer.Enabled = true;
        }

        public static void status(string msg)
        {
            Console.WriteLine("Client: " + msg);
        }

        public void BulletsCollision()
        {
            for (int i = listaPociskow.Count-1; i > -1; i--)
            {
                for (int j = 0; j < Game.map.getListaPrzeszkod.Count; j++)
                {
                    if (CollisionDetection2D.BoundingRectangle(Game.map.getListaPrzeszkod.ElementAt(j).RectanglePoints, listaPociskow.ElementAt(i).RectanglePoints))
                    {
                            listaPociskow.RemoveAt(i);
                            break;
                    }  
                }
            }

            for (int i = listaPociskow.Count - 1; i > -1; i--)
            {
                if (listaPociskow.ElementAt(i).getPozycja.X < 0 || listaPociskow.ElementAt(i).getPozycja.Y < 0 || listaPociskow.ElementAt(i).getPozycja.Y > Game.map.getTekstura.Width || listaPociskow.ElementAt(i).getPozycja.X > Game.map.getTekstura.Height)

                    listaPociskow.RemoveAt(i);
            }
        }




        public void fuckinStop()
        {
            if (this.isRunning())
            {
                updateTimer.Stop();
                this.disconnect();
            }
        }


        public short getNrRundy
        {
            get { return nrRundy; }
            set { nrRundy = value; }
        }
        public bool getCzyGra
        {
            get { return czyGra; }
            set { czyGra = value; }
        }

    }
}
