namespace MyConsoleMapleRPG.Character.StatusEffects
{
    internal abstract class StatusEffect
    {
        public string Name { get; }
        public int RemainingTurns { get; protected set; } // 남은 턴

        protected StatusEffect(string name, int remainingTurns)
        {
            Name = name;
            RemainingTurns = remainingTurns;
        }

        public bool IsExpired => RemainingTurns <= 0; // 0하면 만료 처리

        public abstract string Apply(Character target);
    }
}