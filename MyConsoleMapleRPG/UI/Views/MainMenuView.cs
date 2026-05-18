using MyConsoleMapleRPG.UI.Data;
using MyConsoleMapleRPG.UI.Rendering;
using MyConsoleMapleRPG.UI.Settings;

namespace MyConsoleMapleRPG.UI.Views
{
    // 메인 메뉴 화면의 출력 전용 View 클래스
    // 창, 로고, 메뉴 항목 표시만 담당
    // 선택 상태는 MainMenu에 전달 받아 표시
    internal class MainMenuView
    {
        private const int WindowX = Layout.MainWindowX;
        private const int WindowY = Layout.MainWindowY;
        private const int WindowWidth = Layout.MainWindowWidth;
        private const int WindowHeight = Layout.MainWindowHeight;

        private const int MenuWidth = 14;
        private const int MenuStartX = 15;
        private const int MenuStartY = 31;
        private const int MenuSpacing = 18;

        public void DrawBase()
        {
            ConsoleRenderer.DrawWindow(WindowX, WindowY, WindowWidth, WindowHeight, " TURN BASED RPG ");
            DrawLogo();
        }

        public void DrawMenuItems(string[] menuItems, int selectedIndex)
        {
            for (int i = 0; i < menuItems.Length; i++) // 메뉴 정보를 가져와서 그려주는 창
            {
                Console.SetCursorPosition(MenuStartX + i * MenuSpacing, MenuStartY);

                string text = i == selectedIndex ? $"▶ {menuItems[i]}" : $" {menuItems[i]}";

                Console.Write(text.PadRight(MenuWidth));
            }
        }

        private void DrawLogo() // 로고 그림
        {
            string[] logo = LogoArt.MainLogo;

            int logoWidth = logo.Max(line => line.Length);
            int rightPadding = 8;

            int logoX = WindowX + WindowWidth - 1 - rightPadding - logoWidth;
            int logoY = 2;

            for (int i = 0; i < logo.Length; i++)
            {
                Console.SetCursorPosition(logoX, logoY + i);
                Console.Write(logo[i]);
            }
        }
    }
}