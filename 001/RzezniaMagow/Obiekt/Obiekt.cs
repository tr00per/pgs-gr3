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
        private float katObrotu;
        private Vector2 punktObrotu;

        

        public Obiekt()
        {
            pozycja = new Vector2();
            ID = 0;
            punktObrotu = new Vector2();
            katObrotu = 0;
            
        }

        public Obiekt(byte x)
        {
            ID = x;
            pozycja = new Vector2();
            punktObrotu = new Vector2();
            katObrotu = 0;
            
        }
        public Obiekt(float x, float y)
        {
            pozycja = new Vector2(x, y);
            ID = 0;
            punktObrotu = new Vector2();
            katObrotu = 0;
            
        }

        public Obiekt(float x, float y, byte z)
        {
            pozycja = new Vector2(x, y);
            ID = z;
            punktObrotu = new Vector2();
            katObrotu = 0;
           
        }

        public Obiekt(Obiekt kopia)
        {
            pozycja = kopia.pozycja;
            ID = kopia.ID;
            punktObrotu = kopia.punktObrotu;
            katObrotu = 0;
            

        }

        public Obiekt(Obiekt kopia, byte idd)
        {
            pozycja = kopia.pozycja;
            ID = idd;
            punktObrotu = new Vector2();
            katObrotu = 0;


        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            
            spriteBatch.Draw(tekstura,pozycja, Color.White);
            //spriteBatch.Draw(tekstura, new Rectangle((int)pozycja.X, (int)pozycja.Y, tekstura.Width, tekstura.Height), Color.White);
            
           // if (CollisionDetection2D.CDPerformedWith == UseForCollisionDetection.Rectangles)
            //    Primitives2D.DrawRectangle(RectanglePoints, spriteBatch);
            
           
        }
        public virtual void LoadContent(Texture2D tlo)
        {
            
            this.tekstura = tlo;
        }

        public List<Vector2> RectanglePoints
        {
            get
            {
                return new List<Vector2>()
                {
                     Rotations.RotatePoint((pozycja-punktObrotu),pozycja,katObrotu ),
                     Rotations.RotatePoint(new Vector2() { X = (pozycja - punktObrotu).X + tekstura.Width, Y = (pozycja - punktObrotu).Y }, pozycja, katObrotu),
                     Rotations.RotatePoint(new Vector2() { X = (pozycja - punktObrotu).X + tekstura.Width, Y = (pozycja - punktObrotu).Y + tekstura.Height }, pozycja, katObrotu),
                     Rotations.RotatePoint(new Vector2() { X = (pozycja - punktObrotu).X, Y = (pozycja - punktObrotu).Y + tekstura.Height }, pozycja , katObrotu)
                };
            }

        }



        #region GET - SET

        public Vector2 getPunktObrotu
        {
            get { return punktObrotu; }
            set { punktObrotu = value; }
        }


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
        public float getKatObrotu
        {
            get { return katObrotu; }
            set { katObrotu = value; }
        }
        #endregion


    }
}
