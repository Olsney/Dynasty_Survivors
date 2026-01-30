using System;
using Code.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Elements
{
    public class ShopItemView : MonoBehaviour
    {
        [SerializeField] private Button _buyButton;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private ItemType _itemType;
        [SerializeField] private Image _image;

        private Action _onBuy;
        private int _count;
        private int _price;

        public ItemType ItemType => _itemType;
        public int Count => _count;
        public int Price => _price;
        public Image Image => _image;
        public string Name => _nameText.text;
        
        public void Initialize(int price,
            string itemName,
            int count,
            ItemType itemType,
            Sprite sprite,
            Action onBuy)
        {
            _onBuy = onBuy;
            _price = price;
            _count = count;
            _itemType = itemType;

            UpdateUI(price, itemName, count, itemType, sprite);

            HandleSubscribes();
        }

        private void OnDestroy()
        {
            _buyButton.onClick.RemoveAllListeners();
        }

        public void RemoveItem()
        {
            if (_count <= 0)
            {
                Refresh(isAvailable: _count > 0);
                
                return;
            }
            
            _count--;
            Refresh(isAvailable: _count > 0);
        }

        public void Refresh(bool isAvailable)
        {
            _buyButton.interactable = isAvailable;
            
            _countText.text = _count.ToString();
        }

        private void UpdateUI(int price, string itemName, int count, ItemType itemType, Sprite sprite)
        {
            _priceText.text = price.ToString();
            _nameText.text = itemName;
            _countText.text = count.ToString();
            _itemType = itemType;
            _image.sprite = sprite;

            _buyButton.interactable = _count > 0;
        }

        private void HandleSubscribes()
        {
            _buyButton.onClick.RemoveAllListeners();
            _buyButton.onClick.AddListener(() => _onBuy?.Invoke());
        }
    }
}