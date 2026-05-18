using MyConsoleMapleRPG.Character;

namespace MyConsoleMapleRPG.Systems
{
    // 사우나/여관 같은 회복 시설의 실제 회복 규칙 담당
    // 비용 지불과 HP/MP 전체 회복 처리
    internal class SaunaService
    {
        public const int RecoverCost = 50;

        public bool Recover(Player player, out string message) // 체력 회복
        {
            if (player == null)
            {
                message = "플레이어 정보가 없습니다.";
                return false;
            }

            if (!player.SpendGold(RecoverCost))
            {
                message = "골드가 부족합니다.";
                return false;
            }

            player.FullRecover();

            message = "회복되었습니다.";
            return true;
        }
    }
}