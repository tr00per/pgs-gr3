using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numeric;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RzezniaMagow
{
    public class Pocisk : Obiekt
    {
        private byte typPocisku;
        private byte IDOwnera;
        private byte damage;
        private byte opoznienie;
        private byte trafienie;



       

       
        private Vector2 pozycjaKursora;

        private Vector2 predkosc;
       
        
        public Pocisk() : base()
        {
            pozycjaKursora = new Vector2();

        }


        public Pocisk(float x, float y, float xk, float yk,byte id, byte typ, byte owner) : base(x,y,id )
        {
            typPocisku = typ;
            IDOwnera = owner;
            pozycjaKursora = new Vector2(xk, yk);
            if (typPocisku == 1)
            {
                this.LoadContent(Game.content.Load<Texture2D>(@"Pociski\snow"));
                damage = 20;
                opoznienie = 2;
            }
            if (typPocisku == 2)
            {
                this.LoadContent(Game.content.Load<Texture2D>(@"Pociski\blue"));
                damage = 20;
                opoznienie = 2;
            }
            if (typPocisku == 3)
            {
                this.LoadContent(Game.content.Load<Texture2D>(@"Pociski\red"));
                damage = 20;
                opoznienie = 2;
            }
            if (typPocisku == 4)
            {
                this.LoadContent(Game.content.Load<Texture2D>(@"Pociski\purple"));
                damage = 20;
                opoznienie = 2;
            }
            getPunktObrotu = new Vector2(getTekstura.Width / 2, getTekstura.Height / 2);

        }

        
        public void calculateSpeed()
        {

            predkosc = new Vector2(pozycjaKursora.X - getPozycja.X, pozycjaKursora.Y - getPozycja.Y);

            float dlugosc = (float)Math.Sqrt(Math.Pow((pozycjaKursora.X - getPozycja.X),2) + Math.Pow((pozycjaKursora.Y - getPozycja.Y),2));

            predkosc = new Vector2(predkosc.X * 1 / dlugosc, predkosc.Y * 1 / dlugosc);

           
        }

        public void updatePosition(GameTime gameTime)
        {
            

            float x = getPozycja.X + predkosc.X * gameTime.ElapsedGameTime.Milliseconds/opoznienie;
            float y = getPozycja.Y + predkosc.Y * gameTime.ElapsedGameTime.Milliseconds/opoznienie;

            getPozycja = new Vector2(x, y);



        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {


            if (CollisionDetection2D.CDPerformedWith == UseForCollisionDetection.Rectangles)
                Primitives2D.DrawRectangle(this.RectanglePoints, spriteBatch);

            // base.Draw(gameTime, spriteBatch);
        }

        

        #region GET - SET

        public byte getTrafienie
        {
            get { return trafienie; }
            set { trafienie = value; }
        }

        public byte getDamage
        {
            get { return damage; }
            set { damage = value; }
        }

        public Vector2 getPozycjaKursora
        {
            get { return pozycjaKursora; }
            set { pozycjaKursora = value; }
        }

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
