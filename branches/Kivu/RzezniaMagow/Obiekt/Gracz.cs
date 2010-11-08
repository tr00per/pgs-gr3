using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RzezniaMagow
{
    class Gracz : Obiekt
    {

        private Vector2 pozycjaKursora;
        private short zycie;
        private byte typAvatara;
        private String nick;
        private short punkty;
        private short iloscZgonow;





        public Gracz() : base()
        {
            pozycjaKursora = new Vector2();
        }

        public Gracz(float x , float y, byte id): base(x , y, id)
        {
            pozycjaKursora = new Vector2();

        }

        public Gracz(String name, byte avat, byte id): base(id)
        {
            pozycjaKursora = new Vector2();
            nick = name;
            typAvatara = avat;
            punkty = 0;
            zycie = 100;
            iloscZgonow = 0;


        }

        public Gracz(Gracz kopia) : base(kopia)
        {
            pozycjaKursora = kopia.pozycjaKursora;
            nick = kopia.nick;
            typAvatara = kopia.typAvatara;
            punkty = kopia.punkty;
            zycie = kopia.zycie;
            iloscZgonow = kopia.iloscZgonow;

        }


        #region GET - SET

        public Vector2 PozycjaKursora
        {
            get { return pozycjaKursora; }
            set { pozycjaKursora = value; }
        }


        public byte TypAvatara
        {
            get { return typAvatara; }
            set { typAvatara = value; }
        }


        public String Nick
        {
            get { return nick; }
            set { nick = value; }
        }


        public short Punkty
        {
            get { return punkty; }
            set { punkty = value; }
        }


        public short IloscZgonow
        {
            get { return iloscZgonow; }
            set { iloscZgonow = value; }
        }


        public short Zycie
        {
            get { return zycie; }
            set { zycie = value; }
        }

        #endregion


    }
}
