namespace MyConsoleMapleRPG.Items
{
    // 몬스터가 드랍할 수 있는 아이템 정보를 담는 클래스
    // ItemId를 이용해 참조
    internal class DropItem
    {
        public int ItemId { get; }
        public int Count { get; }
        public double DropRate { get; }

        public DropItem(int itemId, int count, double dropRate)
        {
            if (itemId <= 0)
                throw new ArgumentException("아이템 ID는 1 이상이어야 합니다.");

            if (count <= 0)
                throw new ArgumentException("드랍 개수는 1 이상이어야 합니다.");

            if (dropRate < 0 || dropRate > 1)
                throw new ArgumentException("드랍률은 0 이상 1 이하이어야 합니다.");

            ItemId = itemId;
            Count = count;
            DropRate = dropRate;
        }
    }
}