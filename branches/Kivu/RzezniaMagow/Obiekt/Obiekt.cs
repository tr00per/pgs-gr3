using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RzezniaMagow
{
    public class Obiekt
    {
        private Vector2 pozycja;
        private byte ID;



        public Obiekt()
        {
            pozycja = new Vector2();
            ID = 0;
        }

        public Obiekt(byte x)
        {
            ID = x;
            pozycja = new Vector2();
        }
        public Obiekt(float x, float y)
        {
            pozycja = new Vector2(x, y);
            ID = 0;
        }

        public Obiekt(float x, float y, byte z)
        {
            pozycja = new Vector2(x, y);
            ID = z;
        }

        public Obiekt(Obiekt kopia)
        {
            pozycja = kopia.pozycja;
            ID = kopia.ID;

        }

        public void Draw()
        {

        }


        #region GET - SET

        public Vector2 getPozycja
        {
            get { return pozycja; }
            set 
            { 
                pozycja.X = value.X;
                pozycja.Y = value.Y;
            }
        }


        public byte getID
        {
            get { return ID; }
            set { ID = value; }
        }

        #endregion


    }
}
