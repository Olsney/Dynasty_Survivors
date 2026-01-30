using System;
using System.Collections.Generic;

namespace Code.Data
{
  [Serializable]
  public class ShopItemsData
  {
    public List<ShopItem> ShopItems;

    public ShopItemsData()
    {
      ShopItems = new List<ShopItem>();
    }
  }
}