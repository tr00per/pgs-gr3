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

        //private int kolizja ;
        private Vector2 staraPozycja;
        private Vector2 nowaPozycja;

        public Klawiatura()
        {
            this.stanKlawiatury = Keyboard.GetState();
            //kolizja = 0;
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



            if (stanKlawiatury.IsKeyDown(Keys.Tab))
            {
                Game.konsola = true;
            }
            if (stanKlawiatury.IsKeyUp(Keys.Tab))
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

               // Game.screenManager.Visible = true;
                //Game.client.getCzyGra = false;

            }

            if (KeyJustPressed(Keys.D1))
            {
                Game.czyNowaRunda = true;
                
            }

            if (KeyJustPressed(Keys.D2))
            {

                Game.serwer.getPredkoscWysylania++;
                Console.WriteLine(Game.serwer.getPredkoscWysylania);
            }

            if (KeyJustPressed(Keys.D3))
            {
                Game.serwer.getPredkoscWysylania--;
                Console.WriteLine(Game.serwer.getPredkoscWysylania);

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
                        else
                        {
                            Game.zawodnik.getPozycja = staraPozycja;
                        }
                        

                        bool flaga = true;
                        //for (int i = 0; i < Game.map.getListaPrzeszkod.Count; i++)
                        //    if (CollisionDetection2D.BoundingRectangle(Game.map.getListaPrzeszkod.ElementAt(i).RectanglePoints, Game.zawodnik.RectanglePoints))
                        //    {
                        //       //Game.zawodnik.getPozycja = staraPozycja;
                        //       //flaga = false;
                        //        //Console.WriteLine("Wykryto kolizje");
                        //    }
                        if(flaga)
                            staraPozycja = nowaPozycja;

                        

                        Game.kamera.getPozycja = Game.zawodnik.getPozycja;
                        //wycisniety = false;

                        //System.Console.WriteLine("pozycja myszki X: " + Game.zawodnik.getPozycja.X + " pozycja myszki Y: " + Game.zawodnik.getPozycja.Y);


                    }
                     if (this.stanKlawiatury.IsKeyDown(LEWO))
                    {

                       

                        if (Game.zawodnik.getPozycja.X > Game.map.getMapOffset)
                        {
                            nowaPozycja = new Vector2(Game.zawodnik.getPozycja.X - Game.zawodnik.getWalkSpeed, Game.zawodnik.getPozycja.Y);
                            Game.zawodnik.getPozycja = nowaPozycja;
                        }
                        else
                        {
                            Game.zawodnik.getPozycja = staraPozycja;
                        }


                        bool flaga = true;
                        //for (int i = 0; i < Game.map.getListaPrzeszkod.Count; i++)
                        //    if (CollisionDetection2D.BoundingRectangle(Game.map.getListaPrzeszkod.ElementAt(i).RectanglePoints, Game.zawodnik.RectanglePoints))
                        //    {
                        //        //Game.zawodnik.getPozycja = staraPozycja;
                        //        //flaga = false;
                        //        //Console.WriteLine("Wykryto kolizje");
                        //    }
                        if (flaga)
                            staraPozycja = nowaPozycja;



                        Game.kamera.getPozycja = Game.zawodnik.getPozycja;
                        //System.Console.WriteLine("pozycja myszki X: " + Game.zawodnik.getPozycja.X + " pozycja myszki Y: " + Game.zawodnik.getPozycja.Y);


                    }
                     if (this.stanKlawiatury.IsKeyDown(GORA))
                    {
                        

                        if (Game.zawodnik.getPozycja.Y > Game.map.getMapOffset)
                        {
                            nowaPozycja = new Vector2(Game.zawodnik.getPozycja.X, Game.zawodnik.getPozycja.Y - Game.zawodnik.getWalkSpeed);
                            Game.zawodnik.getPozycja = nowaPozycja;
                        }
                        else
                        {
                            Game.zawodnik.getPozycja = staraPozycja;
                        }

                        bool flaga = true;
                        //for (int i = 0; i < Game.map.getListaPrzeszkod.Count; i++)
                        //    if (CollisionDetection2D.BoundingRectangle(Game.map.getListaPrzeszkod.ElementAt(i).RectanglePoints, Game.zawodnik.RectanglePoints))
                        //    {
                        //        //Game.zawodnik.getPozycja = staraPozycja;
                        //        //flaga = false;
                        //        //Console.WriteLine("Wykryto kolizje");
                        //    }
                        if (flaga)
                            staraPozycja = nowaPozycja;



                        Game.kamera.getPozycja = Game.zawodnik.getPozycja;
                        //System.Console.WriteLine("pozycja myszki X: " + Game.zawodnik.getPozycja.X + " pozycja myszki Y: " + Game.zawodnik.getPozycja.Y);



                    }
                     if (this.stanKlawiatury.IsKeyDown(PRAWO))
                    {
                        
                        if (Game.zawodnik.getPozycja.X < Game.map.getTekstura.Width - Game.map.getMapOffset)
                        {
                            nowaPozycja = new Vector2(Game.zawodnik.getPozycja.X + Game.zawodnik.getWalkSpeed, Game.zawodnik.getPozycja.Y);
                            Game.zawodnik.getPozycja = nowaPozycja;
                        }
                        else
                        {
                            Game.zawodnik.getPozycja = staraPozycja;
                        }


                        bool flaga = true;
                        //for (int i = 0; i < Game.map.getListaPrzeszkod.Count; i++)
                        //    if (CollisionDetection2D.BoundingRectangle(Game.map.getListaPrzeszkod.ElementAt(i).RectanglePoints, Game.zawodnik.RectanglePoints))
                        //    {
                        //        //Game.zawodnik.getPozycja = staraPozycja;
                        //        //flaga = false;
                        //        //Console.WriteLine("Wykryto kolizje");
                        //    }
                        if (flaga)
                            staraPozycja = nowaPozycja;


                        Game.kamera.getPozycja = Game.zawodnik.getPozycja;
                        //System.Console.WriteLine("pozycja myszki X: " + Game.zawodnik.getPozycja.X + " pozycja myszki Y: " + Game.zawodnik.getPozycja.Y);


                    }
                    //kolizja = 0;



                    if (KeyJustPressed(STRZAL))
                    {
                        System.Console.WriteLine("strzal");
                    }

                    if (KeyJustPressed(KONSOLA))
                    {

                        System.Console.WriteLine("konsola");
                    }
                }

        }
    }
}