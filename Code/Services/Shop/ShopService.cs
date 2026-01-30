using System.Collections.Generic;
using System.Linq;
using Code.Data;
using Code.Item;
using Code.Services.Inventory;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.Services.StaticData;
using Code.Services.Wallet;
using Code.UI.Elements;

namespace Code.Services.Shop
{
  public class ShopService : IShopService
  {
    private readonly IStaticDataService _staticDataService;
    private readonly IInventoryService _inventoryService;
    private readonly IWalletService _walletService;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;
    private readonly Dictionary<ItemType, int> _stock;

    public ShopService(IStaticDataService staticDataService,
      IInventoryService inventoryService,
      IWalletService walletService,
      IPersistentProgressService progressService,
      ISaveLoadService saveLoadService)
    {
      _staticDataService = staticDataService;
      _inventoryService = inventoryService;
      _walletService = walletService;
      _progressService = progressService;
      _saveLoadService = saveLoadService;
    }

    public bool TryBuy(ShopItemView shopItemView)
    {
      if (!CanBuy(shopItemView))
        return false;

      if (_walletService.TrySpend(shopItemView.Price) == false)
        return false;

      _inventoryService.Add(shopItemView.ItemType);
      shopItemView.RemoveItem();

      SaveShopItemChanges(shopItemView);

      RefreshShopItemViewButton(shopItemView);

      return true;
    }

    private void SaveShopItemChanges(ShopItemView shopItemView)
    {
      ShopItem item = _progressService.Progress.WorldData.ShopItemsData.ShopItems
        .FirstOrDefault(shopItem => shopItem.ItemType == shopItemView.ItemType);
      item.Count = shopItemView.Count;

      _saveLoadService.SaveProgress();
    }

    public void RefreshShopItemView(ShopItemView shopItemView)
    {
      shopItemView.Refresh(isAvailable: CanBuy(shopItemView));
    }

    private bool CanBuy(ShopItemView shopItemView) =>
      shopItemView.Count > 0 && _walletService.CanAfford(shopItemView.Price);

    private void RefreshShopItemViewButton(ShopItemView shopItemView)
    {
      bool isAvailible = shopItemView.Count > 0 && _walletService.CanAfford(shopItemView.Price);

      shopItemView.Refresh(isAvailible);
    }
  }
}