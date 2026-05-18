namespace MyConsoleMapleRPG.Save
{
    internal class SaveData // 플레이어 저장 정보
    {
        public string PlayerType { get; set; }

        public int Level { get; set; }
        public int Exp { get; set; }

        public int Hp { get; set; }
        public int Mp { get; set; }

        public int Gold { get; set; }

        public int MaxHp { get; set; }
        public int MaxMp { get; set; }

        public int BaseAttack { get; set; }
        public int BaseDefense { get; set; }

        public List<InventorySaveData> InventoryItems { get; set; }
    }
}