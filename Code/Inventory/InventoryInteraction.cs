using System.Collections.Generic;
using Code.UI.Elements;
using UnityEngine;

namespace Code.Inventory
{
    public class InventoryInteraction : MonoBehaviour
    {
        [SerializeField] private List<InventorySlotView> _inventorySlotViews;
        
        public void Initialize()
        {
            foreach (InventorySlotView slotView in _inventorySlotViews) 
                slotView.Initialize();
        }
    }
}