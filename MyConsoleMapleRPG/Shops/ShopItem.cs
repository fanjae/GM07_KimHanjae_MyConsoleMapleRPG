namespace MyConsoleMapleRPG.Shops
{
    internal class ShopItem
    {
        public int ItemId { get; }
        public int Stock { get; private set; }

        public ShopItem(int itemId, int stock = -1) // 재고량을 명시하지 않은 경우 계속 구매 가능
        {
            ItemId = itemId;
            Stock = stock;
        }

        public bool HasStock() // 제고 확인
        {
            return Stock == -1 || Stock > 0;
        }

        public void DecreaseStock()
        {
            if (Stock > 0) Stock--;
        }
    }
}
