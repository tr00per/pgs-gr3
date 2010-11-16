using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RzezniaMagow
{
    public class ClientLogic: Client
    {
        public List<Gracz> listaGraczy;
        public List<Pocisk> listaPociskow;
        private short nrRundy;
        private bool czyGra = false;


        public ClientProtokol clientProtocol;
       
        

        public ClientLogic()
            : base(status)
        {

            clientProtocol = new ClientProtokol();
            listaGraczy = new List<Gracz>();
            listaPociskow = new List<Pocisk>();
            
        }

        override protected void updateArrived(byte[] data)
        {
            //tutaj cuda wianki o tym co sie dzieje po otrzymaniu pakietu
            clientProtocol.unpack(data, 16);

        }

        protected override void beginRound(byte[] data)
        {
            //tutaj cuda wianki o tym co sie dzieje przed poczatkiem rundy
            listaGraczy = new List<Gracz>();
            clientProtocol.unpack(data, 8);

        }

        public static void status(string msg)
        {
            Console.WriteLine("Client: " + msg);
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
