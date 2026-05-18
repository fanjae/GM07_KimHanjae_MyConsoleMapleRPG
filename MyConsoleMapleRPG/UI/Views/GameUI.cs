using MyConsoleMapleRPG.Character;
using MyConsoleMapleRPG.UI.Rendering;
using MyConsoleMapleRPG.UI.Settings;

namespace MyConsoleMapleRPG.UI.Views
{
    // 필드 화면의 우측 패널 UI를 출력하는 View 클래스
    // 상태, 조작 안내, 로그 창을 그림
    internal class GameUI
    {
        private const int StatusTextX = 62;
        private const int StatusTextY = 3;

        private const int StatusBarX = 61;
        private const int HpBarY = 5;
        private const int MpBarY = 6;
        private const int ExpBarY = 7;

        private const int InfoLeftX = 61;
        private const int InfoRightX = 76;

        private const int LogX = 62;
        private const int LogY = 25;
        private const int LogWidth = 15;
        private const int LogHeight = 6;

        private const int StatusWindowY = 1;
        private const int InfoWindowY = 12;
        private const int LogWindowY = 22;

        private readonly Player player;
        private string currentLog = "게임을 시작했습니다.";

        public GameUI(Player player)
        {
            this.player = player;
        }
        public void SetLog(string log)
        {
            currentLog = log;
        }

        public void DrawMapLayout(string mapName)
        {
            ConsoleRenderer.DrawWindow(Layout.GameMapWindowX, Layout.GameMapWindowY, Layout.GameMapWindowWidth, Layout.GameMapWindowHeight, $"MAP - {mapName}");
            ConsoleRenderer.DrawWindow(Layout.SidePanelX, StatusWindowY, 34, 10, "STATUS");
            ConsoleRenderer.DrawWindow(Layout.SidePanelX, InfoWindowY, 34, 9, "INFO");
            ConsoleRenderer.DrawWindow(Layout.SidePanelX, LogWindowY, 34, 11, "LOG");
        }

        public void Draw()
        {
            DrawStatus();
            DrawInfo();
            DrawLog();
        }

        private void DrawStatus()
        {
            Console.SetCursorPosition(StatusTextX, StatusTextY);
            Console.Write($"직업 : {player.Name}  Lv : {player.Level}");

            ConsoleRenderer.DrawBar(StatusBarX, HpBarY, "HP ", player.Hp, player.MaxHp, 8, ConsoleColor.Red);
            ConsoleRenderer.DrawBar(StatusBarX, MpBarY, "MP ", player.Mp, player.MaxMp, 8, ConsoleColor.Blue);
            ConsoleRenderer.DrawBar(StatusBarX, ExpBarY, "EXP", player.Exp, player.MaxExp, 8, ConsoleColor.Yellow);

            Console.SetCursorPosition(70, 9);
            Console.Write($"ATK : {player.Attack}  DEF : {player.Defense}");
        }

        private void DrawInfo()
        {
            Console.SetCursorPosition(63, 14);
            Console.Write("[ 이동 ]");

            Console.SetCursorPosition(61, 16);
            Console.Write("↑ ↓ ← →");

            Console.SetCursorPosition(76, 14);
            Console.Write("[ 메뉴 ]");

            Console.SetCursorPosition(76, 16);
            Console.Write("I   : 인벤토리");

            Console.SetCursorPosition(76, 17);
            Console.Write("Z   : 확인");

            Console.SetCursorPosition(76, 18);
            Console.Write("S   : 저장");

            Console.SetCursorPosition(76, 19);
            Console.Write("ESC : 종료");
        }
        private void DrawLog()
        {
            // 기존 로그 영역 지우기
            for (int i = 0; i < LogHeight; i++)
            {
                Console.SetCursorPosition(LogX, LogY + i);
                Console.Write(new string(' ', LogWidth+13));
            }

            // 로그 줄바꿈 출력
            for (int i = 0; i < LogHeight; i++)
            {
                int startIndex = i * LogWidth;

                if (startIndex >= currentLog.Length)
                    break;

                string line = currentLog.Substring(startIndex,Math.Min(LogWidth, currentLog.Length - startIndex));

                Console.SetCursorPosition(LogX, LogY + i);
                Console.Write(line);
            }
        }
        public void ClearLog()
        {
            SetLog("");
        }
    }
}