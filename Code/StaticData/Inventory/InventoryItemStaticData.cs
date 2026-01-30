using Code.Item;
using UnityEngine;

namespace Code.StaticData.Inventory
{
    [CreateAssetMenu(fileName = "ItemStaticData", menuName = "StaticData/InventoryItemsStaticData")]
    public class InventoryItemStaticData : ScriptableObject
    {
        public Sprite Image;
        public ItemType ItemType;
        public float HealAmount;
    }
}