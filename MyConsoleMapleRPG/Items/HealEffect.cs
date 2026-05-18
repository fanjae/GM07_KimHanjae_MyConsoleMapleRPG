using MyConsoleMapleRPG.Character;

namespace MyConsoleMapleRPG.Items.Effects
{
    // HP를 회복 시키는 아이템 효과
    // ItemEffect를 구현하여, ItemData의 Effect로 공통 방식으로 사용할 수 있다.
    internal class HealEffect : IItemEffect
    {
        private readonly int amount;

        public HealEffect(int amount)
        {
            this.amount = amount;
        }

        public void Apply(Player player)
        {
            player.Heal(amount);
        }
    }
}