namespace MyConsoleMapleRPG.Character.Monster
{
    // MonsterData를 실제 전투용 Monster 객체로 변환하는 팩토리 클래스
    // 데이터 정의와 객체 생성 책임을 분리 하기 위해 사용
    internal static class MonsterFactory
    {
        public static Monster Create(MonsterData data)
        {
            return new Monster(
                data.Name,
                data.Hp,
                data.Mp,
                data.Attack,
                data.Defense,
                data.RewardExp,
                data.RewardGold,
                data.ImagePath,
                data.DropItems,
                data.CounterEffectType,
                data.CounterEffectChance,
                data.CounterEffectDamage,
                data.CounterEffectTurns
            );
        }
    }
}