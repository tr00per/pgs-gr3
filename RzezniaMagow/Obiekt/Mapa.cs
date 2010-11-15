using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RzezniaMagow
{
    public class Mapa : Obiekt
    {
        private Vector2 pozycja;

        private String nazwaPliku;
        
        


        public Mapa(float x, float y): base(x, y)
        {
            
            
        }

        public override void LoadContent(Texture2D tlo)
        {
            base.LoadContent(tlo);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            base.Draw(gameTime, spriteBatch);




        }
        

       
        
        public String getNazwaPliku
        {
            get { return nazwaPliku; }
            set { nazwaPliku = value; }
        }

        public Vector2 getPozycja
        {
            get { return pozycja; }
            set { pozycja = value; }
        }







    }
}
