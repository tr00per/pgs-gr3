using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RzezniaMagow
{
    public class Pocisk : Obiekt
    {
        private byte typPocisku;
        private byte IDOwnera;
        private Vector2 pozycjaKursora;

        public Vector2 getPozycjaKursora
        {
            get { return pozycjaKursora; }
            set { pozycjaKursora = value; }
        }

       
       

        public Pocisk() : base()
        {
            pozycjaKursora = new Vector2();
        }
        public Pocisk(float x, float y, byte typ) : base(x,y)
        {
            typPocisku = typ;
            pozycjaKursora = new Vector2();
        }



        public Pocisk(float x, float y, float xk, float yk,byte id, byte typ, byte owner) : base(x,y,id )
        {
            typPocisku = typ;
            IDOwnera = owner;
            pozycjaKursora = new Vector2(xk, yk);
        }

        public Pocisk(Pocisk kopia, byte Id) : base(kopia, Id)
        {
            typPocisku = kopia.typPocisku;
            IDOwnera = kopia.IDOwnera;
            pozycjaKursora = kopia.pozycjaKursora;

        }


        #region GET - SET


        public byte getIDOwnera
        {
            get { return IDOwnera; }
            set { IDOwnera = value; }
        }


        public byte getTypPocisku
        {
            get { return typPocisku; }
            set { typPocisku = value; }
        }

        #endregion


    }
}
