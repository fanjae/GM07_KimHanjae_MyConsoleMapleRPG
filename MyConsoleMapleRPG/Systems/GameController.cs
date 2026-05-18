using MyConsoleMapleRPG.Character;
using MyConsoleMapleRPG.Enums;
using MyConsoleMapleRPG.Items;
using MyConsoleMapleRPG.UI.Audio;
using MyConsoleMapleRPG.UI.Screens;
using MyConsoleMapleRPG.UI.Settings;

namespace MyConsoleMapleRPG.Systems
{
    // 게임 전체 흐름을 제어하는 최상위 컨트롤러
    // 메인 메뉴 표시, 캐릭터 선택, 실제 게임 화면 진입 연결
    internal class GameController
    {
        private readonly MainMenu mainMenu = new MainMenu();
        private readonly SaveService saveService = new SaveService();

        private string mainMenuMessage = "";
        public void Run()
        {
            AudioManager.PlayBgm("Assets/sound/main.wav");
            ConsoleSetting.ApplyMain();
            while (true)
            {
                MainMenuResult result = mainMenu.Show(mainMenuMessage); // 메뉴 창 선택
                mainMenuMessage = "";

                switch (result)
                {
                    case MainMenuResult.StartGame:
                        StartGame();
                        break;

                    case MainMenuResult.LoadGame:
                        LoadGame();
                        break;

                    case MainMenuResult.Credits:
                        ShowCredits();
                        break;

                     case MainMenuResult.Exit:
                        return;
                }
            }
        }
        private void StartGame()
        {
            ConsoleSetting.ApplyCharacterSelect();

            CharacterSelectScreen characterSelectScreen = new CharacterSelectScreen();
            int selectedJob = characterSelectScreen.Show();

            if (selectedJob == -1)
            {
                ConsoleSetting.Apply(95, 35);
                return;
            }

            Player player = PlayerFactory.Create(selectedJob);

            ConsoleSetting.ApplyMain();

            #if DEBUG
            AddTestItems(player);
#endif


            AudioManager.PlayBgm("Assets/sound/ingame.wav");
            GameScreen gameScreen = new GameScreen(player);
            gameScreen.Show();

            gameScreen.Dispose();

        }
        private void LoadGame()
        {
            Player? loadedPlayer = saveService.LoadPlayer();

            if (loadedPlayer == null)
            {
                mainMenuMessage = "저장 파일이 없습니다.";
                return;
            }

            ConsoleSetting.ApplyMain();

            GameScreen gameScreen = new GameScreen(loadedPlayer);
            gameScreen.Show();

            gameScreen.Dispose();
        }

        private void AddTestItems(Player player)
        {
            player.AddGold(5000);
            player.Inventory.AddItem(ItemDatabase.Get(2001), 2);
            player.Inventory.AddItem(ItemDatabase.Get(2002), 2);
            player.Inventory.AddItem(ItemDatabase.Get(2101), 2);
            player.Inventory.AddItem(ItemDatabase.Get(1001), 5);
        }

        private void ShowCredits()
        {
            Console.Clear();

            Console.SetCursorPosition(35, 10);
            Console.Write("MADE BY FanJae");

            Console.SetCursorPosition(32, 13);
            Console.Write("My Console Maple RPG");

            Console.SetCursorPosition(28, 16);
            Console.Write("아무 키나 누르면 돌아갑니다.");

            Console.ReadKey(true);

            ConsoleSetting.ApplyMain();
        }
    }
}
