using MyConsoleMapleRPG.Enums;
using MyConsoleMapleRPG.Items;

// 몬스터 원본 데이터를 ID 기준으로 보관하는 정적 데이터베이스
// 전투 진입 시 ID 조회 또는 랜덤 선택을 통해 Monster Data를 가져옴
namespace MyConsoleMapleRPG.Character.Monster
{
    internal static class MonsterDatabase
    {
        private static readonly Dictionary<int, MonsterData> monsters = new()
        {
            {
                1,
                new MonsterData("초록 달팽이", 15, 0, 5, 0, 10, 10, "Assets/image/green_snail.png",
                    new List<DropItem>
                    {
                        new DropItem(1001, 1, 0.1),
                        new DropItem(2001, 1, 0.01),
                        new DropItem(3001, 1, 0.3)
                    })
            },
            {
                2,
                new MonsterData("파란 달팽이", 20, 0, 6, 1, 15, 20, "Assets/image/blue_snail.png",
                    new List<DropItem>
                    {
                        new DropItem(1001, 1, 0.2),
                        new DropItem(2001, 1, 0.05),
                        new DropItem(2002, 1, 0.05),
                        new DropItem(3002, 1, 0.3)
                    })
            },
            {
                3,
                new MonsterData("빨간 달팽이", 40, 0, 9, 1, 15, 35, "Assets/image/red_snail.png",
                    new List<DropItem>
                    {
                        new DropItem(1001, 1, 0.2),
                        new DropItem(1002, 1, 0.3),
                        new DropItem(3003, 1, 0.4)
                    })
            },
            {
                4,
                new MonsterData("장로 달팽이", 100, 0, 13, 3, 15, 35, "Assets/image/king_snail.png",
                    new List<DropItem>
                    {
                        new DropItem(1001, 1, 0.2),
                        new DropItem(1002, 1, 0.3),
                        new DropItem(3003, 1, 0.2)
                    },
                    CounterEffectType.Poison, 30, 5, 3)
            }
        };

        public static MonsterData Get(int id) // 몬스터 데이터 정보 획득
        {
            if (!monsters.TryGetValue(id, out MonsterData? monsterData))
                throw new ArgumentException($"존재하지 않는 몬스터 ID입니다. ID: {id}");

            return monsterData;
        }
    }
}