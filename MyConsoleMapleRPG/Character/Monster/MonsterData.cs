
using MyConsoleMapleRPG.Character;
using MyConsoleMapleRPG.Items;
using MyConsoleMapleRPG.Enums;

// 전투에 실제로 등장하는 몬스터 객체
// Character를 상속받아 기본 전투 능력치를 사용하고, 보상 정보와 드롭 아이템을 추가로 가집니다.
namespace MyConsoleMapleRPG.Character.Monster
{
    internal class MonsterData
    {
        public string Name { get; }
        public int Hp { get; }
        public int Mp { get; }
        public int Attack { get; }
        public int Defense { get; }
        public int RewardExp { get; }
        public int RewardGold { get; }
        public string ImagePath { get; }
        public CounterEffectType CounterEffectType { get; }
        public int CounterEffectChance { get; }
        public int CounterEffectDamage { get; }
        public int CounterEffectTurns { get; }

        public List<DropItem> DropItems { get; }

        public MonsterData(string name, int hp, int mp, int attack, int defense, int rewardExp, int rewardGold, string imagePath,
         List<DropItem>? dropItems = null, CounterEffectType counterEffectType = CounterEffectType.None, int counterEffectChance = 0, int counterEffectDamage = 0, int counterEffectTurns = 0)
        {
            Name = name;
            Hp = hp;
            Mp = mp;
            Attack = attack;
            Defense = defense;
            RewardExp = rewardExp;
            RewardGold = rewardGold;
            ImagePath = imagePath;
            DropItems = dropItems ?? new List<DropItem>();

            CounterEffectType = counterEffectType;
            CounterEffectChance = counterEffectChance;
            CounterEffectDamage = counterEffectDamage;
            CounterEffectTurns = counterEffectTurns;
        }
    }
}