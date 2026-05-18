using MyConsoleMapleRPG.Character.Monster;
using MyConsoleMapleRPG.Map;

namespace MyConsoleMapleRPG.Systems
{
    // 맵 이동 중 랜덤 몬스터 조우 여부 판단.
    // 맵 데이터에 등장 가능한 몬스터 목록에 대한 실제 확률 계산 담당
    internal class EncounterService
    {
        private readonly Random random = new Random();
        private const int EncounterRate = 25;

        public Monster? TryEncounter(MapData mapData) // 인카운트 배틀 시도
        {
            if (mapData.EncounterMonsters.Count == 0) return null; // 몬스터 없으면 해당 없음

            if (random.Next(100) >= EncounterRate) return null; 

            int index = random.Next(mapData.EncounterMonsters.Count); // 몬스터를 만날 상황이면 랜덤하게 뽑아서 등장 시킴
            MonsterData data = mapData.EncounterMonsters[index]; 

            return MonsterFactory.Create(data); // 싸울 몬스터 생성
        }
    }
}