using MyConsoleMapleRPG.Character;
using MyConsoleMapleRPG.Enums;
using MyConsoleMapleRPG.Items;
using MyConsoleMapleRPG.Items.Inventory;
using MyConsoleMapleRPG.UI.Rendering;
using MyConsoleMapleRPG.UI.Settings;

namespace MyConsoleMapleRPG.UI.Views
{
    // 인벤토리 화면의 출력 전용 View 클래스
    // 아이템 목록, 선택 아이템 정보, 보유 골드, 조작 안내 등을 그림
    // 아이템 사용/장착 처리는 InventoryScreen, ItemUseService 담당.
    internal class InventoryView
    {
        private readonly Inventory inventory;
        private readonly Player player;
        public InventoryView(Player player)
        {
            this.player = player;
            inventory = player.Inventory;
        }

        public void Draw(int selectedIndex, int gold, string message)
        {
            Console.Clear();

            ConsoleRenderer.DrawWindow(
                Layout.MainWindowX,
                Layout.MainWindowY,
                Layout.MainWindowWidth,
                Layout.MainWindowHeight,
                "INVENTORY"
            );

            DrawItems(selectedIndex);
            DrawGold(gold);
            DrawMessage(message);
            DrawCommand(selectedIndex);

        }
        private void DrawMessage(string message)
        {
            Console.SetCursorPosition(6, 25);
            Console.Write(message.PadRight(25));
        }

        private void DrawItems(int selectedIndex)
        {
            if (inventory.Slots.Count == 0) // 인벤토리가 비어있는 경우
            {
                Console.SetCursorPosition(6, 5);
                Console.Write("인벤토리가 비어 있습니다.");
                return;
            }

            for (int i = 0; i < inventory.Slots.Count; i++)
            {
                InventorySlot slot = inventory.Slots[i];
                if (!TryGetItem(slot.ItemId, out ItemData? itemData) || itemData == null)
                    continue;

                Console.SetCursorPosition(6, 5 + i);

                string cursor = i == selectedIndex ? "▶" : " ";
                string countText = itemData.IsStackable ? $" x{slot.Count}" : "";
                string equippedText = slot.IsEquipped ? " [E]" : "";

                Console.Write($"{cursor} {itemData.Name}{countText}{equippedText}");
            }

            DrawSelectedItemDescription(selectedIndex);
        }

        private void DrawSelectedItemDescription(int selectedIndex)
        {
            if (selectedIndex < 0 || selectedIndex >= inventory.Slots.Count)
                return;

            InventorySlot slot = inventory.Slots[selectedIndex];
            if (!TryGetItem(slot.ItemId, out ItemData? itemData) || itemData == null)
                return;

            ConsoleRenderer.DrawWindow(50, 5, 36, 12, "ITEM INFO");

            if (!string.IsNullOrEmpty(itemData.IconPath))
            {
                AsciiImageRenderer.Draw(itemData.IconPath, 63, 18);
            }


            Console.SetCursorPosition(55, 7);
            Console.Write(itemData.Name);

            Console.SetCursorPosition(55, 10);
            Console.Write(itemData.Description);

            Console.SetCursorPosition(55,13);
            Console.Write($"가격 : {itemData.Price}");
        }
        private void DrawGold(int gold)
        {
            Console.SetCursorPosition(6, 27);
            Console.Write($"보유 골드 : {gold}G");
        }

        private void DrawCommand(int selectedIndex)
        {
            Console.SetCursorPosition(6, 29);

            string actionText = "Z 사용";

            if (selectedIndex >= 0 && selectedIndex < inventory.Slots.Count)
            {
                InventorySlot slot = inventory.Slots[selectedIndex];
                if (TryGetItem(slot.ItemId, out ItemData? itemData) && itemData != null)
                {
                    if (itemData.Type == ItemType.Equipment) 
                        actionText = slot.IsEquipped ? "Z 해제" : "Z 착용";
                }
            }
            Console.Write($"↑↓ 선택   {actionText}   ESC 닫기");
        }
        private bool TryGetItem(int itemId, out ItemData? itemData)
        {
            return ItemDatabase.TryGet(itemId, out itemData) && itemData != null;
        }
    }
}