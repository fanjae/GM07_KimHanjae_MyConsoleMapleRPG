using MyConsoleMapleRPG.Map;

namespace MyConsoleMapleRPG.UI.Rendering
{
    // MapData의 문자 배열을 실제 콘솔 화면에 그리는 클래스
    internal class MapRenderer
    {
        private const int DrawX = 4;
        private const int DrawY = 5;

        public void DrawMap(MapData mapData, int playerX, int playerY)
        {
            for (int y = 0; y < mapData.Tiles.Length; y++)
            {
                Console.SetCursorPosition(DrawX, DrawY + y);

                for (int x = 0; x < mapData.Tiles[y].Length; x++)
                {
                    DrawTileChar(mapData, x, y, playerX, playerY);
                }
            }
        }

        public void DrawTile(MapData mapData, int x, int y, int playerX, int playerY)
        {
            Console.SetCursorPosition(DrawX + x, DrawY + y);
            DrawTileChar(mapData, x, y, playerX, playerY);
        }

        private void DrawTileChar(MapData mapData, int x, int y, int playerX, int playerY)
        {
            if (x == playerX && y == playerY)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write('P');
                Console.ResetColor();
                return;
            }

            char tile = mapData.Tiles[y][x];

            switch (tile)
            {
                case '★':
                case '!':
                case '.':
                    Console.Write(' ');
                    break;

                case '#':
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write('▓');
                    Console.ResetColor();
                    break;

                case 'S':
                case 'H':
                case 'O':
                case 'P':
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(tile);
                    Console.ResetColor();
                    break;

                case 'I':
                case 'N':
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(tile);
                    Console.ResetColor();
                    break;

                case '*':
                case ';':
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write('*');
                    Console.ResetColor();
                    break;

                case '/':
                case '\\':
                case '+':
                case '-':
                case '|':
                case '_':
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(tile);
                    Console.ResetColor();
                    break;

                case ',':
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write('░');
                    Console.ResetColor();
                    break;

                default:
                    Console.Write(tile);
                    break;
            }
        }
    }
}