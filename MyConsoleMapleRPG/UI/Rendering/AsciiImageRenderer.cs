using System.Drawing;

namespace MyConsoleMapleRPG.UI.Rendering
{

    // 이미지 파일을 읽어서 콘솔 배경색 기반의 ASCII/픽셀 아트 출력하는 렌더러
    // UI 전용 유틸.
    internal static class AsciiImageRenderer
    {
        public static void Draw(string path, int x = 0, int y = 0, int? resizeX = null, int? resizeY = null)
        {
            if (!File.Exists(path))
            {
                Console.SetCursorPosition(x, y);
                Console.Write($"이미지 없음: {path}");

                throw new FileNotFoundException($"이미지 파일을 찾을 수 없습니다. Path: {path}, FullPath: {Path.GetFullPath(path)}",path);
            }

            using Bitmap original = new Bitmap(path);

            // 이미지 크기 조정. 명세가 없으면 기본 사용

            int width = resizeX.HasValue ? resizeX.Value : original.Width; 
            int height = resizeY.HasValue ? resizeY.Value : original.Height;

            using Bitmap bitmap = new Bitmap(original, new Size(width, height));

            for (int row = 0; row < bitmap.Height; row++)
            {
                Console.SetCursorPosition(x, y + row);

                for (int col = 0; col < bitmap.Width; col++)
                {
                    Color color = bitmap.GetPixel(col, row);

                    if (color.A == 0) // 투명도 값이 0인 경우
                    {
                        Console.Write("\x1b[0m "); // 서식을 기본 상태로 초기화
                        continue;
                    }

                    Console.Write($"\x1b[48;2;{color.R};{color.G};{color.B}m ");
                }
            }

            Console.Write("\x1b[0m");
            Console.ResetColor();
        }
    }
}