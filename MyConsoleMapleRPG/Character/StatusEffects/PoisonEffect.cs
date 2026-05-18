namespace MyConsoleMapleRPG.Character.StatusEffects
{
    internal class PoisonEffect : StatusEffect
    {
        private readonly int damage;

        public PoisonEffect(int damage, int remainingTurns) : base("중독", remainingTurns)
        {
            this.damage = damage;
        }

        public override string Apply(Character target) // 중독에 대한 도트템 적용
        {
            int finalDamage = target.TakeFixedDamage(damage);
            RemainingTurns--;

            return $"{target.Name}은(는) 중독으로 {finalDamage}의 피해를 입었습니다.";
        }
    }
}