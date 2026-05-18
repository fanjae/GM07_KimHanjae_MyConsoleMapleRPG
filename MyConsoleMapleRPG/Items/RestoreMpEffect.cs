using MyConsoleMapleRPG.Character;

namespace MyConsoleMapleRPG.Items.Effects
{
    // MP를 회복 시키는 아이템 효과
    internal class RestoreMpEffect : IItemEffect
    {
        private readonly int amount;

        public RestoreMpEffect(int amount)
        {
            this.amount = amount;
        }

        public void Apply(Player player)
        {
            player.RestoreMp(amount);
        }
    }
}