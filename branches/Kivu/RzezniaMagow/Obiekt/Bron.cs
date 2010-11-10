using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RzezniaMagow
{
    class Bron
    {

        private String nazwa;
        private short obrazenia;   
        private byte typBroni;
        private short czasLadowania;

        public Bron()
        {
        }
        public Bron(String s, short dam, byte typ, short czas)
        {
            nazwa = s;
            obrazenia = dam;
            typBroni = typ;
            czasLadowania = czas;

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
