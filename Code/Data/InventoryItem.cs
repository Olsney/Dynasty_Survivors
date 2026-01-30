using System;
using Code.Item;

namespace Code.Data
{
    [Serializable]
    public class InventoryItem
    {
        public ItemType ItemType;
        public int Count;
        
        public InventoryItem(ItemType itemType, int count)
        {
            ItemType = itemType;
            Count = count;
        }
    }
}