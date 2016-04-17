using System;

namespace MrPhilGames
{
#if WINDOWS || LINUX

    // The main class.
    public static class Program
    {
        // The main entry point for the application.
        [STAThread]
        static void Main()
        {
            using (Global.game = new LudumDare35())
            {
                Global.game.Run();
            }
        }
    }
#endif
}
