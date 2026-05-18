using MyConsoleMapleRPG.Character;
using MyConsoleMapleRPG.Character.Monster;
using MyConsoleMapleRPG.Map;
using MyConsoleMapleRPG.Systems;
using MyConsoleMapleRPG.UI;
using MyConsoleMapleRPG.UI.Rendering;

namespace MyConsoleMapleRPG.UI.Screens
{
    // 현재 맵 상태, 플레이어 좌표, 이동 입력 처리를 담당
    // 맵 변경 / 전투 발생 / 상점 진입 / 사우나 진입 event로 외부 GameScreen에 알림.
    // 실제 맵 출력은 MapRenderer가 담당한다. Screen는 상호작용 흐름 제어.
    internal class MapScreen
    {
        private readonly Player player;
        private readonly MapRenderer renderer = new MapRenderer();
        private readonly EncounterService encounterService = new EncounterService();

        public event Action? MapChanged; 
        public event Action<Monster>? MonsterEncountered; 

        public event Action? ShopEntered; 
        public event Action? SaunaEntered;

        private MapData currentMap;
        private int playerX;
        private int playerY;

        public string MapName
        {
            get { return currentMap.Name; }
        }
        public MapScreen(MapData mapData, Player player)
        {
            currentMap = mapData;
            this.player = player;

            playerX = mapData.StartX;
            playerY = mapData.StartY;
        }

        private void ChangeMap(MapPortal portal) // 포탈을 통한 맵이동
        {
            currentMap = portal.TargetMap;
            playerX = portal.TargetX;
            playerY = portal.TargetY;


            MapChanged?.Invoke();
        }
        public void MoveToMap(MapData nextMap, int startX, int startY) // 사망 등으로 인한 맵이동
        {
            currentMap = nextMap;
            playerX = startX;
            playerY = startY;

            MapChanged?.Invoke();
        }

        public void Draw() // 렌더러에 맵 그리기 요청
        {
            renderer.DrawMap(currentMap, playerX, playerY);
        }

        public string HandleInput(ConsoleKey key) // 입력 처리
        {
            int nextX = playerX;
            int nextY = playerY;

            switch (key)
            {
                case ConsoleKey.UpArrow: nextY--; break;
                case ConsoleKey.DownArrow: nextY++; break;
                case ConsoleKey.LeftArrow: nextX--; break;
                case ConsoleKey.RightArrow: nextX++; break;
                default: return "";
            }

            return TryMove(nextX, nextY);
        }

        private bool IsBlockedTile(char tile) // 이동 불가 타일
        {
            return "#|_+-/\\".Contains(tile);
        }

        private bool IsOutOfMap(int x, int y) // 맵밖으로 나간 경우
        {
            return y < 0 || y >= currentMap.Tiles.Length || x < 0 || x >= currentMap.Tiles[y].Length;
        }

        private bool TryHandlePortal(int x, int y, out string log) // 포탈 핸들
        {
            if (currentMap.Portals.TryGetValue((x, y), out MapPortal portal))
            {
                ChangeMap(portal);
                log = $"{currentMap.Name}으로 이동";
                return true;
            }

            log = "";
            return false;
        }

        private void MovePlayer(int nextX, int nextY) // 유저 이동
        {
            int prevX = playerX;
            int prevY = playerY;

            playerX = nextX;
            playerY = nextY;

            renderer.DrawTile(currentMap, prevX, prevY, playerX, playerY);
            renderer.DrawTile(currentMap, playerX, playerY, playerX, playerY);
        }

        private void TryHandleEncounter(char target) // 인카운터 배틀 처리
        {
            if (target != ',')
                return;

            Monster? monster = encounterService.TryEncounter(currentMap);

            if (monster != null)
                MonsterEncountered?.Invoke(monster);
        }


        private string TryMove(int nextX, int nextY)
        {
            if (IsOutOfMap(nextX, nextY))
                return "더 이상 갈 수 없습니다.";

            char target = currentMap.Tiles[nextY][nextX];

            if (IsBlockedTile(target))
                return "지나갈 수 없습니다.";

            if (TryHandlePortal(nextX, nextY, out string portalLog))
                return portalLog;

            if (TryHandleShop(target))
                return "상점 이용 완료";

            if (TryHandleSauna(target))
                return "사우나 이용 완료";

            MovePlayer(nextX, nextY);

            TryHandleEncounter(target);

            return "";
        }

        private bool TryHandleShop(char target)
        {
            if (target != '*')
                return false;

            ShopEntered?.Invoke();
            return true;
        }

        private bool TryHandleSauna(char target)
        {
            if (target != ';')
                return false;

            SaunaEntered?.Invoke();
            return true;
        }
    }
}
