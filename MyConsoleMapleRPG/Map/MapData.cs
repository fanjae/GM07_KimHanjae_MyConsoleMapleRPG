using MyConsoleMapleRPG.Character.Monster;

namespace MyConsoleMapleRPG.Map
{
    // 하나의 맵을 구성하는 데이터 클래스
    // 맵 이름, 타일 문자열, 시작 위치, 포탈, 등장 몬스터 목록을 보관
    internal class MapData 
    {
        public string Name { get; }
        public string[] Tiles { get; }
        public int StartX { get; }
        public int StartY { get; }

        public List<MonsterData> EncounterMonsters { get; } // 몬스터 인카운팅 정보

        public Dictionary<(int x, int y), MapPortal> Portals { get; }

        public MapData(string name, string[] tiles, int startX, int startY)
        {
            Name = name;
            Tiles = tiles;
            StartX = startX;
            StartY = startY;
            Portals = new Dictionary<(int x, int y), MapPortal> ();
            EncounterMonsters = new List<MonsterData>();
        }
    }
}
