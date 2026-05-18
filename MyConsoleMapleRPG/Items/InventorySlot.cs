namespace MyConsoleMapleRPG.Items.Inventory
{
    // 인벤토리의 한 칸을 표현하는 클래스
    // 어떤 아이템인가와 몇 개인가에 대한 보관
    internal class InventorySlot
    {
        public int ItemId { get; }
        public int Count { get; private set; }
        public bool IsEquipped { get; private set; }

        public InventorySlot(int itemId, int count) // 인벤토리 슬롯
        {
            if (count <= 0) throw new ArgumentException("아이템 수량은 1 이상이어야 합니다.");

            ItemId = itemId;
            Count = count;
        }
        public void SetEquipped(bool value) // 장비 착용 설정
        {
            IsEquipped = value;
        }

        public void AddCount(int amount) // 개수 늘리기
        {
            if (amount <= 0) return;

            Count += amount;
        }

        public void RemoveCount(int amount) // 개수 줄이기
        {
            if (amount <= 0) return;

            Count = Math.Max(0, Count - amount);
        }


    }
}