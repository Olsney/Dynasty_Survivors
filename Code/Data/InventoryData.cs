using System;
using System.Collections.Generic;
using Code.Item;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public class InventoryData
    {
        [SerializeField] private List<InventoryItem> _items = new();
        public IReadOnlyList<InventoryItem> Items => _items;
        
        public int GetItemCount(ItemType itemType)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].ItemType == itemType)
                    return _items[i].Count;
            }
                
            return 0;
        }
        
        public InventoryItem Add(ItemType itemType)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].ItemType == itemType)
                {
                    _items[i].Count++;
                    
                    return _items[i];
                }
            }

            InventoryItem newItem = new InventoryItem(itemType, 1);
            
            _items.Add(newItem);
            return newItem;
        }
        
        public InventoryItem EnsureEmptySlot(ItemType itemType)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].ItemType == itemType)
                    return _items[i];
            }

            InventoryItem emptySlot = new InventoryItem(itemType, 0);
            _items.Add(emptySlot);
            
            return emptySlot;
        }

        public InventoryItem UseItem(ItemType itemType)
        {
            InventoryItem item = FindItem(itemType);
            
            if (GetItemCount(itemType) <= 0)
                return item;
            
            item.Count--;

            return item;
        }

        private InventoryItem FindItem(ItemType itemType)
        {
            InventoryItem item = _items.Find(x => x.ItemType == itemType);

            return item;
        }
    }
}