using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RzezniaMagow
{
    public class Gracz : Obiekt
    {

        private Vector2 pozycjaKursora;
        private byte zycie;
        private byte typAvatara;
        private String nick;
        private byte punkty;
        private byte iloscZgonow;

        private List<Pocisk> listaPociskow;

        private Bron aktualnaBron;



        public Gracz()
            : base()
        {
            pozycjaKursora = new Vector2();
            listaPociskow = new List<Pocisk>();
            aktualnaBron = new Bron();
        }

        public Gracz(float x, float y, byte id)
            : base(x, y, id)
        {
            pozycjaKursora = new Vector2();
            listaPociskow = new List<Pocisk>();
            punkty = 0;
            zycie = 100;
            iloscZgonow = 0;
            aktualnaBron = new Bron();

        }

        public Gracz(byte id) : this(0, 0, id) { }

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
        public Gracz(byte id,String name, byte avat )
        {
            pozycjaKursora = new Vector2();
            nick = name;
            typAvatara = avat;
            punkty = 0;
            zycie = 100;
            iloscZgonow = 0;
            listaPociskow = new List<Pocisk>();
            aktualnaBron = new Bron();
            getID = id;
            if (typAvatara == 1)
                this.LoadContent(Game.content.Load<Texture2D>(@"Avatar\czerwony"));
            if (typAvatara == 2)
                this.LoadContent(Game.content.Load<Texture2D>(@"Avatar\niebieski"));
            if (typAvatara == 3)
                this.LoadContent(Game.content.Load<Texture2D>(@"Avatar\Frog"));
            if (typAvatara == 4)
                this.LoadContent(Game.content.Load<Texture2D>(@"Avatar\rozowy"));


        }

        public Gracz(Gracz kopia)
            : base(kopia)
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


        public byte getPunkty
        {
            get { return punkty; }
            set { punkty = value; }
        }


        public byte getIloscZgonow
        {
            get { return iloscZgonow; }
            set { iloscZgonow = value; }
        }


        public byte getZycie
        {
            get { return zycie; }
            set { zycie = value; }
        }

        public List<Pocisk> getListaPociskow
        {
            get { return listaPociskow; }
            set { listaPociskow = value; }
        }

        public Bron getAktualnaBron
        {
            get { return aktualnaBron; }
            set { aktualnaBron = value; }
        }

        #endregion


    }
}
