namespace MyConsoleMapleRPG.UI.Rendering
{
    // 여러 화면에서 공통으로 사용하는 콘솔 렌더링 유틸리티
    // 창 테두리와 상태바 UI
    // 화면별 세부 배치는 각 View 클래스 처리.
    internal static class ConsoleRenderer
    {
        public  static void DrawWindow(int x, int y, int width, int height, string title = "") // 창 밖의 틀 표현
        {
            Console.SetCursorPosition(x, y); // 상단 테두리 
            Console.Write("┌");
            Console.Write(new string('─', width - 2));
            Console.Write("┐");

            for (int row = 1; row < height - 1; row++) // 중앙 (본문 영역 비움)
            {
                Console.SetCursorPosition(x, y + row);
                Console.Write("│");
                Console.Write(new string(' ', width - 2));
                Console.Write("│");
            }

            Console.SetCursorPosition(x, y + height - 1); // 하단 테두리
            Console.Write("└");
            Console.Write(new string('─', width - 2));
            Console.Write("┘");

            if (title != "") // 창 제목 (타이틀 부분)
            {
                Console.SetCursorPosition(x + 3, y); 
                Console.Write($" {title} ");
            }
        }

        public static void DrawBar(int x,int y,string label,int current,int max,int width, ConsoleColor color)
        {
            if (max <= 0) max = 1;

            current = Math.Clamp(current, 0, max); // 값 범위 보정
            int filled = current * width / max;

            Console.SetCursorPosition(x, y);
            Console.Write(new string(' ', 5)); // 기존 출력 제거

            Console.SetCursorPosition(x, y);
            Console.Write($"{label} [");

            Console.ForegroundColor = color;
            Console.Write(new string('■', filled));
            Console.Write(new string('□', width - filled));
            Console.ResetColor();

            Console.Write("] ");

            string valueText = $" {current}/{max}";
            Console.Write(valueText.PadRight(7));
        }


    }
}
