using System;

namespace RzezniaMagow
{
    static class Program
    {
        public static Game game;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        static void Main(string[] args)
        {
            game = new Game();
                game.Run();


        }
    }
}

