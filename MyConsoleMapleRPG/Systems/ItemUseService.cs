using MyConsoleMapleRPG.Character;
using MyConsoleMapleRPG.Enums;
using MyConsoleMapleRPG.Items;
using MyConsoleMapleRPG.Items.Inventory;
namespace MyConsoleMapleRPG.Systems
{
    // 인벤토리에서 선택한 아이템을 실제로 사용. 즉, 사용 방식을 결정.
    internal class ItemUseService
    {
        public bool UseItem(Inventory inventory, int slotIndex, Player player, out string message) // 아이템 사용
        {
            if (!inventory.TryGetSlot(slotIndex, out InventorySlot? slot) || slot == null)
            {
                message = "잘못된 슬롯입니다.";
                return false;
            }

            if (!ItemDatabase.TryGet(slot.ItemId, out ItemData? item) || item == null)
            {
                message = "존재하지 않는 아이템입니다.";
                return false;
            }

            if (item.Type == ItemType.Consumable) // 먹을 수 있는 아이템의 경우
            {
                if (item.Effect == null) // 이펙트가 없으면 사용 효과 없음
                {
                    message = "사용 효과가 없습니다.";
                    return false;
                }

                item.Effect.Apply(player);
                inventory.RemoveItemAt(slotIndex, 1);
                message = $"{item.Name}을(를) 사용했습니다.";
                return true;
            }

            if (item.Type == ItemType.Equipment) // 장비인 경우
            {
                return player.Equipment.ToggleEquip(slot, item, player.JobType, out message); // 장착 적용과 해제 사이에 토글
            }

            message = "사용할 수 없는 아이템입니다.";
            return false;
        }
    }
}
