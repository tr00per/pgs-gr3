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
        int leftrightRot = 0;


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

                    if (currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        
                    }


                    if (currentMouseState.X != originalMouseState.X)
                    {
                        if(currentMouseState.X>0 && currentMouseState.X<1024 && currentMouseState.Y>0 && currentMouseState.Y<768)

                        System.Console.WriteLine("pozycja X: " + currentMouseState.X + " pozycja Y: " + currentMouseState.Y);
                        
                    }


                   
                    this.originalMouseState = Mouse.GetState();
                    leftrightRot = 0;

                
            
        }

    }
}
