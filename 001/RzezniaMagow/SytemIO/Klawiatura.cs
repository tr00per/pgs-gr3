using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Threading;




namespace RzezniaMagow
{


    public class Klawiatura
    {
        private KeyboardState stanKlawiatury;
        private KeyboardState poprzedniStanKlawiatury;


        public Keys GORA = Keys.W;
        public Keys DOL = Keys.S;
        public Keys PRAWO = Keys.D;
        public Keys LEWO = Keys.A;
        public Keys STRZAL = Keys.Space;

        public Keys KONSOLA = Keys.Tab;
        private Vector2 staraPozycja;
        private Vector2 nowaPozycja;

        public Klawiatura()
        {
            this.stanKlawiatury = Keyboard.GetState();
            
            staraPozycja = new Vector2();
            nowaPozycja = new Vector2();
        }

        /// <summary>
        /// Funkcja obsługujaca pojedyńcze wciśniecie przycisku
        /// </summary>
        /// <param name="key">wartość klawisza</param>
        /// <returns>flaga potwierdzenia wciśniecia klawisza</returns>
        private bool KeyJustPressed(Keys key)
        {
            return stanKlawiatury.IsKeyDown(key) && poprzedniStanKlawiatury.IsKeyUp(key);
        }

        /// <summary>
        /// Funkcja realizacje działania przycisków.
        /// </summary>
        public void procesKlawiatury()
        {
            poprzedniStanKlawiatury = stanKlawiatury;
            stanKlawiatury = Keyboard.GetState();



            if (stanKlawiatury.IsKeyDown(KONSOLA))
            {
                Game.konsola = true;
            }
            if (stanKlawiatury.IsKeyUp(KONSOLA))
            {
                Game.konsola = false;
            }

            if (KeyJustPressed(Keys.Escape))
            {
                Console.WriteLine("GAME: Exiting...");
                if (Game.czySerwer)
                {
                    Game.serwer.fuckinStop();
                }
                else
                {
                    Game.client.fuckinStop();
                }

                Console.WriteLine("GAME: Almost done.");
                Program.game.Exit();

            }

            if (KeyJustPressed(Keys.D1))
            {
              //  Game.czyNowaRunda = true;
               // SerwerProtocol prot = new SerwerProtocol();
              //  byte[] data = prot.createPackage(Game.serwer.getPlayers, Game.serwer.getBullets, Game.map.getListaBonusow, Common.PACKET_BEGIN, 5);
              //  Game.serwer.sendUpdate(Common.PACKET_BEGIN, data);
                
            }
            if (KeyJustPressed(Keys.D4))
            {
               

            }

            if (Game.client.getCzyGra)
            {

                if (this.stanKlawiatury.IsKeyDown(DOL))
                {
                    if (Game.zawodnik.getPozycja.Y < Game.map.getTekstura.Height - Game.map.getMapOffset)
                    {
                        nowaPozycja = new Vector2(Game.zawodnik.getPozycja.X, Game.zawodnik.getPozycja.Y + Game.zawodnik.getWalkSpeed);
                        Game.zawodnik.getPozycja = nowaPozycja;
                    }
                    bool wolne = true;
                    for (int i = 0; i < Game.map.getListaPrzeszkod.Count; i++)
                        if (CollisionDetection2D.BoundingRectangle(Game.map.getListaPrzeszkod.ElementAt(i).RectanglePoints, Game.zawodnik.RectanglePoints))
                        {
                            wolne = false;
                        }
                    if (wolne)
                    {
                        staraPozycja = nowaPozycja;
                        Game.zawodnik.getPozycja = staraPozycja;
                    }
                    else
                    {
                        Game.zawodnik.getPozycja = staraPozycja;
                    }
                    Game.kamera.getPozycja = Game.zawodnik.getPozycja;


                }
                if (this.stanKlawiatury.IsKeyDown(LEWO))
                {

                    if (Game.zawodnik.getPozycja.X > Game.map.getMapOffset)
                    {
                        nowaPozycja = new Vector2(Game.zawodnik.getPozycja.X - Game.zawodnik.getWalkSpeed, Game.zawodnik.getPozycja.Y);
                        Game.zawodnik.getPozycja = nowaPozycja;
                    }

                    bool wolne = true;
                    for (int i = 0; i < Game.map.getListaPrzeszkod.Count; i++)
                        if (CollisionDetection2D.BoundingRectangle(Game.map.getListaPrzeszkod.ElementAt(i).RectanglePoints, Game.zawodnik.RectanglePoints))
                        {
                            wolne = false;
                        }
                    if (wolne)
                    {
                        staraPozycja = nowaPozycja;
                        Game.zawodnik.getPozycja = staraPozycja;
                    }
                    else
                    {
                        Game.zawodnik.getPozycja = staraPozycja;
                    }
                    Game.kamera.getPozycja = Game.zawodnik.getPozycja;

                }
                if (this.stanKlawiatury.IsKeyDown(GORA))
                {


                    if (Game.zawodnik.getPozycja.Y > Game.map.getMapOffset)
                    {
                        nowaPozycja = new Vector2(Game.zawodnik.getPozycja.X, Game.zawodnik.getPozycja.Y - Game.zawodnik.getWalkSpeed);
                        Game.zawodnik.getPozycja = nowaPozycja;
                    }

                    bool wolne = true;
                    for (int i = 0; i < Game.map.getListaPrzeszkod.Count; i++)
                        if (CollisionDetection2D.BoundingRectangle(Game.map.getListaPrzeszkod.ElementAt(i).RectanglePoints, Game.zawodnik.RectanglePoints))
                        {
                            wolne = false;
                        }
                    if (wolne)
                    {
                        staraPozycja = nowaPozycja;
                        Game.zawodnik.getPozycja = staraPozycja;
                    }
                    else
                    {
                        Game.zawodnik.getPozycja = staraPozycja;
                    }
                    Game.kamera.getPozycja = Game.zawodnik.getPozycja;

                }
                if (this.stanKlawiatury.IsKeyDown(PRAWO))
                {

                    if (Game.zawodnik.getPozycja.X < Game.map.getTekstura.Width - Game.map.getMapOffset)
                    {
                        nowaPozycja = new Vector2(Game.zawodnik.getPozycja.X + Game.zawodnik.getWalkSpeed, Game.zawodnik.getPozycja.Y);
                        Game.zawodnik.getPozycja = nowaPozycja;
                    }

                    bool wolne = true;
                    for (int i = 0; i < Game.map.getListaPrzeszkod.Count; i++)
                        if (CollisionDetection2D.BoundingRectangle(Game.map.getListaPrzeszkod.ElementAt(i).RectanglePoints, Game.zawodnik.RectanglePoints))
                        {
                            wolne = false;
                        }
                    if (wolne)
                    {
                        staraPozycja = nowaPozycja;
                        Game.zawodnik.getPozycja = staraPozycja;
                    }
                    else
                    {
                        Game.zawodnik.getPozycja = staraPozycja;
                    }
                    Game.kamera.getPozycja = Game.zawodnik.getPozycja;

                }
               
            }

        }
    }
}