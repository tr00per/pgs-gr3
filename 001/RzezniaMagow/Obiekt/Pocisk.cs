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

       
       

        public Pocisk() : base()
        {

        }
        public Pocisk(float x, float y, byte typ) : base(x,y)
        {
            typPocisku = typ;
        }



        public Pocisk(float x, float y,byte id, byte typ, byte owner) : base(x,y,id )
        {
            typPocisku = typ;
            IDOwnera = owner;
        }

        public Pocisk(Pocisk kopia) : base(kopia)
        {
            typPocisku = kopia.typPocisku;
            IDOwnera = kopia.IDOwnera;

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
