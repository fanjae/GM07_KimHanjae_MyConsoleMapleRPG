using MyConsoleMapleRPG.Enums;
using MyConsoleMapleRPG.Items;
using MyConsoleMapleRPG.Items.Inventory;

namespace MyConsoleMapleRPG.Character
{
    // 플레이어가 착용 중인 장비 상태를 관리한다.
    // 무기,방어구 슬롯을 보관하고 장비 보너스 합계를 계산
    internal class Equipment
    {
        public ItemData? Weapon { get; private set; }
        public ItemData? Armor { get; private set; }

        private InventorySlot? weaponSlot;
        private InventorySlot? armorSlot;

        public bool ToggleEquip(InventorySlot slot, ItemData item, JobType jobType, out string message)
        {
            if (slot == null)
            {
                message = "선택한 인벤토리 슬롯이 없습니다.";
                return false;
            }
            if (item == null)
            {
                message = "선택한 아이템 정보가 없습니다.";
                return false;
            }

            if (!item.IsEquipment || item.EquipmentStats == null)
            {
                message = "장비 아이템이 아닙니다.";
                return false;
            }

            EquipmentStats stats = item.EquipmentStats;

            if ((stats.RequiredJobs & jobType) == 0)
            {
                message = "현재 직업은 착용할 수 없습니다.";
                return false;
            }

            if (slot.IsEquipped)
            {
                Unequip(slot, item, out message);
                return true;
            }

            switch (stats.EquipmentType)
            {
                case EquipmentType.Weapon:
                    weaponSlot?.SetEquipped(false);
                    weaponSlot = slot;
                    Weapon = item;
                    break;

                case EquipmentType.Armor:
                    armorSlot?.SetEquipped(false);
                    armorSlot = slot;
                    Armor = item;
                    break;

                default:
                    message = "지원하지 않는 장비 타입입니다.";
                    return false;
            }

            slot.SetEquipped(true);
            message = $"{item.Name}을(를) 착용했습니다.";
            return true;
        }
        private void Unequip(InventorySlot slot, ItemData item, out string message)
        {
            if (slot == weaponSlot)
            {
                weaponSlot.SetEquipped(false);
                weaponSlot = null;
                Weapon = null;
                message = $"{item.Name}을(를) 착용 해제했습니다.";
                return;
            }

            if (slot == armorSlot)
            {
                armorSlot.SetEquipped(false);
                armorSlot = null;
                Armor = null;
                message = $"{item.Name}을(를) 착용 해제했습니다.";
                return;
            }

            slot.SetEquipped(false);
            message = $"{item.Name}을(를) 착용 해제했습니다.";
        }

        public int TotalAttackBonus =>
            GetAttackBonus(Weapon) + GetAttackBonus(Armor);

        public int TotalDefenseBonus =>
            GetDefenseBonus(Weapon) + GetDefenseBonus(Armor);

        private int GetAttackBonus(ItemData? item)
        {
            return item?.EquipmentStats?.AttackBonus ?? 0;
        }

        private int GetDefenseBonus(ItemData? item)
        {
            return item?.EquipmentStats?.DefenseBonus ?? 0;
        }
    }
}
