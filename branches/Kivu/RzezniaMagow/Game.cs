using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace RzezniaMagow
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        public static Gracz zawodnik;
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ClientProtokol klient;
        SerwerProtocol serwer;
        byte[] trescPakietu;
        byte[] odebranyPakiet;

        
        Klawiatura klawiatura;
        Myszka mysz;

        List<Gracz> listaGraczy;
        List<Pocisk> listaPociskow;
        Texture2D test;

        public static Kamera2d kamera;


        Texture2D karta1;
        Texture2D karta2;
        Texture2D karta3;
        Texture2D karta4;

        Texture2D cel;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            zawodnik = new Gracz("kivu", 1);
            zawodnik.getPozycja = new Vector2(0, 0);
            klient = new ClientProtokol();
            serwer = new SerwerProtocol();
            trescPakietu = new byte[255];
            odebranyPakiet = new byte[255];
            listaGraczy = new List<Gracz>();
            listaPociskow = new List<Pocisk>();
            klawiatura = new Klawiatura();
            mysz = new Myszka();

           

            kamera = new Kamera2d();

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;


            IsMouseVisible = true;
            graphics.IsFullScreen = false;
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            test = Content.Load<Texture2D>("Angels");
            karta1 = Content.Load<Texture2D>("Angus");
            karta2 = Content.Load<Texture2D>("Anti");
            karta3 = Content.Load<Texture2D>("Arbor");
            karta4 = Content.Load<Texture2D>("Arena");
            cel = Content.Load<Texture2D>("cel");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            //listaGraczy.Add(zawodnik);

            //listaGraczy.Add(new Gracz(3.77f,6.43f, 34));
            //listaGraczy.ElementAt(1).getListaPociskow.Add(new Pocisk(zawodnik.getPozycja.X, zawodnik.getPozycja.Y, 2));
            //listaGraczy.ElementAt(1).getListaPociskow.Add(new Pocisk(zawodnik.getPozycja.X, zawodnik.getPozycja.Y, 2));
            //listaGraczy.ElementAt(1).getListaPociskow.Add(new Pocisk(zawodnik.getPozycja.X, zawodnik.getPozycja.Y, 2));

            //listaGraczy.Add(new Gracz(3.11f, 4.43f, 19));
            //listaGraczy.ElementAt(1).getListaPociskow.Add(new Pocisk(zawodnik.getPozycja.X, zawodnik.getPozycja.Y, 3));
            //listaGraczy.ElementAt(1).getListaPociskow.Add(new Pocisk(zawodnik.getPozycja.X, zawodnik.getPozycja.Y, 3));
            //listaGraczy.ElementAt(1).getListaPociskow.Add(new Pocisk(zawodnik.getPozycja.X, zawodnik.getPozycja.Y, 3));


            //zawodnik.getListaPociskow.Add(new Pocisk(zawodnik.getPozycja.X,zawodnik.getPozycja.Y, 1));
            //zawodnik.getListaPociskow.Add(new Pocisk(zawodnik.getPozycja.X,zawodnik.getPozycja.Y, 1));
            //zawodnik.getListaPociskow.Add(new Pocisk(zawodnik.getPozycja.X,zawodnik.getPozycja.Y, 1));
            //zawodnik.getListaPociskow.Add(new Pocisk(zawodnik.getPozycja.X,zawodnik.getPozycja.Y, 1));


            //for (int i = 0; i < listaGraczy.Count; i++)
            //{
            //    for (int j = 0; j < listaGraczy.ElementAt(i).getListaPociskow.Count; j++)
            //    {
            //        listaPociskow.Add(listaGraczy.ElementAt(i).getListaPociskow.ElementAt(j));
            //    }
            //}


            //    //klient.createPackage(zawodnik, 2);
            //    //trescPakietu = klient.getTablica;

            //    //serwer.unpack(trescPakietu);

            //    //odebranyPakiet = serwer.getTablica;

            //serwer.createPackage(listaGraczy, listaPociskow, 4, 1);
            //trescPakietu = serwer.getTablica;

            //klient.unpack(trescPakietu);


            //System.Console.WriteLine("break");

            mysz.procesMyszy();
            klawiatura.procesKlawiatury();

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.SaveState, kamera.getTransformation(graphics));

            

            spriteBatch.Draw(karta1, new Vector2(0,0), Color.White);
            spriteBatch.Draw(karta2, new Vector2(500, 500), Color.White);
            spriteBatch.Draw(karta3, new Vector2(500, -500), Color.White);
            spriteBatch.Draw(karta4, new Vector2(-500, -500), Color.White);



            spriteBatch.Draw(test, zawodnik.getPozycja, Color.White);
            spriteBatch.Draw(cel, zawodnik.getPozycjaKursora, Color.White);

            spriteBatch.End();




            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
