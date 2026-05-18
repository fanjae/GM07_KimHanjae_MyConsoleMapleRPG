using MyConsoleMapleRPG.Character.StatusEffects;

namespace MyConsoleMapleRPG.Character
{
    // 플레이어와 몬스터 공통으로 가지는 기본 능력치와 전투 행동을 정의하는 추상 클래스
    // 직접 생성하지 않고, Player, Monster 같은 하위 클래스에서 상속해서 사용

    abstract class Character
    {
        public string ImagePath { get; protected set; }
        private string name;
        private int hp;
        private int maxHp;
        private int mp;
        private int maxMp;
        private int baseAttack;
        private int baseDefense;

        private readonly List<StatusEffect> statusEffects = new();

        public IReadOnlyList<StatusEffect> StatusEffects => statusEffects;

        public virtual int Attack => BaseAttack;
        public virtual int Defense => BaseDefense;

        public Character(string name, int hp, int mp, int attack, int defense) // 캐릭터 생성자
        {
            Name = name;

            MaxHp = hp;
            Hp = hp;

            MaxMp = mp;
            Mp = mp;

            BaseAttack = attack;
            BaseDefense = defense;
        }

        public string Name // 이름 프로퍼티
        {
            get { return name; }
            protected set { name = value; }
        }
        public int Hp // 체력 프로퍼티
        {
            get { return hp; }
            protected set
            {
                if (value <= 0) hp = 0;
                else hp = value;
            }
        }

        public int MaxHp // 체력 프로퍼티
        {
            get { return maxHp; }
            protected set
            {
                if (value <= 0) maxHp = 1;
                else maxHp = value;
            }
        }

        public int Mp // 마나 프로퍼티
        {
            get { return mp; }
            protected set
            {
                if (value <= 0) mp = 0;
                else mp = value;
            }
        }

        public int MaxMp // 마나 프로퍼티
        {
            get { return maxMp; }
            protected set
            {
                if (value < 0) maxMp = 0;
                else maxMp = value;
            }
        }

        public int BaseAttack
        {
            get { return baseAttack; }
            protected set
            {
                if (value <= 0) baseAttack = 1;
                else baseAttack = value;
            }
        }

        public int BaseDefense
        {
            get { return baseDefense; }
            protected set
            {
                if (value <= 0) baseDefense = 0;
                else baseDefense = value;
            }
        }


        public virtual int TakeDamage(int damage) // 데미지 입음 
        {
            int finalDamage = damage - Defense;

            if (finalDamage < 1)
                finalDamage = 1;

            Hp -= finalDamage;

            return finalDamage;
        }

        public int TakeFixedDamage(int damage) // 도트뎀(현재는 중독에만 사용하고 있음)
        {
            if (damage <= 0)
                return 0;

            Hp -= damage;
            return damage;
        }

        public bool AddStatusEffect(StatusEffect effect) // 상태이상 부여 메서드
        {
            // 중복 메커니즘 방지
            if (statusEffects.Any(e => e.GetType() == effect.GetType()))
                return false;

            statusEffects.Add(effect);
            return true;
        }

        public List<string> ProcessStatusEffects() // 상태 이상 적용시 발생한 효과 처리 메서드
        {
            List<string> messages = new();

            foreach (StatusEffect effect in statusEffects) // 현재 적용 중인 상태 이상 적용
            {
                string message = effect.Apply(this);

                if (!string.IsNullOrWhiteSpace(message))
                    messages.Add(message);
            }

            statusEffects.RemoveAll(effect => effect.IsExpired); // 턴 확인 후 만료된 효과 모두 제거

            return messages;
        }
        public abstract int Action(Character target); // 모든 Character는 Action을 상속받아 처리한다. (Battle시 사용)

    }
}