namespace MyConsoleMapleRPG.Save
{
    internal class InventorySaveData // 인벤토리 저장 정보
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public bool IsEquipped { get; set; }
    }
}