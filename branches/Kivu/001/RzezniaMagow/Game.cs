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
        public static ContentManager content;
        public static Mapa map;
        public static ClientLogic client;
        SpriteBatch spriteBatch;
        
        Klawiatura klawiatura;
        Myszka mysz;

        public static Kamera2d kamera;
       

        Texture2D karta4;

        Texture2D cel;
       

        public Game()
        {
            
            graphics = new GraphicsDeviceManager(this);
            content = new ContentManager(Services);
            content.RootDirectory = "Content";

            client = new ClientLogic();
            klawiatura = new Klawiatura();
            mysz = new Myszka();


            map = new Mapa(0, 0);
            
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
            base.Initialize();
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;


            IsMouseVisible = true;
            graphics.IsFullScreen = false;

            zawodnik = new Gracz(7, "kivu", 1);
            zawodnik.getPozycja = new Vector2(0, 0);
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


           
            
            karta4 = content.Load<Texture2D>("Arena");

            
            cel = content.Load<Texture2D>("cel");
            map.LoadContent(content.Load<Texture2D>(@"Maps\mapa"));
           // zawodnik.LoadContent(content.Load<Texture2D>(@"Avatar\Angels"));
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

            //wysy³anie do logiki klienta informacji o graczu

            if(client.getCzyGra && gameTime.ElapsedGameTime.Milliseconds/10==5)
            client.sendUpdate(client.clientProtocol.createPackage(zawodnik));

            mysz.procesMyszy();
            klawiatura.procesKlawiatury();

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

           

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


           if (client.getCzyGra)
           {
                map.Draw(gameTime, spriteBatch);
               

                
                //spriteBatch.Draw(karta4, new Vector2(500, 0), Color.White);

                spriteBatch.Draw(zawodnik.getTekstura, zawodnik.getPozycja, Color.White);
                spriteBatch.Draw(cel, zawodnik.getPozycjaKursora, Color.White);

                
            }

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
