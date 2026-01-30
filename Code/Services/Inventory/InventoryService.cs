using System;
using Code.Data;
using Code.Item;
using Code.Services.Health;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.Services.StaticData;
using Code.StaticData.Inventory;

namespace Code.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IHeroHealthService _heroHealthService;
        private readonly IStaticDataService _staticData;

        private InventoryData InventoryData => _persistentProgressService.Progress.WorldData.InventoryData;

        public event Action<InventoryItem> UsedItem;

        public InventoryService(IPersistentProgressService persistentProgressService,
            ISaveLoadService saveLoadService,
            IHeroHealthService heroHealthService,
            IStaticDataService staticData)
        {
            _persistentProgressService = persistentProgressService;
            _saveLoadService = saveLoadService;
            _heroHealthService = heroHealthService;
            _staticData = staticData;
        }
        
        public InventoryItem EnsureEmptySlot(ItemType type) =>
            _persistentProgressService.Progress.WorldData.InventoryData.EnsureEmptySlot(type);

        public int GetItemCount(ItemType itemType) =>
            InventoryData.GetItemCount(itemType);

        public void Add(ItemType itemType)
        {
            InventoryItem inventoryItem = InventoryData.Add(itemType);
            
            UsedItem?.Invoke(inventoryItem);
            _saveLoadService.SaveProgress();
        }

        public void UseItem(ItemType itemType)
        {
            if (InventoryData.GetItemCount(itemType) <= 0)
                return;

            InventoryItem usedItem = InventoryData.UseItem(itemType);
            
            InventoryItemStaticData itemStaticData = _staticData.GetInventoryItemStaticData(itemType);
            
            _heroHealthService.Heal(itemStaticData.HealAmount);

            UsedItem?.Invoke(usedItem);
            _saveLoadService.SaveProgress();
        }
    }
}