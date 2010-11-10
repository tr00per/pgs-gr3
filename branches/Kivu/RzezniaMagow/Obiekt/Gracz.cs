using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RzezniaMagow
{
    public  class Gracz : Obiekt
    {

        private Vector2 pozycjaKursora;
        private short zycie;
        private byte typAvatara;
        private String nick;
        private short punkty;
        private short iloscZgonow;

        private List<Pocisk> listaPociskow;

        private Bron aktualnaBron;

       


        public Gracz() : base()
        {
            pozycjaKursora = new Vector2();
            listaPociskow = new List<Pocisk>();
            aktualnaBron = new Bron();
        }

        public Gracz(float x , float y, byte id): base(x , y, id)
        {
            pozycjaKursora = new Vector2();
            listaPociskow = new List<Pocisk>();
            punkty = 0;
            zycie = 100;
            iloscZgonow = 0;
            aktualnaBron = new Bron();

        }

        public Gracz(String name, byte avat)
        {
            pozycjaKursora = new Vector2();
            nick = name;
            typAvatara = avat;
            punkty = 0;
            zycie = 100;
            iloscZgonow = 0;
            listaPociskow = new List<Pocisk>();
            aktualnaBron = new Bron();

        }

        public Gracz(Gracz kopia) : base(kopia)
        {
            pozycjaKursora = kopia.pozycjaKursora;
            nick = kopia.nick;
            typAvatara = kopia.typAvatara;
            punkty = kopia.punkty;
            zycie = kopia.zycie;
            iloscZgonow = kopia.iloscZgonow;
            listaPociskow = new List<Pocisk>();
            aktualnaBron = kopia.aktualnaBron;
            
            

        }


        #region GET - SET

        public Vector2 getPozycjaKursora
        {
            get { return pozycjaKursora; }
            set { pozycjaKursora = value; }
        }


        public byte getTypAvatara
        {
            get { return typAvatara; }
            set { typAvatara = value; }
        }


        public String getNick
        {
            get { return nick; }
            set { nick = value; }
        }


        public short getPunkty
        {
            get { return punkty; }
            set { punkty = value; }
        }


        public short getIloscZgonow
        {
            get { return iloscZgonow; }
            set { iloscZgonow = value; }
        }


        public short getZycie
        {
            get { return zycie; }
            set { zycie = value; }
        }

        public List<Pocisk> getListaPociskow
        {
            get { return listaPociskow; }
        }

        public Bron AktualnaBron
        {
            get { return aktualnaBron; }
            set { aktualnaBron = value; }
        }

        #endregion


    }
}
