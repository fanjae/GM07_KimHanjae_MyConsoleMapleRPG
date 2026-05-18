using System.Text.Json;
using MyConsoleMapleRPG.Character;
using MyConsoleMapleRPG.Enums;
using MyConsoleMapleRPG.Save;
using MyConsoleMapleRPG.Items;
using MyConsoleMapleRPG.Items.Inventory;

namespace MyConsoleMapleRPG.Systems
{
    internal class SaveService
    {
        // 게임 데이터 및 세이브 및 로드를 전담하는 서비스 클래스입니다.
        // 플레이어 스탯 및 인벤토리 상태를 JSON 형식의 로컬 파일로 저장하고 복원합니다.

        private const string SaveFilePath = "MySaveFile.json";

        private readonly JsonSerializerOptions options = new()
        {
            WriteIndented = true
        };

        public void Save(Player player) // 저장
        {
            SaveData saveData = CreateSaveData(player);

            string json = JsonSerializer.Serialize(saveData, options);
            File.WriteAllText(SaveFilePath, json); // json 형태로 파일 쓰고 닫음
        }


        // 인게임 플레이어 객체로부터 파일 저장에 필요한 데이터를 뽑아와서 저장한다.
        private SaveData CreateSaveData(Player player) // 저장 데이터 생성
        {
            return new SaveData
            {
                PlayerType = player.JobType.ToString(),

                Level = player.Level,
                Exp = player.Exp,

                Hp = player.Hp,
                MaxHp = player.MaxHp,
                Mp = player.Mp,
                MaxMp = player.MaxMp,

                BaseAttack = player.BaseAttack,
                BaseDefense = player.BaseDefense,

                Gold = player.Gold,
                InventoryItems = CreateInventorySaveData(player)
            };
        }

        // JSON 파일을 읽어서 다시 역직렬화
        public SaveData? Load()
        {
            // 파일 없으면 null 리턴
            if (!File.Exists(SaveFilePath))
                return null;

            try
            {
                // 파일 전체 읽어옴
                string json = File.ReadAllText(SaveFilePath); // string 형태로 파일 읽고 닫음
                return JsonSerializer.Deserialize<SaveData>(json);
            }
            catch (IOException) // 파일 입출력 자체 실패
            {
                return null;
            }
            catch (UnauthorizedAccessException) // 접근 권한 문제 
            {
                return null;
            }
            catch (JsonException) // 파싱 실패 
            {
                return null;
            }
        }

        public Player? LoadPlayer()
        {
            // 원본 파일 데이터 가져옴.
            SaveData? saveData = Load();

            if (saveData == null)
                return null;

            // 저장된 문자열을 Enum 유형으로 처리
            if (!Enum.TryParse(saveData.PlayerType, ignoreCase: true, out JobType jobType))
                return null;

            // 해당 직업군에 맞는 인스턴스 생성
            Player player = PlayerFactory.Create(jobType);

            // 생성된 플레이어 기본 능력치 데이터 복구(레벨 보정)
            player.RestoreState(saveData.Level,saveData.Exp,saveData.Hp,saveData.MaxHp,saveData.Mp,saveData.MaxMp,saveData.BaseAttack,saveData.BaseDefense,saveData.Gold);

            // 인벤토리 복구
            RestoreInventory(player, saveData.InventoryItems);

            return player;
        }

        // 플레이어의 인벤토리 슬롯 정보를 리스트 형태로 정제 처리
        private List<InventorySaveData> CreateInventorySaveData(Player player)
        {
            List<InventorySaveData> inventoryItems = new();

            foreach (InventorySlot slot in player.Inventory.Slots)
            {
                inventoryItems.Add(new InventorySaveData
                {
                    ItemId = slot.ItemId,
                    Quantity = slot.Count,
                    IsEquipped = slot.IsEquipped
                });
            }

            return inventoryItems;
        }

        // 불러온 세이브 데이터 목록을 바탕으로 인벤토리 슬롯 생성 및 장착 상태 재구성
        private void RestoreInventory(Player player, List<InventorySaveData> inventoryItems)
        {
            foreach (InventorySaveData inventoryItem in inventoryItems)
            {
                // 데이터 검사
                if (!ItemDatabase.TryGet(inventoryItem.ItemId, out ItemData? itemData))
                    continue;

                
                if (itemData == null)
                    continue;

                // 저장 데이터 구조 그대로 강제 복원
                // 슬롯은 기본적으로 미장착 상태로 생성
                InventorySlot slot = player.Inventory.AddSlotForLoad(inventoryItem.ItemId,inventoryItem.Quantity,false);


                // 장착된 슬롯이있었으면 착용처리
                if (inventoryItem.IsEquipped)
                {
                    player.Equipment.ToggleEquip(slot, itemData, player.JobType, out _);
                }
            }
        }
    }
}