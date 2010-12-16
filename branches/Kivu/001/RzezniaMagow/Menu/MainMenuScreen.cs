#region File Description
//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using System.Windows.Forms;
using System.Text;
using System.Collections;
using System.Data;
using System.Xml;
using System.Linq;
using System;

#endregion

namespace RzezniaMagow
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    class MainMenuScreen : MenuScreen
    {
        #region Initialization

        
       
       

        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public MainMenuScreen()
            : base("Main Menu")
        {
            // Create our menu entries.
            MenuEntry playGameMenuEntry = new MenuEntry("Play Game");
            MenuEntry optionsMenuEntry = new MenuEntry("Create serwer");
            MenuEntry exitMenuEntry = new MenuEntry("Exit");
            
            // Hook up menu event handlers.
            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            optionsMenuEntry.Selected += CreateSerwerEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            //do odkomentowania po podlaczeniu serwera

            ClientForm ser = new ClientForm();

            ser.Show();

            //je¿eli szukasz obs³ugi dol¹czania nowego gracza idz do klasy clientForm.cs w folderze Formularz :)

        }


        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void CreateSerwerEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            Game.serwer = new ServerLogic();
            Game.serwer.serverStarted();
            Game.czySerwer =  true;
            //Console.WriteLine("SERVER RUNNING: " + Game.serwer..ToString());

            //Game.client = new ClientLogic();
            //Game.client.connect("127.0.0.1", 20000, "Kivu", 1);
            //Game.czyKlient = true;
            //Console.WriteLine("WAITING...");

            Game.screenManager.Visible = false;
            Game.screenManager.RemoveScreen(this);

            for (int i = 0; i < ScreenManager.GetScreens().Count; i++)
                Game.screenManager.RemoveScreen(ScreenManager.GetScreens().ElementAt(i));
           // ScreenManager.AddScreen(new PauseMenuScreen(), e.PlayerIndex);
        }


        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            Program.game.Exit();
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }


        #endregion
    }
}
