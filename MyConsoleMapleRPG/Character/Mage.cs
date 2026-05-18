using MyConsoleMapleRPG.Enums;

namespace MyConsoleMapleRPG.Character
{
    // 마법사 직업 플레이어
    // MP 성장이 높고, 고정 추가 피해를 주는 스킬
    internal class Mage : Player
    {
        public Mage() : base("마법사", 40, 50, 5, 0, JobType.Mage)
        {
            ImagePath = "Assets/image/mage.png";

            Skills.Add(new Skill("매직 클로", 15, SkillType.Active, (user, target) => user.Attack + 20));
        }

        protected override void ApplyLevelUpStats()
        {
            MaxHp += 7;
            MaxMp += 10;
            BaseAttack += 1;

            if (Level % 2 == 0)
                BaseDefense += 1;
        }

    }
}
