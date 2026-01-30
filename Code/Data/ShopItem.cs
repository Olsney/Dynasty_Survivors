using System;
using Code.Item;
using UnityEngine;

namespace Code.Data
{
  [Serializable]
  public class ShopItem
  {
    public int Price;
    public string Name;
    public int Count;
    public ItemType ItemType;
    public Sprite Image;
    
    public ShopItem(int price, string name, int count, ItemType itemType, Sprite image)
    {
      Price = price;
      Name = name;
      Count = count;
      ItemType = itemType;
      Image = image;
    }
  }
}