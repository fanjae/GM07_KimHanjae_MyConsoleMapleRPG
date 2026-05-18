using MyConsoleMapleRPG.Systems;
using MyConsoleMapleRPG.UI.Settings;

namespace MyConsoleMapleRPG
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            ConsoleSetting.ConsoleLock();

            GameController game = new GameController();
            game.Run();
        }
    }
}