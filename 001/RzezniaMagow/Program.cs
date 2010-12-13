using System;
using System.Windows.Forms;

namespace RzezniaMagow
{
    static class Program
    {
        public static Game game;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main(string[] args)
        {
            game = new Game();
            game.Run();
        }
    }
}

