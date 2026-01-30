using Code.Logic;
using UnityEngine;

namespace Code.Item
{
    public class HealItem : MonoBehaviour
    {
        private ItemType _itemType;
        private int _healValue;
        
        public ItemType ItemType => _itemType;
        public int HealValue => _healValue;

        public void Initialize(ItemType itemType, int healValue)
        {
            _itemType = itemType;
            _healValue = healValue;
        }

        public void Heal(IHealth targetHealth)
        {
            targetHealth.Heal(_healValue);
        }
    }
}