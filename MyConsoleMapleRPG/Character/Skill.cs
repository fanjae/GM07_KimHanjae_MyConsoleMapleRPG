using MyConsoleMapleRPG.Enums;
namespace MyConsoleMapleRPG.Character
{
    internal class Skill
    {
        public string Name { get; }
        public int MpCost { get; }
        public SkillType SkillType { get; }
        public Func<Player, Character, int> DamageFormula { get; }

        public Skill(string name, int mpCost, SkillType skillType,  Func<Player, Character, int> damageFormula)
        {
            Name = name;
            MpCost = mpCost;
            SkillType = skillType;
            DamageFormula = damageFormula;
        }

        public bool Use(Player user, Character target, out string message)
        {
            if (user.Mp < MpCost)
            {
                message = "MP가 부족합니다.";
                return false;
            }

            user.ConsumeMp(MpCost);

            int damage = DamageFormula(user, target);

            int finalDamage = target.TakeDamage(damage);
            message = $"{user.Name}의 {Name}! {target.Name}에게 {finalDamage}의 피해를 입혔습니다.";
            return true;
        }
    }
}
