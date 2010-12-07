using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RzezniaMagow
{
    public class Gracz : Obiekt
    {

        private Vector2 pozycjaKursora;
        private byte zycie;
        private byte typAvatara;
        private String nick;
        private byte punkty;
        private byte iloscZgonow;
        private int walkSpeed;

        private Color fontColor;
        private bool czyZyje;
        private int punktyMany;
        private byte zycieMaks = 100;

        

       

       
        private List<Pocisk> listaPociskow;

        private Bron aktualnaBron;



        public Gracz()
            : base()
        {
            pozycjaKursora = new Vector2();
            listaPociskow = new List<Pocisk>();
            aktualnaBron = new Bron(typAvatara);
            walkSpeed = 2;
            getPunktObrotu = new Vector2(20, 40);
            czyZyje = true;
        }

        public Gracz(float x, float y, byte id)
            : base(x, y, id)
        {
            pozycjaKursora = new Vector2();
            listaPociskow = new List<Pocisk>();
            punkty = 0;
            zycie = 100;
            iloscZgonow = 0;
            aktualnaBron = new Bron(typAvatara);
            walkSpeed = 2;
            getPunktObrotu = new Vector2(20, 40);
            czyZyje = true;
        }

        public Gracz(byte id) : this(0, 0, id) { walkSpeed = 2; }
        
        public Gracz(byte id,String name, byte avat )
        {
            pozycjaKursora = new Vector2();
            nick = name;
            typAvatara = avat;
            punkty = 0;
            zycie = 100;
            iloscZgonow = 0;
            listaPociskow = new List<Pocisk>();
            aktualnaBron = new Bron(typAvatara);
            getID = id;
            walkSpeed = 2;
            czyZyje = true;
            punktyMany = 50;

            if (typAvatara == 1)
            {
                this.LoadContent(Game.content.Load<Texture2D>(@"Avatar\czerwony"));
                fontColor = Color.Red;
                getPunktObrotu = new Vector2(20, 40);
            }
            if (typAvatara == 2)
            {
                this.LoadContent(Game.content.Load<Texture2D>(@"Avatar\niebieski"));
                fontColor = Color.Blue;
                getPunktObrotu = new Vector2(20, 40);
            }
            if (typAvatara == 3)
            {
                this.LoadContent(Game.content.Load<Texture2D>(@"Avatar\Reaper"));
                fontColor = Color.Green;
                getPunktObrotu = new Vector2(20, 40);
            }
            if (typAvatara == 4)
            {
                this.LoadContent(Game.content.Load<Texture2D>(@"Avatar\rozowy"));
                fontColor = Color.Purple;
                getPunktObrotu = new Vector2(20, 40);
            }

        }

        public Gracz(Gracz kopia)
            : base(kopia)
        {
            pozycjaKursora = kopia.pozycjaKursora;
            nick = kopia.nick;
            typAvatara = kopia.typAvatara;
            punkty = kopia.punkty;
            zycie = kopia.zycie;
            iloscZgonow = kopia.iloscZgonow;
            listaPociskow = new List<Pocisk>();
            aktualnaBron = kopia.aktualnaBron;
            walkSpeed = kopia.walkSpeed;
            czyZyje = true;

        }



        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
           

            if (CollisionDetection2D.CDPerformedWith == UseForCollisionDetection.Rectangles)
                Primitives2D.DrawRectangle(this.RectanglePoints, spriteBatch);

           // base.Draw(gameTime, spriteBatch);
        }


        #region GET - SET

        public Vector2 getPozycjaKursora
        {
            get { return pozycjaKursora; }
            set { pozycjaKursora = value; }
        }

        public byte getZycieMaks
        {
            get { return zycieMaks; }
            set { zycieMaks = value; }
        }

        public int getPunktyMany
        {
            get { return punktyMany; }
            set { punktyMany = value; }

        }

        public int getWalkSpeed
        {
            get { return walkSpeed; }
            set { walkSpeed = value; }
        }

        public bool getCzyZyje
        {
            get { return czyZyje; }
            set { czyZyje = value; }
        }

        public byte getTypAvatara
        {
            get { return typAvatara; }
            set { typAvatara = value; }
        }


        public String getNick
        {
            get {
                for (int m = 0; m < nick.Length; m++)
                { 
                    if ((int)nick[m] == 0)
                        nick.Remove(m);
                }
                
                return nick; 
            
            }
            set { nick = value; }
        }


        public byte getPunkty
        {
            get { return punkty; }
            set { punkty = value; }
        }


        public byte getIloscZgonow
        {
            get { return iloscZgonow; }
            set { iloscZgonow = value; }
        }
        public Color getFontColor
        {
            get { return fontColor; }
            set { fontColor = value; }
        }

        public byte getZycie
        {
            get { return zycie; }
            set { zycie = value; }
        }

        public List<Pocisk> getListaPociskow
        {
            get { return listaPociskow; }
            set { listaPociskow = value; }
        }

        public Bron getAktualnaBron
        {
            get { return aktualnaBron; }
            set { aktualnaBron = value; }
        }

        #endregion


    }
}
