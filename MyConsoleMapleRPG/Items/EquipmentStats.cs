using MyConsoleMapleRPG.Enums;

namespace MyConsoleMapleRPG.Items
{
    // 장비 아이템만 필요한 스탯 정보를 담는 데이터 클래스
    internal class EquipmentStats
    {
        public EquipmentType EquipmentType { get; }
        public JobType RequiredJobs { get; }
        public int AttackBonus { get; }
        public int DefenseBonus { get; }

        public EquipmentStats(EquipmentType equipmentType,JobType requiredJobs,int attackBonus = 0,int defenseBonus = 0)
        {
            EquipmentType = equipmentType;
            RequiredJobs = requiredJobs;
            AttackBonus = attackBonus;
            DefenseBonus = defenseBonus;
        }
    }
}
