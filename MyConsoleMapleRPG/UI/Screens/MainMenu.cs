using MyConsoleMapleRPG.Enums;

using MyConsoleMapleRPG.UI.Views;

namespace MyConsoleMapleRPG.UI.Screens
{
    // 메인 메뉴의 입력 루프와 선택 결과 반환 담당
    // 출력은 MainMenuView에 위임
    // 선택 인덱스를 enum으로 변환하여, GameController에서 처리.

    internal class MainMenu
    {
        private readonly MainMenuView view = new MainMenuView();

        private readonly string[] menuItems =
        {
            "게임 시작",
            "불러오기",
            "CREDITS",
            "종료"
        };

        public MainMenuResult Show(string message = "")
        {
            int selectedIndex = 0;

            Console.Clear();
            view.DrawBase();
            DrawMessage(message);

            while (true)
            {
                view.DrawMenuItems(menuItems, selectedIndex);

                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.RightArrow:
                        selectedIndex = MenuInput.MoveHorizontal(selectedIndex, menuItems.Length, key);
                        break;

                    case ConsoleKey.Enter:
                        return ConvertToResult(selectedIndex);
                }
            }
        }

        private MainMenuResult ConvertToResult(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    return MainMenuResult.StartGame;
                case 1:
                    return MainMenuResult.LoadGame;
                case 2:
                    return MainMenuResult.Credits;
                case 3:
                    return MainMenuResult.Exit;
                default:
                    return MainMenuResult.Exit;
            }
        }

        private void DrawMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            int x = (95 - message.Length) / 2;
            int y = 27;

            Console.SetCursorPosition(x, y);
            Console.Write(message);
        }
    }
}