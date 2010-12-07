using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace RzezniaMagow
{
    public class Mapa : Obiekt
    {

        private List<Bonus> listaBonusow;
        private List<Obiekt> listaPrzeszkod;
        private List<Obiekt> listaPulapek;



        
        private int mapOffset;
        private byte nextBonusID;



        public Mapa(float x, float y): base(x, y)
        {
            listaPrzeszkod = new List<Obiekt>();
            listaBonusow = new List<Bonus>();
            listaPulapek = new List<Obiekt>();
            mapOffset = 17;
            nextBonusID = 0;
            
            
        }

        public void CreateWalls()
        {
            String path = "wall.txt";

            String[] items = File.ReadAllText(path).
            Split(new String[] { " ", Environment.NewLine },
            StringSplitOptions.RemoveEmptyEntries);
            List<int> numbers = new List<int>();
            foreach (String item in items)
            {
                int value = 0;
                if (Int32.TryParse(item.Trim(), out value))
                {
                    numbers.Add(value);
                }
            }

            for (int i = 0; i < numbers.Count; i += 2)
            {
                Obiekt przeszkoda = new Obiekt(numbers[i], numbers[i+1]);
                przeszkoda.LoadContent(Game.content.Load<Texture2D>(@"Maps\lava"));
                //przeszkoda.getPunktObrotu = new Vector2(przeszkoda.getTekstura.Width / 2, przeszkoda.getTekstura.Height / 2);
                przeszkoda.getPunktObrotu = new Vector2(0, 0);

                listaPrzeszkod.Add(przeszkoda);
            }
        }

        public void CreateBonus()
        {
            String path ="bonus.txt";
           
            String[] items = File.ReadAllText(path).
            Split(new String[] { " ", Environment.NewLine },
            StringSplitOptions.RemoveEmptyEntries);
            List<int> numbers = new List<int>();
            foreach (String item in items)
            {
                int value = 0;
                if (Int32.TryParse(item.Trim(), out value))
                {
                    //konwersja sie powiodla.
                    numbers.Add(value);
                }
            }

            for (int i = 0; i < numbers.Count; i+=2)
            {
                Bonus bonus = new Bonus(numbers[i], numbers[i+1], typBonusu.apteczka);
                bonus.getID = nextBonusID;
                nextBonusID++;
                bonus.LoadContent(Game.content.Load<Texture2D>(@"Maps\potion"));
                bonus.getPunktObrotu = new Vector2(0, 0);
                listaBonusow.Add(bonus);
            }

 

        }

        public void CreateTrap()
        {

            String path = "trap.txt";

            String[] items = File.ReadAllText(path).
            Split(new String[] { " ", Environment.NewLine },
            StringSplitOptions.RemoveEmptyEntries);
            List<int> numbers = new List<int>();
            foreach (String item in items)
            {
                int value = 0;
                if (Int32.TryParse(item.Trim(), out value))
                {
                    numbers.Add(value);
                }
            }

            for (int i = 0; i < numbers.Count; i += 2)
            {
                Obiekt pulapka = new Obiekt(numbers[i], numbers[i + 1]);
                pulapka.LoadContent(Game.content.Load<Texture2D>(@"Maps\kolce"));
                
                pulapka.getPunktObrotu = new Vector2(0, 0);

                listaPulapek.Add(pulapka);
            }

        }

        public override void LoadContent(Texture2D tlo)
        {
            CreateWalls();
            CreateBonus();
            CreateTrap();
            base.LoadContent(tlo);
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            base.Draw(gameTime, spriteBatch);
            for (int i = 0; i < listaPrzeszkod.Count; i++)
            {
                listaPrzeszkod.ElementAt(i).Draw(gameTime, spriteBatch);

            }

            for (int i = 0; i < listaBonusow.Count; i++)
            {
                if(listaBonusow.ElementAt(i).getCzyZlapany==0)
                listaBonusow.ElementAt(i).Draw(gameTime, spriteBatch);

            }

            for (int i = 0; i < listaPulapek.Count; i++)
            {
                
                    listaPulapek.ElementAt(i).Draw(gameTime, spriteBatch);

            }
            
        }

        public void bonusReset()
        {
            for (int i = 0; i < listaBonusow.Count; i++)
            {
                listaBonusow.ElementAt(i).getCzyZlapany = 0;

            }

        }

        public int getMapOffset
        {
            get { return mapOffset; }
            set { mapOffset = value; }
        }

        public List<Bonus> getListaBonusow
        {
            get { return listaBonusow; }
            set { listaBonusow = value; }
        }

        public List<Obiekt> getListaPrzeszkod
        {
            get { return listaPrzeszkod; }
            set { listaPrzeszkod = value; }
        }

        public List<Obiekt> getListaPulapek
        {
            get { return listaPulapek; }
            set { listaPulapek = value; }
        }
    }
}
