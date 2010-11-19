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

        private int walkSpeed = 2;
        private bool wycisniety;


        public Klawiatura()
        {
            this.stanKlawiatury = Keyboard.GetState();

            wycisniety = true;
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

            if (KeyJustPressed(Keys.Escape))
            {
                Game.client.fuckinStop();
                Game.serwer.fuckinStop();
               // Game.screenManager.Visible = true;
                //Game.client.getCzyGra = false;

            }

            if (KeyJustPressed(Keys.D1))
            {
                Game.czyNowaRunda = true;
                
            }

            if (KeyJustPressed(Keys.D2))
            {

                Game.client = new ClientLogic();
                if (Game.client.connect("127.0.0.1", 20000, "tr00per", 2))
                    Game.client.getCzyGra = true;
                Console.WriteLine("CLIENT RUNNING: " + Game.client.isRunning().ToString());
                Console.WriteLine("WAITING...");

            }

            if (KeyJustPressed(Keys.D3))
            {
                Game.client.disconnect();
                Console.WriteLine("CLIENT RUNNING: " + Game.client.isRunning().ToString());


            }

            if (KeyJustPressed(Keys.D4))
            {
                

            }
            if (Game.client.getCzyGra)
            {
                
                    if (this.stanKlawiatury.IsKeyDown(DOL))
                    {
                        if (Game.zawodnik.getPozycja.Y + Game.zawodnik.getTekstura.Height < Game.map.getTekstura.Height)
                            Game.zawodnik.getPozycja = new Vector2(Game.zawodnik.getPozycja.X, Game.zawodnik.getPozycja.Y + walkSpeed);

                        Game.kamera.getPozycja = Game.zawodnik.getPozycja;
                        wycisniety = false;

                    }
                    else if (this.stanKlawiatury.IsKeyDown(GORA))
                    {
                        if (Game.zawodnik.getPozycja.Y > 0)
                            Game.zawodnik.getPozycja = new Vector2(Game.zawodnik.getPozycja.X, Game.zawodnik.getPozycja.Y - walkSpeed);

                        Game.kamera.getPozycja = Game.zawodnik.getPozycja;
                        wycisniety = false;

                    }
                    else if (this.stanKlawiatury.IsKeyDown(LEWO))
                    {
                        if (Game.zawodnik.getPozycja.X > 0)
                            Game.zawodnik.getPozycja = new Vector2(Game.zawodnik.getPozycja.X - walkSpeed, Game.zawodnik.getPozycja.Y);

                        Game.kamera.getPozycja = Game.zawodnik.getPozycja;
                        wycisniety = false;
                    }

                    else if (this.stanKlawiatury.IsKeyDown(PRAWO))
                    {
                        if (Game.zawodnik.getPozycja.X + Game.zawodnik.getTekstura.Width < Game.map.getTekstura.Width)
                            Game.zawodnik.getPozycja = new Vector2(Game.zawodnik.getPozycja.X + walkSpeed, Game.zawodnik.getPozycja.Y);

                        Game.kamera.getPozycja = Game.zawodnik.getPozycja;
                        wycisniety = false;
                    }
                
                //    if (this.stanKlawiatury.IsKeyUp(LEWO) || this.stanKlawiatury.IsKeyUp(GORA) || this.stanKlawiatury.IsKeyUp(DOL) || this.stanKlawiatury.IsKeyUp(PRAWO))
                //{
                //    wycisniety = true;
                //}
               

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