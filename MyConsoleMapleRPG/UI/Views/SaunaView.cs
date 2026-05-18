using MyConsoleMapleRPG.Character;
using MyConsoleMapleRPG.Systems;
using MyConsoleMapleRPG.UI.Rendering;

namespace MyConsoleMapleRPG.UI.Views
{
    // 사우나 화면의 출력 전용 View
    // 플레이어 상태, 메뉴, 메시지 ASCII 아트를 그림

    internal class SaunaView
    {
        public void DrawBase()
        {
            Console.Clear();
            ConsoleRenderer.DrawWindow(2, 1, 90, 32, "SAUNA");
            DrawSaunaArt();
        }

        public void DrawMenu()
        {
            ConsoleRenderer.DrawWindow(58, 13, 30, 18, "MENU");

            Console.SetCursorPosition(61, 16);
            Console.Write("따뜻한 사우나");

            Console.SetCursorPosition(61, 18);
            Console.Write("HP / MP 전체 회복");

            Console.SetCursorPosition(61, 20);
            Console.Write($"비용 : {SaunaService.RecoverCost}G");

            Console.SetCursorPosition(61, 24);
            Console.Write("[Z] 회복하기");

            Console.SetCursorPosition(61, 28);
            Console.Write("[ESC] 나가기");
        }

        public void DrawPlayerStatus(Player player)
        {
            ConsoleRenderer.DrawWindow(58, 4, 30, 7, "STATUS");

            Console.SetCursorPosition(61, 6);
            Console.Write($"Gold : {player.Gold}G".PadRight(20));

            ConsoleRenderer.DrawBar(61, 8, "HP", player.Hp, player.MaxHp, 5, ConsoleColor.Red);
            ConsoleRenderer.DrawBar(61, 9, "MP", player.Mp, player.MaxMp, 5, ConsoleColor.Blue);
        }

        public void DrawMessage(string message)
        {
            Console.SetCursorPosition(61, 22);
            Console.Write(new string(' ', 24));

            Console.SetCursorPosition(61, 22);
            Console.Write(message);
        }

        private void DrawSaunaArt()
        {
            string[] saunaArt =
            {
        "┌──────────────────────────────────────────────┐",
        "│        ~        ~        ~        ~          │",
        "│    ~        ♨   ♨   ♨        ~~~~~        │",
        "│                                              │",
        "│        ██████████████████████                │",
        "│        █                    █                │",
        "│        █       SAUNA        █                │",
        "│        █                    █                │",
        "│        ██████████████████████                │",
        "│                                              │",
        "│        ┌────────────────────┐                │",
        "│        │====================│                │",
        "│        │====================│                │",
        "│        │====================│                │",
        "│        └────────────────────┘                │",
        "│              ( -_- )                         │",
        "│               /| |\\          ~               │",
        "│               / \\                            │",
        "│      ~~~~~~~~ steam ~~~~~~~~~~~~~            │",
        "│                                              │",
        "└──────────────────────────────────────────────┘"
    };

            int x = 6;
            int y = 5;

            for (int i = 0; i < saunaArt.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(saunaArt[i]);
            }
        }
    }
}