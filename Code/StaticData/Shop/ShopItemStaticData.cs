using Code.Item;
using UnityEngine;
using UnityEngine.UI;

namespace Code.StaticData.Shop
{
    [CreateAssetMenu(fileName = "ShopStaticData", menuName = "StaticData/ShopStaticData")]
    public class ShopItemStaticData : ScriptableObject
    {
        public Sprite Image; 
        public ItemType ItemType;

        public string Name;
        public int Price;
        public int Count;
    }
}