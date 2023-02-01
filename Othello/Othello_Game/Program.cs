using System;

namespace Othello
{
    public static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            GameController gameController = new GameController();
        }
    }
}
