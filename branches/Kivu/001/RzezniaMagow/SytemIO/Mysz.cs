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
                        if(Game.zawodnik.getPunktyMany>2)
                        if (currentMouseState.LeftButton != originalMouseState.LeftButton)
                            if (currentMouseState.LeftButton == ButtonState.Pressed)
                                if (currentMouseState.X > 0 && currentMouseState.X < 800 && currentMouseState.Y > 0 && currentMouseState.Y < 600)
                                {
                                    Game.zawodnik.getPozycjaKursora = new Vector2(currentMouseState.X + Game.kamera.getPozycja.X - Game.graphics.PreferredBackBufferWidth / 2, currentMouseState.Y + Game.kamera.getPozycja.Y - Game.graphics.PreferredBackBufferHeight / 2);

                                    Pocisk poc  = new Pocisk(Game.zawodnik.getPozycja.X, Game.zawodnik.getPozycja.Y,Game.zawodnik.getPozycjaKursora.X, Game.zawodnik.getPozycjaKursora.Y,
                                                                                (byte)Game.zawodnik.getListaPociskow.Count,Game.zawodnik.getAktualnaBron.getTypBroni, Game.zawodnik.getID);
                                    poc.getKatObrotu = (float)Math.Atan2(-(Game.zawodnik.getPozycjaKursora.X - Game.zawodnik.getPozycja.X), (Game.zawodnik.getPozycjaKursora.Y - Game.zawodnik.getPozycja.Y)) + MathHelper.Pi / 2;


                                    poc.calculateSpeed();
                                    Game.zawodnik.getListaPociskow.Add(poc);
                                    Game.zawodnik.getPunktyMany -= 2;

                                }
                            if (currentMouseState.X > 0 && currentMouseState.X < 800 && currentMouseState.Y > 0 && currentMouseState.Y < 600)
                                Game.zawodnik.getPozycjaKursora = new Vector2(currentMouseState.X + Game.kamera.getPozycja.X - Game.graphics.PreferredBackBufferWidth / 2, currentMouseState.Y + Game.kamera.getPozycja.Y - Game.graphics.PreferredBackBufferHeight / 2);
                        
                        this.originalMouseState = Mouse.GetState();
                    }        
        }
    }
}
