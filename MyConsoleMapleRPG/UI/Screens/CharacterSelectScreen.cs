using MyConsoleMapleRPG.UI;
using MyConsoleMapleRPG.UI.Audio;
using MyConsoleMapleRPG.UI.Views;

namespace MyConsoleMapleRPG.UI.Screens
{
    // 직업 선택 화면의 입력 흐름을 담당하는 Screen 클래스
    // 실제 출력은 CharacterSelectview에 위임. 클래스는 선택 인덱스와 입력 처리만 관리

    internal class CharacterSelectScreen
    {
        private readonly CharacterSelectView view = new CharacterSelectView();

        private readonly string[] menuItems =
        {
            "전사",
            "마법사"
        };

        public int Show() 
        {
            int selectedIndex = 0;

            Console.Clear();
            view.DrawBaseScreen();

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

                    case ConsoleKey.Z:
                    case ConsoleKey.Enter:
                        return selectedIndex;

                    case ConsoleKey.Escape:
                        return -1;
                }
            }
        }
    }
}