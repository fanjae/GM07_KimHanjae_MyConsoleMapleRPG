using MyConsoleMapleRPG.Character;
using MyConsoleMapleRPG.Character.Monster;
using MyConsoleMapleRPG.Enums;

namespace MyConsoleMapleRPG.Systems
{
    // 전투의 실제 규칙을 담당하는 클래스
    // 화면 입력/출력은 BattleUI, BattleScreen 처리
    internal class BattleSystem
    {
        private readonly List<string> rewardLogs = new();
        public IReadOnlyList<string> RewardLogs => rewardLogs;
        private readonly Queue<string> pendingLogs = new();

        private readonly DropService dropService = new DropService();

        private readonly Player player;
        private readonly Monster monster;

        public BattleSystem(Player player, Monster monster)
        {
            this.player = player;
            this.monster = monster;
        }

        private BattleResult PlayerAction(string actionLog, out string log) // 플레이어 턴
        {
            log = actionLog;

            if (monster.Hp <= 0) // HP가 0이면 플레이어 승리
            {
                rewardLogs.Clear(); // 기존 보상 관련 로그를 모두 지운다.

                int levelUpCount = player.GainExp(monster.RewardExp);
                player.AddGold(monster.RewardGold);

                log = $"{monster.Name}을 처치했습니다.";

                rewardLogs.Add($"경험치 {monster.RewardExp}를 획득했습니다.");
                if (levelUpCount > 0)
                {
                    rewardLogs.Add($"레벨업! 현재 레벨: Lv.{player.Level}");
                }
                rewardLogs.Add($"골드 {monster.RewardGold}를 획득했습니다.");
                List<string> dropLogs = dropService.GiveDrops(player, monster);

                foreach (string dropLog in dropLogs)
                {
                    rewardLogs.Add(dropLog);
                }

                return BattleResult.Victory;
            }

            return MonsterTurn(ref log); // 상대 턴으로 넘긴다. 
        }
        public BattleResult PlayerAttack(out string log) // 플레이어 기본 공격
        {
            BattleResult result = ProcessPlayerStatusEffects(out log); // 상태이상 처리

            if (result != BattleResult.None)
                return result;

            int damage = player.Action(monster);
            string attackLog = $"{player.Name}의 공격! {monster.Name}에게 {damage}의 피해를 입혔습니다.";

            if (monster.TryApplyCounterEffect(player, out string counterMessage)) // 플레이어가 공격할 때 적용되는 상태이상이 있는 경우
                AddLog(counterMessage);

            return PlayerAction(attackLog, out log);
        }

        public BattleResult PlayerSkill(int skillIndex, out string log)
        {
            BattleResult result = ProcessPlayerStatusEffects(out log); // 상태이상 처리

            if (result != BattleResult.None)
                return result;

            bool success = player.UseSkill(skillIndex, monster, out string skillLog);

            if (!success)
            {
                log = skillLog;
                return BattleResult.InvalidAction;
            }

            if (monster.TryApplyCounterEffect(player, out string counterMessage)) // 플레이어가 공격할 때 적용되는 상태이상이 있는 경우
                AddLog(counterMessage);

            return PlayerAction(skillLog, out log);
        }

        public BattleResult Escape(out string log) // 도망
        {
            log = "도망쳤습니다.";
            return BattleResult.Escape;
        }

        private BattleResult MonsterTurn(ref string log) // 몬스터 턴
        {
            int damage = monster.Action(player);
            AddLog($"{monster.Name}의 반격! {player.Name}은(는) {damage}의 피해를 입었습니다.");

            if (player.Hp <= 0)
            {
                AddLog($"{player.Name}이 쓰러졌습니다.");
                return BattleResult.Defeat;
            }

            return BattleResult.None;
        }

        private BattleResult ProcessPlayerStatusEffects(out string log) // 상태이상 처리
        {
            log = "";

            List<string> statusLogs = player.ProcessStatusEffects();

            foreach (string statusLog in statusLogs)
            {
                AddLog(statusLog);
            }

            if (player.Hp <= 0) // 상태 이상의 효과 등으로 인해 플레이어 HP가 0이 된 경우
            {
                log = $"{player.Name}이 쓰러졌습니다.";
                return BattleResult.Defeat;
            }

            return BattleResult.None;
        }

        public bool TryDequeueLog(out string log)
        {
            return pendingLogs.TryDequeue(out log);
        }

        private void AddLog(string log)
        {
            if (!string.IsNullOrWhiteSpace(log))
                pendingLogs.Enqueue(log);
        }
    }
}