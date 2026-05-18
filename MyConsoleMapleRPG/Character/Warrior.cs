using MyConsoleMapleRPG.Enums;

namespace MyConsoleMapleRPG.Character
{
    // 전사 직업 플레이어입니다.
    // 높은 HP/공격력/방어력 성장을 가지며, 스킬은 강한 물리 공격입니다.
    internal class Warrior : Player
    {
        public Warrior() : base("전사", 50,15,9,1,JobType.Warrior)
        {
            ImagePath = "Assets/image/warrior.png";

            Skills.Add(new Skill("파워 스트라이크", 10,SkillType.Active, (user, target) => user.Attack * 2));
        }

        protected override void ApplyLevelUpStats()
        {
            MaxHp += 12;
            MaxMp += 3;
            BaseAttack += 2;
            BaseDefense += 1;
        }


    }
}
