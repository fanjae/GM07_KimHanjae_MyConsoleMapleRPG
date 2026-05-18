using MyConsoleMapleRPG.Character;
using MyConsoleMapleRPG.Systems;
using MyConsoleMapleRPG.UI.Views;

namespace MyConsoleMapleRPG.UI.Screens
{
    // 사우나 화면의 입력 루프를 담당
    // 회복에 대한 실제 로직은 SanuaService가 담당
    // 결과 메시지 및 출력은 SanuaView에 위임
    internal class SaunaScreen
    {
        private readonly Player player;
        private readonly SaunaService saunaService = new SaunaService();
        private readonly SaunaView view = new SaunaView();

        private string message = "";

        public SaunaScreen(Player player)
        {
            this.player = player;
        }

        public void Show() // 사우나 관련 흐름 처리 시작 부분
        {
            view.DrawBase();
            view.DrawPlayerStatus(player);
            view.DrawMenu();
            view.DrawMessage(message);

            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.Z:
                        bool success = saunaService.Recover(player, out message);

                        view.DrawPlayerStatus(player);
                        view.DrawMenu();
                        view.DrawMessage(message);

                        if (success)
                        {
                            Console.ReadKey(true);
                            return;
                        }

                        break;

                    case ConsoleKey.Escape:
                        return;
                }
            }
        }
    }
}