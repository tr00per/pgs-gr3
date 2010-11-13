using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.IO;


namespace RzezniaMagow
{


    public class Klawiatura
    {



        private KeyboardState stanKlawiatury;
        private KeyboardState poprzedniStanKlawiatury;


        public Keys GORA = Keys.Up;
        public Keys DOL = Keys.Down;
        public Keys PRAWO = Keys.Right;
        public Keys LEWO = Keys.Left;
        public Keys STRZAL = Keys.Space;

        public Keys KONSOLA = Keys.Tab;

        private int walkSpeed = 2;


        public Klawiatura()
        {
            this.stanKlawiatury = Keyboard.GetState();
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

            }

            if (this.stanKlawiatury.IsKeyDown(DOL))
            {
                if (Game.zawodnik.getPozycja.Y < Game.map.getTekstura.Height)
                Game.zawodnik.getPozycja = new Vector2(Game.zawodnik.getPozycja.X, Game.zawodnik.getPozycja.Y + walkSpeed);

                Game.kamera.getPozycja = Game.zawodnik.getPozycja;

            }
            if (this.stanKlawiatury.IsKeyDown(GORA))
            {
                if(Game.zawodnik.getPozycja.Y>0)
                Game.zawodnik.getPozycja = new Vector2(Game.zawodnik.getPozycja.X, Game.zawodnik.getPozycja.Y - walkSpeed);

                Game.kamera.getPozycja = Game.zawodnik.getPozycja;


            }


            if (this.stanKlawiatury.IsKeyDown(LEWO))
            {
                if (Game.zawodnik.getPozycja.X > 0)
                Game.zawodnik.getPozycja = new Vector2(Game.zawodnik.getPozycja.X - walkSpeed, Game.zawodnik.getPozycja.Y);

                Game.kamera.getPozycja = Game.zawodnik.getPozycja;
            }

            if (this.stanKlawiatury.IsKeyDown(PRAWO))
            {
                if (Game.zawodnik.getPozycja.X < Game.map.getTekstura.Width)
                Game.zawodnik.getPozycja = new Vector2(Game.zawodnik.getPozycja.X + walkSpeed, Game.zawodnik.getPozycja.Y);

                Game.kamera.getPozycja = Game.zawodnik.getPozycja;

            }

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