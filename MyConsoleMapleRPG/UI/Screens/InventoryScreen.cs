using MyConsoleMapleRPG.Character;
using MyConsoleMapleRPG.Items;
using MyConsoleMapleRPG.Items.Inventory;
using MyConsoleMapleRPG.Systems;
using MyConsoleMapleRPG.UI.Audio;
using MyConsoleMapleRPG.UI.Views;

namespace MyConsoleMapleRPG.UI.Screens
{
    // 인벤토리 화면의 입력 루프와 선택 상태를 관리
    // 아이템 사용/장착 처리는 ItemUseService가 담당
    // View는 화면 출력, Screen은 사용자 입력에 따른 흐름 제어를 담당 

    internal class InventoryScreen
    {
        private readonly Player player;
        private readonly InventoryView view;
        private readonly ItemUseService itemUseService;

        private int selectedIndex = 0;
        private string message = "";

        public InventoryScreen(Player player)
        {
            this.player = player;
            view = new InventoryView(player);
            itemUseService = new ItemUseService();
        }

        public void Show() // 인벤토리 정보 보여줌
        {
            while (true)
            {
                view.Draw(selectedIndex, player.Gold, message);

                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        MoveUp();
                        break;

                    case ConsoleKey.DownArrow:
                        MoveDown();
                        break;

                    case ConsoleKey.Z:
                        AudioManager.PlaySfx("Assets/sound/select.wav");
                        UseSelectedItem();
                        break;

                    case ConsoleKey.Escape:
                        return;
                }
            }
        }

        private void MoveUp() // 위로 이동
        {
            if (player.Inventory.Slots.Count == 0)
                return;

            selectedIndex--;

            if (selectedIndex < 0)
                selectedIndex = player.Inventory.Slots.Count - 1;
        }

        private void MoveDown() // 아래로 이동
        {
            if (player.Inventory.Slots.Count == 0)
                return;

            selectedIndex++;

            if (selectedIndex >= player.Inventory.Slots.Count)
                selectedIndex = 0;
        }

        private void UseSelectedItem() // 사용 버튼 누름
        {
            if (player.Inventory.Slots.Count == 0) // 해당 슬롯에 아무것도 없는 경우
                return;

            itemUseService.UseItem(player.Inventory, selectedIndex, player, out message); // 아이템 사용

            if (selectedIndex >= player.Inventory.Slots.Count) 
                selectedIndex = Math.Max(0, player.Inventory.Slots.Count - 1);
        }
    }
}