using System;
using Code.Data;
using Code.Item;

namespace Code.Services.Inventory
{
    public interface IInventoryService
    {
        event Action<InventoryItem> UsedItem;
        InventoryItem EnsureEmptySlot(ItemType type);
        int GetItemCount(ItemType itemType);
        void Add(ItemType itemType);
        void UseItem(ItemType itemType);
        
    }
}