using MyConsoleMapleRPG.Character;

namespace MyConsoleMapleRPG.Items.Effects
{
    // 소비 아이템 효과의 공통 인터페이스
    // 효과 종류가 달라도 Apply(Player)하나로 실행할 수 있게 함.
    internal interface IItemEffect
    {
        void Apply(Player player);
    }
}