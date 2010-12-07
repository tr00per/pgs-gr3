using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RzezniaMagow
{
    public enum typBonusu { apteczka, mana, bron };

    public class Bonus : Obiekt
    {


        private typBonusu typBon;

       

        private byte czyZlapany;

        

        

        


        public Bonus() : base()
        {
        }

        public Bonus(float x, float y, typBonusu bon): base(x, y)
        {
            typBon = bon;

        }


        public byte getCzyZlapany
        {
            get { return czyZlapany; }
            set { czyZlapany = value; }
        }

        public typBonusu getTypBonusu
        {
            get { return typBon; }
            set { typBon = value; }

        }


    }
}
