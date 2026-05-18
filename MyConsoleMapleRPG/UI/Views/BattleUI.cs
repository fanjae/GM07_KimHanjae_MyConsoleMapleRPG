using MyConsoleMapleRPG.Character;
using MyConsoleMapleRPG.Character.Monster;
using MyConsoleMapleRPG.UI.Rendering;

namespace MyConsoleMapleRPG.UI.Views
{
    // 전투 화면의 출력 전용 UI 클래스.
    // 기본 레이아웃, 캐릭터 이미지, HP/MP 상태바, 명령어, 전투 로그를 표현
    internal class BattleUI
    {
        private const int WindowX = 2;
        private const int WindowY = 1;
        private const int WindowWidth = 90;
        private const int WindowHeight = 32;

        private const int PlayerImageX = 20;
        private const int PlayerImageY = 3;

        private const int MonsterImageX = 62;
        private const int MonsterImageY = 3;

        private const int ImageWidth = 16;
        private const int ImageHeight = 16;

        private const int PlayerStatusX = 8;
        private const int PlayerHpY = 17;
        private const int PlayerMpY = 18;

        private const int MonsterStatusX = 50;
        private const int MonsterHpY = 17;


        private readonly Player player;
        private readonly Monster monster;

        public BattleUI(Player player, Monster monster) // 배틀 UI
        {
            this.player = player;
            this.monster = monster;
        }

        public void DrawBaseScreen()
        {
            Console.Clear();

            DrawBase();
            DrawCharacters();
            DrawMenu();
        }

        public void DrawDynamic(string battleLog)
        {
            DrawStatus();
            DrawLog(battleLog);
        }

        private void DrawBase() // 기본 틀(화면에서 보이는 플레이어, 커맨드, 로그창 그림)
        {
            ConsoleRenderer.DrawWindow(WindowX, WindowY, WindowWidth, WindowHeight, $"BATTLE - {monster.Name}");

            Console.SetCursorPosition(6, 3);
            Console.Write("PLAYER");

            Console.SetCursorPosition(68, 3);
            Console.Write("MONSTER");

            ConsoleRenderer.DrawWindow(5, 22, 84, 4, "COMMAND");
            ConsoleRenderer.DrawWindow(5, 27, 84, 4, "LOG");
        }

        private void DrawCharacters() // 캐릭 정보 그림
        {
            AsciiImageRenderer.Draw(player.ImagePath, PlayerImageX, PlayerImageY, ImageWidth, ImageHeight);
            AsciiImageRenderer.Draw(monster.ImagePath, MonsterImageX, MonsterImageY, ImageWidth, ImageHeight);

            Console.Write("\x1b[0m");
            Console.ResetColor();
        }

        private void DrawStatus() // 상태 정보 그림
        {
            ConsoleRenderer.DrawBar(PlayerStatusX, PlayerHpY, "PLAYER HP", player.Hp, player.MaxHp, 10, ConsoleColor.Red);
            ConsoleRenderer.DrawBar(PlayerStatusX, PlayerMpY, "PLAYER MP", player.Mp, player.MaxMp, 10, ConsoleColor.Blue);

            ConsoleRenderer.DrawBar(MonsterStatusX, MonsterHpY, "ENEMY  HP", monster.Hp, monster.MaxHp, 10, ConsoleColor.Red);

            Console.SetCursorPosition(8, 20);
            Console.Write($"ATK : {player.Attack}  DEF : {player.Defense}");

            Console.SetCursorPosition(55, 20);
            Console.Write($"ATK : {monster.Attack}  DEF : {monster.Defense}");

            DrawStatusEffects(); // 상태 이상 관련 처리(정상, 중독 여부 등의 현재 상태이상 정보 확인)
        }

        private void DrawMenu() // 메뉴 정보 그림
        {
            Console.SetCursorPosition(10, 24);
            Console.Write("[Z] 공격");

            Console.SetCursorPosition(25, 24);
            Console.Write("[X] 스킬");

            Console.SetCursorPosition(40, 24);
            Console.Write("[C] 도망");
        }

        private void DrawLog(string battleLog) // 로그 정보
        {
            int x = 10;
            int y = 29;
            int width = 75;

            Console.SetCursorPosition(x, y);
            Console.Write(new string(' ', width));

            Console.SetCursorPosition(x, y);
            Console.Write(battleLog);
        }

        public void DrawSkillMenu(int selectedIndex) // 스킬 메뉴 관련 정보
        {
            ConsoleRenderer.DrawWindow(5, 22, 84, 4, "SKILL");

            for (int i = 0; i < player.Skills.Count; i++)
            {
                Console.SetCursorPosition(10 + i * 25, 24);

                string cursor = i == selectedIndex ? "▶ " : "  ";
                Skill skill = player.Skills[i];

                Console.Write($"{cursor}{skill.Name} MP:{skill.MpCost}");
            }
        }
        public void DrawCommandMenu() // 커맨드 메뉴 관련 정보
        {
            ConsoleRenderer.DrawWindow(5, 22, 84, 4, "COMMAND");
            DrawMenu();
        }

        private void DrawStatusEffects() // 상태이상 정보 관련 그림
        {
            Console.SetCursorPosition(8, 21);
            Console.Write(new string(' ', 30));

            Console.SetCursorPosition(8, 21);

            if (player.StatusEffects.Count == 0)
            {
                Console.Write("상태 : 정상");
                return;
            }

            string statusText = string.Join(", ", player.StatusEffects.Select(effect => effect.Name));
            Console.Write($"상태 : {statusText}");
        }
    }
}