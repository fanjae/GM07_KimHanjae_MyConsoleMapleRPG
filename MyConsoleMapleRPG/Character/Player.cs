using MyConsoleMapleRPG.Enums;
using MyConsoleMapleRPG.Items.Inventory;

namespace MyConsoleMapleRPG.Character
{
    // 플레이어 캐릭터의 공통 기능을 담당하는 추상 클래스입니다.
    // 직업별 플레이어(Warrior, Mage)는 이 클래스를 상속해서 스킬과 레벨업 성장치를 구현합니다.
    internal abstract class Player : Character
    {
        public int Level { get; protected set; }
        public int Exp { get; protected set; }
        public int MaxExp { get; protected set; }

        public int Gold { get; private set; }

        public JobType JobType { get; }
        public Equipment Equipment { get; } = new Equipment();

        public List<Skill> Skills { get; } = new();

        public override int Attack => BaseAttack + Equipment.TotalAttackBonus;
        public override int Defense => BaseDefense + Equipment.TotalDefenseBonus;

        public Inventory Inventory { get; } = new Inventory();

        public Player(string name, int hp, int mp, int attack, int defense, JobType jobType, int gold = 100) : base(name, hp, mp, attack, defense)
        {
            Level = 1;
            Exp = 0;
            MaxExp = 100;
            Gold = gold;
            JobType = jobType;
        }
        public override int Action(Character target)
        {
            return target.TakeDamage(Attack);
        }

        public int GainExp(int amount) // 경험치를 받음
        {
            if (amount <= 0) return 0;

            Exp += amount;

            int levelUpCount = 0;

            while (Exp >= MaxExp) // 경험치를 넘어간 경우
            {
                Exp -= MaxExp;
                LevelUp();
                levelUpCount++;
            }

            return levelUpCount;
        }
        public void AddGold(int amount)
        {
            if (amount <= 0) return;

            Gold += amount;
        }

        public bool SpendGold(int amount)
        {
            if (amount <= 0) return false;

            if (Gold < amount) return false;

            Gold -= amount;
            return true;
        }

        private void LevelUp() // 레벨업 
        {
            Level++;
            MaxExp += 50;

            ApplyLevelUpStats(); // 스탯 업그레이드
            FullRecover(); // 체력 회복
        }

        public void Heal(int amount)
        {
            Hp += amount;

            if (Hp > MaxHp)
                Hp = MaxHp;
        }

        public void RestoreMp(int amount)
        {
            Mp += amount;

            if (Mp > MaxMp)
                Mp = MaxMp;
        }

        public void FullRecover()
        {
            Hp = MaxHp;
            Mp = MaxMp;
        }
        public void ConsumeMp(int amount)
        {
            if (amount <= 0) return ;
            Mp -= amount;
        }
        public bool UseSkill(int skillIndex, Character target, out string message)
        {
            if (skillIndex < 0 || skillIndex >= Skills.Count)
            {
                message = "잘못된 스킬입니다.";
                return false;
            }

            Skill skill = Skills[skillIndex];
            return skill.Use(this, target, out message);
        }

        public void LoseExpByPercent(int percent)
        {
            if (percent <= 0) return;

            int lostExp = Exp * percent / 100;
            Exp -= lostExp;

            if (Exp < 0)
                Exp = 0;
        }

        public void RestoreState(int level,int exp,int hp,int maxHp,int mp,int maxMp,int baseAttack,int baseDefense,int gold)
        {
            Level = level;
            Exp = exp;

            MaxHp = maxHp;
            Hp = hp;

            MaxMp = maxMp;
            Mp = mp;

            BaseAttack = baseAttack;
            BaseDefense = baseDefense;

            Gold = gold;
        }

        protected abstract void ApplyLevelUpStats();


    }
}
