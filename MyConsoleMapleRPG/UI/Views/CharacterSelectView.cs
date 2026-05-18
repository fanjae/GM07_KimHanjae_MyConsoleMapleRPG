using MyConsoleMapleRPG.UI.Rendering;

namespace MyConsoleMapleRPG.UI.Views
{  
    // 직업 선택 화면의 출력 전용 View 클래스
    // 직업 이미지와 선택 메뉴 출력.

    internal class CharacterSelectView
    {
        private const int MenuStartX = 22;
        private const int MenuY = 36;
        private const int MenuSpacing = 45;
        private const int MenuWidth = 14;

        public void DrawBaseScreen() // 직업 선택 관련 기본 창 그림
        {
            ConsoleRenderer.DrawWindow(2, 1, 90, 40, "JOB SELECT");

            Console.SetCursorPosition(38, 3);
            Console.Write("직업을 선택하세요");

            AsciiImageRenderer.Draw("Assets/image/sword.png", 10, 8);
            AsciiImageRenderer.Draw("Assets/image/staff.png", 45, 8);

            Console.Write("\x1b[0m");
            Console.ResetColor();
        }

        public void DrawMenuItems(string[] menuItems, int selectedIndex) // 메뉴 창 그림 (전사, 마법사 글자)
        {
            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.SetCursorPosition(MenuStartX + i * MenuSpacing, MenuY);

                string text = i == selectedIndex ? $"▶ {menuItems[i]}" : $" {menuItems[i]}"; // 선택 유무에 따라 화살표 표기

                Console.Write(text.PadRight(MenuWidth));
            }

            Console.Write("\x1b[0m");
            Console.ResetColor();
        }
    }
}