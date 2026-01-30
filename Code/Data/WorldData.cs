using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Code.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
        
        [FormerlySerializedAs("VisitedTriggerIDs")] [FormerlySerializedAs("TriggerIDs")] 
        public List<int> VisitedTriggerIds;
        
        public LootData LootData;
        public InventoryData InventoryData;
        public ShopItemsData ShopItemsData;

        public KillData KillData;
        public PlayTimeData PlayTimeData;

        public WorldData(string initialLevel)
        {
            PositionOnLevel = new PositionOnLevel(initialLevel);
            VisitedTriggerIds = new List<int>();
            LootData = new LootData();
            InventoryData = new InventoryData();
            ShopItemsData = new ShopItemsData();
            KillData = new KillData();
            PlayTimeData = new PlayTimeData();
        }
    }
}
