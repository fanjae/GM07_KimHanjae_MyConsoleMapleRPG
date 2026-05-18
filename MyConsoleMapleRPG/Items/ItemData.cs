using MyConsoleMapleRPG.Items.Effects;
using MyConsoleMapleRPG.Enums;

namespace MyConsoleMapleRPG.Items
{
    // 아이템의 원본 데이터를 표현하는 클래스
    // 이름, 설명, 가격, 타입, 스택 여부, 효과, 장비 스탯등을 가짐

    internal class ItemData
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public int Price { get; }
        public ItemType Type { get; }

        public bool IsStackable { get; }
        public int MaxStackCount { get; }

        public IItemEffect? Effect { get; }
        public string? IconPath { get; }

        public EquipmentStats? EquipmentStats { get; }

        public bool IsEquipment => Type == ItemType.Equipment;

        public ItemData(int id,string name,string description,int price,ItemType type,bool isStackable,int maxStackCount,IItemEffect? effect = null, string? iconPath = null, EquipmentStats? equipmentStats = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Type = type;
            IsStackable = isStackable;
            MaxStackCount = maxStackCount;
            Effect = effect;
            IconPath = iconPath;
            EquipmentStats = equipmentStats;

            Validate();
        }

        private void Validate()
        {
            if (Id <= 0)
                throw new ArgumentException("아이템 ID는 1 이상이어야 합니다.");

            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("아이템 이름은 비어 있을 수 없습니다.");

            if (Price < 0)
                throw new ArgumentException("아이템 가격은 음수일 수 없습니다.");

            if (MaxStackCount <= 0)
                throw new ArgumentException("최대 스택 수는 1 이상이어야 합니다.");

            if (!IsStackable && MaxStackCount != 1)
                throw new ArgumentException("스택 불가능 아이템의 최대 스택 수는 1이어야 합니다.");

            if (Type == ItemType.Consumable && Effect == null)
                throw new ArgumentException("소비 아이템은 Effect가 필요합니다.");

            if (Type == ItemType.Equipment && EquipmentStats == null)
                throw new ArgumentException("장비 아이템은 EquipmentStats가 필요합니다.");

            if (Type != ItemType.Equipment && EquipmentStats != null)
                throw new ArgumentException("장비가 아닌 아이템은 EquipmentStats를 가질 수 없습니다.");
        }
    }
}