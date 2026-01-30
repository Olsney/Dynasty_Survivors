using System;
using System.Collections.Generic;
using Code.Data;
using Code.Item;
using Code.Services.Inventory;
using Code.Services.PersistentProgress;
using Code.Services.StaticData;
using Code.StaticData.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Elements
{
    public class InventorySlotView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _countText;
        [FormerlySerializedAs("_button")] [SerializeField] private Button _buyButton;
        [SerializeField] private ItemType _itemType;
        [SerializeField] private Image _itemImage;

        private InventoryItem _item;
        private IInventoryService _inventoryService;
        private IStaticDataService _staticData;

        public ItemType ItemType => _itemType;
        
        [Inject]
        private void Construct(IInventoryService inventoryService,
            IStaticDataService staticDataService)
        {
            _inventoryService = inventoryService;
            _staticData = staticDataService;
        }
        
        public void Initialize()
        {
            InventoryItemStaticData data = _staticData.GetInventoryItemStaticData(_itemType);
            _itemImage.sprite = data.Image;
            
            _item = _inventoryService.EnsureEmptySlot(_itemType);
            
            Refresh();
            
            _buyButton.onClick.AddListener(() => TryUseItem());
            
            _inventoryService.UsedItem += OnUsedItem;
        }

        private void OnDestroy()
        {
            _buyButton.onClick.RemoveAllListeners();
            _inventoryService.UsedItem -= OnUsedItem;
        }

        private void OnUsedItem(InventoryItem item)
        {
            if (item.ItemType == _itemType)
            {
                _item = item;
                Refresh();
            }
        }

        private void TryUseItem()
        {
            _inventoryService.UseItem(_item.ItemType);
        }

        private void Refresh()
        {
            if (_item == null)
            {
                int count = default;
                
                _countText.text = count.ToString();
                _buyButton.interactable = false;
                
                return;
            }
            
            _countText.text = _item.Count.ToString();
            _buyButton.interactable = _item.Count > 0;
        }
    }
}