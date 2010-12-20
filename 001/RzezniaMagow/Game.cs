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
using System.Threading;
using System.Timers;

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
        public static ServerLogic serwer;
        public static SpriteBatch spriteBatch;
        public static ScreenManager screenManager;
        public static Kamera2d kamera;

        private SpriteFont spriteFont;
        private SpriteFont messageFont;
        Klawiatura klawiatura;
        Myszka mysz;

        public static bool czySerwer, czyKlient, konsola, koniecGry;
        volatile public static bool czyNowaRunda;
        public static System.Timers.Timer manaTimer;

        //zmienne testowe w razie problemow do usuniecia

        Texture2D cel;
        Texture2D consola;
        Texture2D kapLewy, kapPrawy;
        Texture2D kapLewyPusty, kapPrawyPusty;
        public static string message;
        public static int czasPrzygotowania = 50;


        public Game()
        {

            graphics = new GraphicsDeviceManager(this);
            content = new ContentManager(Services);
            content.RootDirectory = "Content";

            client = new ClientLogic();
            klawiatura = new Klawiatura();
            mysz = new Myszka();

            screenManager = new ScreenManager(this);

            map = new Mapa(0, 0);

            kamera = new Kamera2d();
            czySerwer = false;
            czyNowaRunda = false;
            IsFixedTimeStep = false;
            koniecGry = false;

            message = null;
            manaTimer = new System.Timers.Timer(100);
            manaTimer.Elapsed += new ElapsedEventHandler(manaTimerTick);

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
            graphics.SynchronizeWithVerticalRetrace = true;

            IsMouseVisible = false;

            graphics.IsFullScreen = false;
            CollisionDetection2D.CDPerformedWith = UseForCollisionDetection.Rectangles;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screenManager = new ScreenManager(this);

            Components.Add(screenManager);

            // Activate the first screens.
            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);
            screenManager.LoadContent();



            spriteFont = content.Load<SpriteFont>("menufont");
            messageFont = content.Load<SpriteFont>("messagefont");

            cel = content.Load<Texture2D>("cel");
            consola = content.Load<Texture2D>("Menu/konsola");
            map.LoadContent(content.Load<Texture2D>("Maps/mapa"));
            kapLewy = content.Load<Texture2D>("pelnyLewy");
            kapPrawy = content.Load<Texture2D>("pelnyPrawy");
            kapLewyPusty = content.Load<Texture2D>("pustyLewy");
            kapPrawyPusty = content.Load<Texture2D>("pustyPrawy");
            CollisionDetection2D.AdditionalRenderTargetForCollision = new RenderTarget2D(graphics.GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, 1, graphics.GraphicsDevice.DisplayMode.Format);
            Primitives2D.dotTexture = content.Load<Texture2D>("Dot");

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

            if (czySerwer)
            {
                klawiatura.procesKlawiatury();
                for (int i = 0; i < serwer.getBullets.Count; i++)
                {
                    serwer.getBullets.ElementAt(i).updatePosition(gameTime);
                }
                serwer.removeBullets();

                if (serwer.getBullets.Count > 0)
                    serwer.bulletsCollision();

                if (serwer.getBullets.Count > 0)
                    serwer.bulletsPlayersCollision();
                serwer.removeBullets();

                serwer.bonusPlayersCollision();
                serwer.trapPlayersCollision();
                //serwer.playerPlayerCollision();
            }




            if (client.getCzyGra && Game.zawodnik.getCzyZyje && message == null && !koniecGry)
            {
                mysz.procesMyszy();
                klawiatura.procesKlawiatury();
            }

            for (int i = 0; i < client.listaPociskow.Count; i++)
            {
                client.listaPociskow.ElementAt(i).updatePosition(gameTime);
            }
            if (client.listaPociskow.Count > 0)
                client.BulletsCollision();




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
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.SaveState, kamera.getTransformation(graphics));
            if (client.getCzyGra)
            {
                map.Draw(gameTime, spriteBatch);

                for (int i = 0; i < client.listaGraczy.Count; i++)
                {
                    if (client.listaGraczy.ElementAt(i).getZycie != 0)
                    {
                        spriteBatch.Draw(client.listaGraczy.ElementAt(i).getTekstura, client.listaGraczy.ElementAt(i).getPozycja, null, Color.White,
                                        client.listaGraczy.ElementAt(i).getKatObrotu, client.listaGraczy.ElementAt(i).getPunktObrotu, 1.0f, SpriteEffects.None, 0);

                        spriteBatch.DrawString(spriteFont, (client.listaGraczy.ElementAt(i).getNick + "  " + client.listaGraczy.ElementAt(i).getZycie), new Vector2(client.listaGraczy.ElementAt(i).getPozycja.X - 25, client.listaGraczy.ElementAt(i).getPozycja.Y - 70),
                                                client.listaGraczy.ElementAt(i).getFontColor);
                    }
                }

                if (client.listaPociskow.Count > 0)
                    for (int i = 0; i < client.listaPociskow.Count; i++)
                    {
                        if (client.listaPociskow.ElementAt(i).getTrafienie == 0)
                            spriteBatch.Draw(client.listaPociskow.ElementAt(i).getTekstura, client.listaPociskow.ElementAt(i).getPozycja,
                                            null, Color.White, client.listaPociskow.ElementAt(i).getKatObrotu, client.listaPociskow.ElementAt(i).getPunktObrotu, 1.0f, SpriteEffects.None, 0);
                    }


                spriteBatch.Draw(cel, zawodnik.getPozycjaKursora, Color.White);
                rysujKapelusze(spriteBatch);

                if (message != null)
                {
                    spriteBatch.DrawString(messageFont, message, new Vector2(zawodnik.getPozycja.X - 200, zawodnik.getPozycja.Y - 200), Color.Red);
                    czasPrzygotowania--;
                    if (czasPrzygotowania < 0)
                        message = null;
                }
            }

            if (konsola)
            {
                Vector2 kons = new Vector2(zawodnik.getPozycja.X - 250, zawodnik.getPozycja.Y - 200);
                spriteBatch.Draw(consola, kons, Color.White);
                
                spriteBatch.DrawString(spriteFont, "Nick       Punkty     Smierci", new Vector2(kons.X + 50, kons.Y + 100), Color.GreenYellow);

                for (int i = 0; i < client.listaGraczy.Count; i++)
                {
                    spriteBatch.DrawString(spriteFont, client.listaGraczy.ElementAt(i).getNick, new Vector2(kons.X + 50, kons.Y + 150 + i * 30), Color.Red);
                    spriteBatch.DrawString(spriteFont, client.listaGraczy.ElementAt(i).getPunkty.ToString(), new Vector2(kons.X + 200, kons.Y + 150 + i * 30), Color.Red);
                    spriteBatch.DrawString(spriteFont, client.listaGraczy.ElementAt(i).getIloscZgonow.ToString(), new Vector2(kons.X + 360, kons.Y + 150 + i * 30), Color.Red);
                }
            }




            spriteBatch.End();

            base.Draw(gameTime);
        }


        public void rysujKapelusze(SpriteBatch spriteBatch)
        {
            Vector2 pozycjaLewa = new Vector2(Game.zawodnik.getPozycja.X + 320, Game.zawodnik.getPozycja.Y - 290);
            Vector2 pozycjaPrawa = new Vector2(Game.zawodnik.getPozycja.X - 390, Game.zawodnik.getPozycja.Y - 290);

            float poziomMany = (float)(Game.zawodnik.getPunktyMany) / 100;
            float poziomZycia = (float)(Game.zawodnik.getZycie) / 100;

            spriteBatch.Draw(kapLewyPusty, pozycjaLewa, Color.White);
            //spriteBatch.Draw(kapLewy, pozycjaLewa, new Rectangle(0, 0, kapLewy.Width, (int)(kapLewy.Height * poziomMany)), Color.White);

            spriteBatch.Draw(kapPrawyPusty, pozycjaPrawa, Color.White);
            //spriteBatch.Draw(kapPrawy, pozycjaPrawa, new Rectangle(0, 0, kapPrawy.Width, (int)(kapPrawy.Height * poziomZycia)), Color.White);
        
            spriteBatch.Draw(kapLewy, new Vector2(pozycjaLewa.X, pozycjaLewa.Y + kapLewy.Height - (int)(kapLewy.Height * poziomMany)), new Rectangle(0, kapLewy.Height - (int)(kapLewy.Height * poziomMany), kapLewy.Width, (int)(kapLewy.Height * poziomMany)), Color.White);
            spriteBatch.Draw(kapPrawy, new Vector2(pozycjaPrawa.X, pozycjaPrawa.Y + kapPrawy.Height - (int)(kapPrawy.Height * poziomZycia)), new Rectangle(0, kapPrawy.Height - (int)(kapPrawy.Height * poziomZycia), kapPrawy.Width, (int)(kapPrawy.Height * poziomZycia)), Color.White);
        
        
        
        
        
        
        }

        private void manaTimerTick(object o, ElapsedEventArgs args) 
        {
            if (zawodnik != null && zawodnik.getPunktyMany<100)
                zawodnik.getPunktyMany++;

        }














    }
}
