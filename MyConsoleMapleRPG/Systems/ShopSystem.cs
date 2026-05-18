using MyConsoleMapleRPG.Shops;
using MyConsoleMapleRPG.Character;
using MyConsoleMapleRPG.Items;


namespace MyConsoleMapleRPG.Systems
{
    // 상점의 구매 / 판매 규칙을 처리하는 클래스
    // 화면 출력은 ShopView/ShopScreen가 처리
    internal class ShopSystem
    {
        
        private readonly List<ShopItem> items;

        public IReadOnlyList<ShopItem> Items
        {
            get { return items; }
        }

        public ShopSystem(List<ShopItem> items)
        {
            this.items = new List<ShopItem>(items);
        }

        public bool Buy(int selectedIndex, Player player, out string message) // 구매
        {
            message = "";

            if (selectedIndex < 0 || selectedIndex >= items.Count) // 선택된 것이 없는 경우
            {
                message = "잘못된 상품입니다.";
                return false;
            }

            ShopItem shopItem = items[selectedIndex];

            if (!shopItem.HasStock()) // 제고가 없는 경우
            {
                message = "재고가 없습니다.";
                return false;
            }

            if (!ItemDatabase.TryGet(shopItem.ItemId, out ItemData? itemData) || itemData == null) // 아이템이 존재하지 않는 경우
            {
                message = "존재하지 않는 아이템입니다.";
                return false;
            }

            if (!player.SpendGold(itemData.Price)) // 돈이 모자른 경우
            {
                message = "골드가 부족합니다.";
                return false;
            }

            player.Inventory.AddItem(itemData, 1); // 인벤토리에 아이템을 추가(실제로는 아이템의 번호만 가져간다.)
            shopItem.DecreaseStock(); // 재고가 있던 경우 재고 감소

            message = $"{itemData.Name}을(를) 구매했습니다.";
            return true;
        }

        public bool Sell(Player player, int slotIndex, out string message)
        {
            message = "";

            if (!player.Inventory.TryGetSlot(slotIndex, out var slot) || slot == null)
            {
                message = "판매할 아이템이 없습니다.";
                return false;
            }

            if (!ItemDatabase.TryGet(slot.ItemId, out ItemData? itemData) || itemData == null)
            {
                message = "존재하지 않는 아이템입니다.";
                return false;
            }

            if (slot.IsEquipped)
            {
                message = "착용 중인 장비는 판매할 수 없습니다.";
                return false;
            }

            int sellPrice = itemData.Price / 2;

            if (!player.Inventory.RemoveItemAt(slotIndex, 1))
            {
                message = "아이템 판매에 실패했습니다.";
                return false;
            }

            player.AddGold(sellPrice);
            message = $"{itemData.Name}을(를) 판매했습니다. +{sellPrice}G";
            return true;
        }
    }
}
