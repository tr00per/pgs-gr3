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

           
            if(currentMouseState != originalMouseState)
                if (currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        System.Console.WriteLine("strzal z myszki");
                        Game.zawodnik.getPozycjaKursora = new Vector2(currentMouseState.X + Game.kamera.getPozycja.X - Game.graphics.PreferredBackBufferWidth / 2, currentMouseState.Y + Game.kamera.getPozycja.Y - Game.graphics.PreferredBackBufferHeight / 2);
                       //System.Console.WriteLine(currentMouseState.X + "    " + currentMouseState.Y); 
                    }


                    if (currentMouseState.X != originalMouseState.X)
                    {
                        if (currentMouseState.X > 0 && currentMouseState.X < 800 && currentMouseState.Y > 0 && currentMouseState.Y < 600)

                           // Game.zawodnik.getPozycjaKursora = new Vector2(currentMouseState.X, currentMouseState.Y);
                        System.Console.WriteLine(currentMouseState.X+"    "+ currentMouseState.Y); 
                        
                    }

                
                   
                    this.originalMouseState = Mouse.GetState();
                   
                
            
        }

    }
}
