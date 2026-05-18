using MyConsoleMapleRPG.Character.StatusEffects;
using MyConsoleMapleRPG.Enums;
using MyConsoleMapleRPG.Items;

namespace MyConsoleMapleRPG.Character.Monster
{
    // 전투에 실제로 등장하는 몬스터 객체
    // Character를 상속 받아서 기본 전투 능력치 사용, 보상 정보와, 드롭 아이템 등의 정보를 가짐.
   internal class Monster : Character
   {
        public int RewardExp { get; }
        public List<DropItem> DropItems { get; }
        public int RewardGold { get; }

        public CounterEffectType CounterEffectType { get; }
        public int CounterEffectChance { get; }
        public int CounterEffectDamage { get; }
        public int CounterEffectTurns { get; }

        public Monster(string name, int hp, int mp, int attack, int defense, int rewardExp, int rewardGold, string imagePath, 
            List<DropItem>? dropItems = null,CounterEffectType counterEffectType = CounterEffectType.None,int counterEffectChance = 0,int counterEffectDamage = 0,int counterEffectTurns = 0)
            : base(name, hp, mp, attack, defense)
        {
            RewardExp = rewardExp;
            RewardGold = rewardGold;

            ImagePath = imagePath;
            DropItems = dropItems ?? new List<DropItem>();

            CounterEffectType = counterEffectType;
            CounterEffectChance = counterEffectChance;
            CounterEffectDamage = counterEffectDamage;
            CounterEffectTurns = counterEffectTurns;
        }

        public override int Action(Character target) // 데미지 처리 메서드
        {
            return target.TakeDamage(Attack);
        }

        public bool TryApplyCounterEffect(Character attacker, out string message) // 상태 이상 적용
        {
            message = "";

            if (CounterEffectType == CounterEffectType.None) // 상태이상에 대한 정보가 없는 경우
                return false;

            if (Random.Shared.Next(100) >= CounterEffectChance)
                return false;

            switch (CounterEffectType)
            {
                case CounterEffectType.Poison: // 중독 상태의 경우
                    bool applied = attacker.AddStatusEffect(new PoisonEffect(CounterEffectDamage, CounterEffectTurns) );

                    if (!applied) // 상태 이상 부여 실패(이미 걸린 상태)
                    {
                        message = "";
                        return false;
                    }
                    message = $"{attacker.Name}은(는) 독에 중독되었습니다.";
                    return true;

                default:
                    return false;
            }
        }
    }
}
