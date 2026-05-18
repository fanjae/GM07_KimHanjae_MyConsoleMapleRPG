using MyConsoleMapleRPG.Items;
namespace MyConsoleMapleRPG.Items.Inventory
{
    // 플레이어가 가진 아이템 슬롯 목록을 관리하는 클래스
    // ItemId와 Count만 저장, 실제 데이터는 ItemDatabase에서 조회
    internal class Inventory
    {
        private readonly List<InventorySlot> slots = new();

        public IReadOnlyList<InventorySlot> Slots => slots;

        // 아이템 추가
        public bool AddItem(ItemData itemData, int count = 1) 
        {
            if (itemData == null)
                return false;

            if (count <= 0)
                return false;

            if (itemData.IsStackable) // 쌓이는 아이템 추가
            {
                AddStackableItem(itemData, count);
                return true;
            }

            AddNonStackableItem(itemData.Id, count); // 쌓이지 않는 아이템 추가
            return true;
        }

        public bool TryGetSlot(int slotIndex, out InventorySlot? slot) // 슬롯 정보 획득
        {
            if (slotIndex < 0 || slotIndex >= slots.Count)
            {
                slot = null;
                return false;
            }

            slot = slots[slotIndex];
            return true;
        }

        public bool RemoveItemAt(int slotIndex, int count = 1) // 특정 위치 아이템 제거
        {
            if (count <= 0)
                return false;

            if (!TryGetSlot(slotIndex, out InventorySlot? slot))
                return false;

            if (slot.Count < count) // 제거해야할 개수보다 슬롯 아이템 개수가 적음
                return false;

            slot.RemoveCount(count); // 개수 깎기

            if (slot.Count <= 0) // 슬롯에 아이템이 없으면 해당 슬롯 삭제
                slots.RemoveAt(slotIndex);

            return true;
        }
        public bool RemoveItem(int itemId, int count)
        {
            // 0개 이하일때 차감 불가
            if (count <= 0)
                return false;

            int totalCount = slots.Where(slot => slot.ItemId == itemId).Sum(slot => slot.Count); // Linq
            // 해당 아이템의 보유량 계산

            if (totalCount < count) return false;

            int remaining = count; // 남은 아이템 개수(목표 차감량)

            // 기본적으로 뒷쪽부터 순차제거 하는 방식으로 구현
            for (int i = slots.Count - 1; i >= 0 && remaining > 0; i--)
            {
                InventorySlot slot = slots[i];

                if (slot.ItemId != itemId)
                    continue;

                // 현재 슬롯에서 차감할 수량
                int removeCount = Math.Min(slot.Count, remaining);
                slot.RemoveCount(removeCount);
                remaining -= removeCount; 

                if (slot.Count <= 0)
                    slots.RemoveAt(i);
            }

            return true;
        }


        private void AddStackableItem(ItemData itemData, int count) // 쌓이는 아이템 처리
        {
            int remainingCount = count;

            foreach (InventorySlot slot in slots)
            {
                if (slot.ItemId != itemData.Id) // 아이템이 다르면 다음 슬롯으로
                    continue;

                int availableSpace = itemData.MaxStackCount - slot.Count; // 할당 공간 계산

                if (availableSpace <= 0)
                    continue;

                int addCount = Math.Min(availableSpace, remainingCount); // 추가 가능한 개수 만큼 추가
                slot.AddCount(addCount);
                remainingCount -= addCount;

                if (remainingCount <= 0)
                    return;
            }

            while (remainingCount > 0) // 추가 가능할때까지 계속 넣ㄱ
            {
                int addCount = Math.Min(itemData.MaxStackCount, remainingCount);
                slots.Add(new InventorySlot(itemData.Id, addCount));
                remainingCount -= addCount;
            }
        }

        private void AddNonStackableItem(int itemId, int count) // 쌓기 안되는 아이템 추가
        {
            for (int i = 0; i < count; i++)
            {
                slots.Add(new InventorySlot(itemId, 1));
            }
        }

        public InventorySlot AddSlotForLoad(int itemId, int count, bool isEquipped) // 세이브 데이터 정보를 불러올때 인벤토리 복원
        {
            InventorySlot slot = new InventorySlot(itemId, count); // 인벤토리 슬롯에 아이템 재할당

            slot.SetEquipped(isEquipped); // 장비 착용 처리

            slots.Add(slot); 

            return slot;
        }
    }
}