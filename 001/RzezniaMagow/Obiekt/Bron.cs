using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RzezniaMagow
{
    public class Bron : Obiekt
    {

        private String nazwa;
        private short obrazenia;   
        private byte typBroni;
        private short czasLadowania;

        public Bron()
        {
        }

        public Bron(float x , float y,String s, short dam, byte typ, short czas) : base (x,y)
        {
            nazwa = s;
            obrazenia = dam;
            typBroni = typ;
            czasLadowania = czas;

        }

        public Bron(String s, short dam, byte typ, short czas)

        {
            nazwa = s;
            obrazenia = dam;
            typBroni = typ;
            czasLadowania = czas;

        }

        public Bron(Bron kopia)
        {
            nazwa = kopia.nazwa;
            obrazenia = kopia.obrazenia;
            typBroni = kopia.typBroni;
            czasLadowania = kopia.czasLadowania;
            getPozycja = new Vector2();


        }



        #region GET - SET


        public short getCzasLadowania
        {
            get { return czasLadowania; }
            set { czasLadowania = value; }
        }
        public byte getTypBroni
        {
            get { return typBroni; }
            set { typBroni = value; }
        }
        public short getObrazienia
        {
            get { return obrazenia; }
            set { obrazenia = value; }
        }

        public String getNazwa
        {
            get { return nazwa; }
            set { nazwa = value; }
        }

        #endregion
    




    }
}
