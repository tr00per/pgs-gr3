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
        private List<Bonus> bonusList;

        public List<Bonus> BonusList
        {
            get { return bonusList; }
            set { bonusList = value; }
        }

        public Mapa(float x, float y): base(x, y)
        {
            this.bonusList = new List<Bonus>();
        }


        public void addBonus(Bonus bonus)
        {
            this.bonusList.Add(bonus);
        }

        public void removeBonus(Bonus bonus)
        {
            this.bonusList.Remove(bonus);
        }
        public void removeBonus(int x,int y)
        {
            foreach (Bonus b in bonusList)
            {
                if(b.getPozycja.X==x && b.getPozycja.Y==y)
                {
                    this.bonusList.Remove(b);
                    break;
                }
            }
        }

        public Bonus getBonus(int x,int y)
        {
            foreach (Bonus b in bonusList)
            {
                if(b.getPozycja.X==x && b.getPozycja.Y==y)
                {
                    return b;
                }
            }
            return null;
        }


        public override void LoadContent(Texture2D tlo)
        {
            base.LoadContent(tlo);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            base.Draw(gameTime, spriteBatch);

        }
    }
}
