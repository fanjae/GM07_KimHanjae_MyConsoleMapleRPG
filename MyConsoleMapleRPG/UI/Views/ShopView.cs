using MyConsoleMapleRPG.Character;
using MyConsoleMapleRPG.Enums;
using MyConsoleMapleRPG.Items;
using MyConsoleMapleRPG.Shops;
using MyConsoleMapleRPG.Systems;
using MyConsoleMapleRPG.UI.Rendering;
using MyConsoleMapleRPG.UI.Settings;

namespace MyConsoleMapleRPG.UI.Views
{
    // 상점 화면의 출력 전용 View 클래스
    // 구매/판매 탭, 아이템 목록 등의 출력 정보를 그림

    internal class ShopView
    {
        private readonly ShopSystem shopSystem;
        private const int ListStartY = 4;
        private const int ListVisibleCount = 4;

        public ShopView(ShopSystem shopSystem)
        {
            this.shopSystem = shopSystem;
        }

        public void Draw(int selectedIndex, int playerGold, string message, ShopMode mode, Player player)
        {
            Console.Clear();

            ConsoleRenderer.DrawWindow(Layout.MainWindowX, Layout.MainWindowY, Layout.MainWindowWidth, Layout.MainWindowHeight, "SHOP");

            DrawTabs(mode);

            if (mode == ShopMode.Buy)
            {
                DrawItems(selectedIndex);
                DrawSelectedItemInfo(selectedIndex);
            }
            else
            {
                DrawSellItems(selectedIndex, player);
                DrawSelectedSellItemInfo(selectedIndex, player);
            }

            DrawGold(playerGold);
            DrawMessage(message);
            DrawCommand(mode);
        }

        private void DrawTabs(ShopMode mode) // 탭 그려주는 메서드
        {
            Console.SetCursorPosition(8, 3);

            Console.Write(mode == ShopMode.Buy ? "▶ 구매" : "  구매");

            Console.SetCursorPosition(20, 3);
            Console.Write(mode == ShopMode.Sell ? "▶ 판매" : "  판매");
        }

        private void DrawItems(int selectedIndex) // 아이템 리스트 정보 그려주는 메서드
        {
            int windowX = 6;
            int windowY = 3;
            int windowWidth = 76;
            int windowHeight = 8;

            int startX = 8;
            int startY = 5;
            int startIndex = 0;

            ConsoleRenderer.DrawWindow(windowX, windowY, windowWidth, windowHeight, " 아이템 LIST");

            Console.SetCursorPosition(startX + 2, startY - 1);
            Console.Write("이름");

            Console.SetCursorPosition(startX + 28, startY - 1);
            Console.Write("가격");

            Console.SetCursorPosition(startX + 40, startY - 1);
            Console.Write("재고");

            if (selectedIndex >= ListVisibleCount)
                startIndex = selectedIndex - ListVisibleCount + 1;

            for (int i = 0; i < ListVisibleCount; i++) // 최대 볼 수 있는 개수 만큼만 출력한다.
            {
                int itemIndex = startIndex + i;

                Console.SetCursorPosition(startX, startY + i);
                Console.Write(new string(' ', 65));

                if (itemIndex >= shopSystem.Items.Count)
                    continue;

                ShopItem shopItem = shopSystem.Items[itemIndex];

                if (!TryGetItem(shopItem.ItemId, out ItemData? itemData) || itemData == null)
                    continue;

                string cursor = itemIndex == selectedIndex ? "▶" : " ";
                string stockText = GetStockText(shopItem);

                Console.SetCursorPosition(startX, startY + i);
                Console.Write($"{cursor} {itemData.Name.PadRight(14)}");

                Console.SetCursorPosition(startX + 28, startY + i);
                Console.Write($"{itemData.Price}G");

                Console.SetCursorPosition(startX + 40, startY + i);
                Console.Write(stockText);
            }
        }

        public void DrawSelectedItemInfo(int selectedIndex)
        {
            ConsoleRenderer.DrawWindow(6, 12, 76, 13, " ITEM INFO ");

            if (selectedIndex < 0 || selectedIndex >= shopSystem.Items.Count)
                return;

            ShopItem shopItem = shopSystem.Items[selectedIndex];
            if (!TryGetItem(shopItem.ItemId, out ItemData? itemData) || itemData == null)
                return;

            if (!string.IsNullOrEmpty(itemData.IconPath))
                AsciiImageRenderer.Draw(itemData.IconPath, 11, 14);

            Console.SetCursorPosition(24, 14);
            Console.Write(itemData.Name);

            Console.SetCursorPosition(24, 16);
            Console.Write(itemData.Description);

            Console.SetCursorPosition(24, 18);
            Console.Write($"가격 : {itemData.Price}G");

            Console.SetCursorPosition(24, 20);
            string stockText = GetDetailStockText(shopItem);
            Console.Write($"재고 : {stockText}");
        }

        private void DrawGold(int gold)
        {
             Console.SetCursorPosition(6, 27);
            Console.Write($"보유 골드 : {gold}G");
        }

