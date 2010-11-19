using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace RzezniaMagow
{

    public class Myszka
    {



        MouseState originalMouseState;
        
        


        public Myszka()
        {
            originalMouseState = Mouse.GetState();
        }

        /// <summary>
        /// Funkcja realizacje działania przycisków i ruchu myszy.
        /// </summary>
        public void procesMyszy()
        {
            

                    MouseState currentMouseState = Mouse.GetState();
                    if (Game.client.getCzyGra)
                    {

                        if (currentMouseState.LeftButton != originalMouseState.LeftButton)
                            if (currentMouseState.LeftButton == ButtonState.Pressed)
                                if (currentMouseState.X > 0 && currentMouseState.X < 800 && currentMouseState.Y > 0 && currentMouseState.Y < 600)
                            {
                                System.Console.WriteLine("strzal z myszki");
                                Game.zawodnik.getPozycjaKursora = new Vector2(currentMouseState.X + Game.kamera.getPozycja.X - Game.graphics.PreferredBackBufferWidth / 2, currentMouseState.Y + Game.kamera.getPozycja.Y - Game.graphics.PreferredBackBufferHeight / 2);

                                Game.zawodnik.getListaPociskow.Add(new Pocisk(Game.zawodnik.getPozycja.X, Game.zawodnik.getPozycja.Y, (byte)Game.zawodnik.getListaPociskow.Count,
                                                                                Game.zawodnik.getAktualnaBron.getTypBroni, Game.zawodnik.getID));
                            }


                        //if (currentMouseState.X != originalMouseState.X)
                        //{
                            if (currentMouseState.X > 0 && currentMouseState.X < 800 && currentMouseState.Y > 0 && currentMouseState.Y < 600)
                            Game.zawodnik.getPozycjaKursora = new Vector2(currentMouseState.X + Game.kamera.getPozycja.X - Game.graphics.PreferredBackBufferWidth / 2, currentMouseState.Y + Game.kamera.getPozycja.Y - Game.graphics.PreferredBackBufferHeight / 2);

                       // }
                        //System.Console.WriteLine("pozycja myszki X: " + currentMouseState.X + " pozycja myszki Y: " + currentMouseState.X);


                        this.originalMouseState = Mouse.GetState();
                    }  
                
            
        }

    }
}
