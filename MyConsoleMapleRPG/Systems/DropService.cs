using MyConsoleMapleRPG.Character;
using MyConsoleMapleRPG.Character.Monster;
using MyConsoleMapleRPG.Items;

namespace MyConsoleMapleRPG.Systems
{
    // 몬스터 처지 후 아이템 드랍을 처리하는 서비스
    // 드랍 확률과 인벤토리 지급 관련

    internal class DropService
    {
        private readonly Random random = new Random();

        public List<string> GiveDrops(Player player, Monster monster)
        {
            List<string> dropLogs = new List<string>();

            foreach (var dropItem in monster.DropItems)
            {
                if (random.NextDouble() > dropItem.DropRate)
                    continue;

                if (!ItemDatabase.TryGet(dropItem.ItemId, out ItemData? itemData) || itemData == null)
                    continue;

                player.Inventory.AddItem(itemData, dropItem.Count);
                dropLogs.Add($"{itemData.Name} x{dropItem.Count}개를 획득했습니다.");
            }

            return dropLogs;
        }
    }
}