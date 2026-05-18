using MyConsoleMapleRPG.Enums;
using MyConsoleMapleRPG.Items.Effects;
namespace MyConsoleMapleRPG.Items
{
    // 아이템 원본 데이터를 보관하는 정적 데이터베이스.
    // 인벤토리, 상점, 드랍 시스템은 ItemId를 통해 이곳에서 ItemData를 조회
    internal static class ItemDatabase
    {
        private static readonly Dictionary<int, ItemData> items = new()
        {
            {
                1001,
                new ItemData(
                    id: 1001,
                    name: "빨간 포션",
                    description: "HP를 30 회복합니다.",
                    price: 50,
                    type: ItemType.Consumable,
                    isStackable: true,
                    maxStackCount: 99,
                    effect: new HealEffect(30),
                    iconPath: "Assets/image/red_potion.png"
                )
            },
            {
                1002,
                new ItemData(
                    id: 1002,
                    name: "하얀 포션",
                    description: "HP를 100 회복합니다.",
                    price: 150,
                    type: ItemType.Consumable,
                    isStackable: true,
                    maxStackCount: 99,
                    effect: new HealEffect(100),
                    iconPath: "Assets/image/white_potion.png"
                )
            },
            {
                1003,
                new ItemData(
                    id: 1003,
                    name: "파란 포션",
                    description: "MP를 30 회복합니다.",
                    price: 70,
                    type: ItemType.Consumable,
                    isStackable: true,
                    maxStackCount: 99,
                    effect: new RestoreMpEffect(30),
                    iconPath: "Assets/image/blue_potion.png"
                )
            },
            {
                2001,
                new ItemData(
                    id: 2001,
                    name: "검",
                    description: "전사가 쓰는 검. 공격력 + 5",
                    price: 300,
                    type: ItemType.Equipment,
                    isStackable: false,
                    maxStackCount: 1,
                    iconPath: "Assets/image/sword_shop.png",
                    equipmentStats: new EquipmentStats(equipmentType: EquipmentType.Weapon,requiredJobs: JobType.Warrior,attackBonus: 5)
                )
            },
            {
                2002,
                new ItemData(
                    id: 2002,
                    name: "마법 지팡이",
                    description: "마법사용 무기. 공격력 +3",
                    price: 300,
                    type: ItemType.Equipment,
                    isStackable: false,
                    maxStackCount: 1,
                    iconPath: "Assets/image/magic_cane.png",
                    equipmentStats: new EquipmentStats(equipmentType: EquipmentType.Weapon,requiredJobs: JobType.Mage,attackBonus: 3)
                )
            },
            {
                2101,
                new ItemData(
                    id: 2101,
                    name: "가죽 갑옷",
                    description: "생긴것과 다르게 튼튼하다. 방어력 +4",
                    price: 250,
                    type: ItemType.Equipment,
                    isStackable: false,
                    maxStackCount: 1,
                    iconPath: "Assets/image/armor.png",
                    equipmentStats: new EquipmentStats(equipmentType: EquipmentType.Armor,requiredJobs: JobType.All,defenseBonus: 4)
                )
            },
            {
                3001,
                new ItemData(
                    id: 3001,
                    name: "초록 달팽이의 껍질",
                    description: "초록 달팽이의 껍질이다.",
                    price: 10,
                    type: ItemType.Etc,
                    isStackable: true,
                    maxStackCount: 99
                )
            },
            {
                3002,
                new ItemData(
                    id: 3002,
                    name: "파란 달팽이의 껍질",
                    description: "파란 달팽이의 껍질이다.",
                    price: 20,
                    type: ItemType.Etc,
                    isStackable: true,
                    maxStackCount: 99
                )
            },
            {
                3003,
                new ItemData(
                    id: 3003,
                    name: "빨간 달팽이의 껍질",
                    description: "빨간 달팽이의 껍질이다.",
                    price: 40,
                    type: ItemType.Etc,
                    isStackable: true,
                    maxStackCount: 99
                )
            }

        };

        public static ItemData Get(int itemId)
        {
            if (!items.TryGetValue(itemId, out ItemData? itemData))
                throw new Exception($"존재하지 않는 아이템 ID입니다. ID: {itemId}");

            if (itemData.Id != itemId)
                throw new Exception($"아이템 DB 키와 ItemData.Id가 다릅니다. Key: {itemId}, Id: {itemData.Id}");

            return itemData;
        }

        public static bool TryGet(int itemId, out ItemData? itemData)
        {
            return items.TryGetValue(itemId, out itemData);
        }

        public static bool Exists(int itemId)
        {
            return items.ContainsKey(itemId);
        }
    }
}