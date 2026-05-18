using MyConsoleMapleRPG.Character;
using MyConsoleMapleRPG.Enums;
using MyConsoleMapleRPG.Shops;
using MyConsoleMapleRPG.Systems;
using MyConsoleMapleRPG.UI.Audio;
using MyConsoleMapleRPG.UI.Views;

namespace MyConsoleMapleRPG.UI.Screens
{
    // 상점 화면의 입력 루프와 구매/판매 모드 상태 관리
    // 구매/판매의 동작은 ShopSystem에 위임. 출력은 ShopView에 위임.

    internal class ShopScreen
    {
        private readonly Player player;
        private readonly ShopSystem shopSystem;
        private readonly ShopView view;
        private ShopMode mode = ShopMode.Buy;

        private int selectedIndex = 0;
        private string message = "";

        private int CurrentItemCount // 현재 아이템 개수 (구매일땐 상점의 아이템이 판매일때는 내 인벤의 아이템이 보인다.)
        {
            get
            {
                return mode == ShopMode.Buy
                    ? shopSystem.Items.Count
                    : player.Inventory.Slots.Count;
            }
        }

        public ShopScreen(Player player) // 상점에서 보이는 아이템 목록
        {
            this.player = player;

            shopSystem = new ShopSystem(new List<ShopItem>
            {
                new ShopItem(1001, -1),
                new ShopItem(1002, -1),
                new ShopItem(1003, -1),
                new ShopItem(2001, -1),
                new ShopItem(2002, -1),
                new ShopItem(2101, -1),

            });

            view = new ShopView(shopSystem);
        }

        public void Show()
        {
            while(true)
            {
                view.Draw(selectedIndex, player.Gold, message, mode, player); // 상점 그리기

                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        {
                            MoveUp();
                            break;
                        }

                    case ConsoleKey.DownArrow:
                        {
                            MoveDown();
                            break;
                        }

                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.RightArrow:
                        ToggleMode();
                        break;

                    case ConsoleKey.Z:
                        if (mode == ShopMode.Buy)
                        {
                            BuySelectedItem();
                            AudioManager.PlaySfx("Assets/sound/select.wav");
                        }
                        else
                        {
                            SellSelectedItem();
                            AudioManager.PlaySfx("Assets/sound/select.wav");
                        }
                        break;

                    case ConsoleKey.Escape:
                        return;
                    
                }
            }
        }

        private void MoveUp() // 윗키 이동
        {
            int count = CurrentItemCount;

            if (count == 0)
                return;

            selectedIndex--;

            if (selectedIndex < 0)
                selectedIndex = count - 1;
        }

        private void MoveDown() // 아랫키 이동
        {
            int count = CurrentItemCount;

            if (count == 0)
                return;

            selectedIndex++;

            if (selectedIndex >= count)
                selectedIndex = 0;
        }

        private void BuySelectedItem() // 구매 아이템 선택 
        {
            shopSystem.Buy(selectedIndex, player, out message);
        }

        private void SellSelectedItem() // 판매 아이템 선택
        {
            shopSystem.Sell(player, selectedIndex, out message);

            if (selectedIndex >= player.Inventory.Slots.Count)
                selectedIndex = Math.Max(0, player.Inventory.Slots.Count - 1);
        }
        private void ToggleMode() // 구매 / 판매 토글
        {
            mode = mode == ShopMode.Buy
                ? ShopMode.Sell
                : ShopMode.Buy;

            selectedIndex = 0;
            message = "";
        }

    }
}
