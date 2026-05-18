using MyConsoleMapleRPG.Character;
using MyConsoleMapleRPG.Character.Monster;
using MyConsoleMapleRPG.Enums;
using MyConsoleMapleRPG.Map;
using MyConsoleMapleRPG.Systems;
using MyConsoleMapleRPG.UI.Audio;
using MyConsoleMapleRPG.UI.Views;

namespace MyConsoleMapleRPG.UI.Screens
{
    // 실제 게임 플레이 화면의 최상위 Screen 클래스.
    // MapScreen과 GameUI를 연결, 인벤토리/전투/상점/사우나 화면 전환 담당
    // MapScrren에서 발생한 이벤트를 받아 필요한 화면을 엶
    internal class GameScreen : IDisposable
    {
        private readonly MapScreen mapScreen;
        private readonly SaveService saveService = new SaveService();
        private readonly GameUI gameUI;
        private readonly Player player;

        private bool preserveLogOnce = false;

        public GameScreen(Player player)
        {
            this.player = player; 

            mapScreen = new MapScreen(Maps.Town, player);
            gameUI = new GameUI(player);

            mapScreen.MapChanged += DrawAll;
            mapScreen.MonsterEncountered += StartBattle;

            mapScreen.ShopEntered += OpenShop;
            mapScreen.SaunaEntered += OpenSauna;
        }

        public void Show()
        {
            DrawAll();

            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Escape)
                {
                    return;
                }

                if (key == ConsoleKey.S)
                {
                    saveService.Save(player);

                    gameUI.SetLog("게임을 저장했습니다.");
                    gameUI.Draw();
                    AudioManager.PlaySfx("Assets/sound/save.wav");
                    continue;
                }

                if (key == ConsoleKey.I)
                {
                    OpenInventory();
                    DrawAll();
                    continue;
                }

                string resultLog = mapScreen.HandleInput(key);

                if (!string.IsNullOrWhiteSpace(resultLog))
                {
                    gameUI.SetLog(resultLog);
                    gameUI.Draw();
                }
                else
                {
                    if (preserveLogOnce)
                    {
                        preserveLogOnce = false;
                        continue;
                    }

                    gameUI.ClearLog();
                    gameUI.Draw();
                }
            }
        }

        private void DrawAll()
        {
            Console.Clear();

            gameUI.DrawMapLayout(mapScreen.MapName);
            mapScreen.Draw();
            gameUI.Draw();
        }

        private void StartBattle(Monster monster) // 배틀 시작한 경우 처리
        {
            BattleScreen battleScreen = new BattleScreen(player, monster);

            BattleResult result = battleScreen.Show(); // 배틀 처리 시작

            if (result == BattleResult.Defeat)
            {
                HandlePlayerDefeat();
                return;
            }

            DrawAll();
        }

        private void OpenInventory() // 인벤토리를 연 경우 인벤토리 스크린 처리
        {
            InventoryScreen inventoryScreen = new InventoryScreen(player);
            inventoryScreen.Show();
        }

        private void OpenShop() // 상점에 들어간 경우 상점 처리
        {
            ShopScreen shopScreen = new ShopScreen(player);
            shopScreen.Show();

            DrawAll();
        }

        private void OpenSauna() // 사우나에 들어간 경우 사우나 입장처리
        {
            SaunaScreen saunaScreen = new SaunaScreen(player);
            saunaScreen.Show();

            DrawAll();
        }
        private void HandlePlayerDefeat() // 플레이어 패배시 마을 이동
        {
            player.LoseExpByPercent(20);
            player.FullRecover();

            gameUI.SetLog("쓰러졌습니다. 마을에서 시작합니다. EXP -20%");

            preserveLogOnce = true;

            mapScreen.MoveToMap(Maps.Town, 35, 9);
        }

        public void Dispose() // 이벤트 정리
        {
            mapScreen.MapChanged -= DrawAll;
            mapScreen.MonsterEncountered -= StartBattle;
            mapScreen.ShopEntered -= OpenShop;
            mapScreen.SaunaEntered -= OpenSauna;
        }
    }

}
