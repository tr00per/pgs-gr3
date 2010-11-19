using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace RzezniaMagow
{
    public class Obiekt
    {
        private Vector2 pozycja;
        private byte ID;

        private Texture2D tekstura;




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

        public Obiekt(Obiekt kopia, byte idd)
        {
            pozycja = kopia.pozycja;
            ID = idd;


        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            
            spriteBatch.Draw(tekstura,pozycja, Color.White);
            
            
        }
        public virtual void LoadContent(Texture2D tlo)
        {
            
            this.tekstura = tlo;
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
        public Texture2D getTekstura
        {
            get { return tekstura; }
            set { tekstura = value; }
        }

        public byte getID
        {
            get { return ID; }
            set { ID = value; }
        }

        #endregion


    }
}