        private void DrawMessage(string message)
        {
            int windowX = 52;
            int windowY = 25;
            int windowWidth = 34;
            int windowHeight = 6;

            int textX = windowX + 3;
            int textY = windowY + 2;
            int textWidth = 15;
            int textHeight = 3;

            ConsoleRenderer.DrawWindow(windowX, windowY, windowWidth, windowHeight, " LOG ");

            for (int i = 0; i < textHeight; i++)
            {
                Console.SetCursorPosition(textX, textY + i);
                Console.Write(new string(' ', textWidth));
            }

            for (int i = 0; i < textHeight; i++)
            {
                int startIndex = i * textWidth;

                if (startIndex >= message.Length) break;

                string line = message.Substring(startIndex,Math.Min(textWidth, message.Length - startIndex));

                Console.SetCursorPosition(textX, textY + i);
                Console.Write(line);
            }
        }

        private void DrawCommand(ShopMode mode)
        {
            Console.SetCursorPosition(6, 29);

            if (mode == ShopMode.Buy)
                Console.Write("←→ 구매/판매   ↑↓ 선택   Z 구매   ESC 닫기");
            else
                Console.Write("←→ 구매/판매   ↑↓ 선택   Z 판매   ESC 닫기");
        }

        private void DrawSellItems(int selectedIndex, Player player)
        {
            int windowX = 6;
            int windowY = 3;
            int windowWidth = 76;
            int windowHeight = 8;

            int startX = 8;
            int startY = 5;

            ConsoleRenderer.DrawWindow(windowX, windowY, windowWidth, windowHeight, " 판매 ITEM LIST ");

            Console.SetCursorPosition(startX + 2, startY - 1);
            Console.Write("이름");

            Console.SetCursorPosition(startX + 28, startY - 1);
            Console.Write("판매가");

            Console.SetCursorPosition(startX + 40, startY - 1);
            Console.Write("수량");

            int startIndex = 0;

            if (selectedIndex >= ListVisibleCount)
                startIndex = selectedIndex - ListVisibleCount + 1;

            for (int i = 0; i < ListVisibleCount; i++) // 리스트에 보이는 만큼만 출력
            {
                int itemIndex = startIndex + i;

                Console.SetCursorPosition(startX, startY + i);
                Console.Write(new string(' ', 65));

                if (itemIndex >= player.Inventory.Slots.Count)
                    continue;

                var slot = player.Inventory.Slots[itemIndex];
                if (!TryGetItem(slot.ItemId, out ItemData? itemData) || itemData == null)
                    continue;

                string cursor = itemIndex == selectedIndex ? "▶" : " ";
                string countText = itemData.IsStackable ? slot.Count.ToString() : "1"; // 여러 개 소지할 수 있는 경우 구분
                string equippedText = slot.IsEquipped ? " [E]" : ""; // 장착 여부 출력 

                Console.SetCursorPosition(startX, startY + i);
                Console.Write($"{cursor} {(itemData.Name + equippedText).PadRight(14)}");

                Console.SetCursorPosition(startX + 28, startY + i);
                Console.Write($"{GetSellPrice(itemData)}G"); // 아이템 판매

                Console.SetCursorPosition(startX + 40, startY + i);
                Console.Write(countText);
            }
        }

        private void DrawSelectedSellItemInfo(int selectedIndex, Player player)
        {
            ConsoleRenderer.DrawWindow(6, 12, 76, 13, " ITEM INFO ");

            if (player.Inventory.Slots.Count == 0)
                return;

            if (selectedIndex < 0 || selectedIndex >= player.Inventory.Slots.Count)
                return;

            var slot = player.Inventory.Slots[selectedIndex];
            if (!TryGetItem(slot.ItemId, out ItemData? itemData) || itemData == null)
                return;

            if (!string.IsNullOrEmpty(itemData.IconPath))
                AsciiImageRenderer.Draw(itemData.IconPath, 11, 14);

            Console.SetCursorPosition(24, 14);
            Console.Write(itemData.Name);

            Console.SetCursorPosition(24, 16);
            Console.Write(itemData.Description);

            Console.SetCursorPosition(24, 18);
            Console.Write($"판매가 : {GetSellPrice(itemData)}G");

            Console.SetCursorPosition(24, 20);
            Console.Write($"보유 수량 : {slot.Count}");
        }
        private bool TryGetItem(int itemId, out ItemData? itemData)
        {
            return ItemDatabase.TryGet(itemId, out itemData) && itemData != null;
        }
        private int GetSellPrice(ItemData itemData)
        {
            return itemData.Price / 2;
        }

        private string GetStockText(ShopItem shopItem)
        {
            return shopItem.Stock == -1 ? "-" : shopItem.Stock.ToString();
        }

        private string GetDetailStockText(ShopItem shopItem)
        {
            return shopItem.Stock == -1 ? "제한 없음" : $"{shopItem.Stock}개";
        }
    }
}
