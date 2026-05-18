using MyConsoleMapleRPG.Character;
using MyConsoleMapleRPG.Character.Monster;
using MyConsoleMapleRPG.Enums;
using MyConsoleMapleRPG.Systems;
using MyConsoleMapleRPG.UI.Views;

namespace MyConsoleMapleRPG.UI.Screens
{

    // 전투 화면의 입력 루프와 전투 결과 흐름 담당.
    // 전투 계산은 BattleSystem, 출력은 BattleUI에 위임
    // 키 입력에 따라 공격/스킬/도망을 실행한 뒤 결과에 따라 전투를 종료한다.

    internal class BattleScreen
    {
        private readonly BattleSystem battleSystem;
        private readonly BattleUI battleUI;
        private readonly Player player;

        private string battleLog;

        public BattleScreen(Player player, Monster monster) // 배틀 시작
        {
            battleSystem = new BattleSystem(player, monster);
            battleUI = new BattleUI(player, monster);
            this.player = player;

            battleLog = $"{monster.Name}가/(이) 나타났습니다!";
        }

        public BattleResult Show()
        {
            battleUI.DrawBaseScreen(); // 배틀 UI 기본 틀
            battleUI.DrawDynamic(battleLog); // 배틀 도중에 그리는 부분

            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                BattleResult result = BattleResult.None;

                switch (key)
                {
                    case ConsoleKey.Z: // 기본 공격
                        result = battleSystem.PlayerAttack(out battleLog);
                        break;

                    case ConsoleKey.X: // 스킬 공격
                        int? skillIndex = SelectSkill();

                        if (skillIndex == null)
                        {
                            battleLog = "스킬 선택을 취소했습니다.";
                            result = BattleResult.InvalidAction;
                            break;
                        }

                        result = battleSystem.PlayerSkill(skillIndex.Value, out battleLog);
                        break;

                    case ConsoleKey.C: // 도망
                        result = battleSystem.Escape(out battleLog);
                        break;

                    case ConsoleKey.Escape: // 테스트용 코드
                        return BattleResult.Escape;

                    default:
                        continue;
                }

                battleUI.DrawDynamic(battleLog); 
                ShowPendingBattleLogs(); // 배틀에서 출력해야할 로그 정보 찍음

                if (result == BattleResult.InvalidAction) // 유효하지 않은 동작
                    continue;

                if (result == BattleResult.None) // 계속 진행
                    continue;

                if (result == BattleResult.Victory) // 배틀 승리
                {
                    ShowRewardLogs(); // 보상 로그 출력
                    return BattleResult.Victory;
                }

                Console.ReadKey(true);
                return result;
            }
        }

        private int? SelectSkill() // 스킬 선택
        {
            int selectedIndex = 0;

            while (true)
            {
                battleUI.DrawSkillMenu(selectedIndex);

                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex--;
                        if (selectedIndex < 0)
                            selectedIndex = player.Skills.Count - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        selectedIndex++;
                        if (selectedIndex >= player.Skills.Count) selectedIndex = 0;
                        break;

                    case ConsoleKey.Z:
                    case ConsoleKey.Enter:
                        battleUI.DrawCommandMenu();
                        battleUI.DrawDynamic(battleLog);
                        return selectedIndex;

                    case ConsoleKey.X:
                    case ConsoleKey.Escape:
                        battleUI.DrawBaseScreen();
                        battleUI.DrawDynamic(battleLog);
                        return null;
                }
            }
        }

        private void ShowRewardLogs()
        {
            foreach (string rewardLog in battleSystem.RewardLogs)
            {
                Console.ReadKey(true);

                battleLog = rewardLog;
                battleUI.DrawDynamic(battleLog);
            }

            Console.ReadKey(true);
        }

        private void ShowPendingBattleLogs()
        {
            while (battleSystem.TryDequeueLog(out string log))
            {
                Console.ReadKey(true);

                battleLog = log;
                battleUI.DrawDynamic(battleLog);
            }
        }
    }
}