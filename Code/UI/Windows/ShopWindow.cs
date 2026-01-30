using System.Collections.Generic;
using Code.Data;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.Services.Shop;
using Code.Services.StaticData;
using Code.Services.TimeService;
using Code.Services.Wallet;
using Code.StaticData.Shop;
using Code.UI.Elements;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.UI.Windows
{
  public class ShopWindow : WindowBase
  {
    [SerializeField] private TextMeshProUGUI _lootValue;
    [SerializeField] private List<ShopItemView> _shopItemView;

    private IStaticDataService _staticData;
    private IShopService _shopService;
    private IPersistentProgressService _progressService;
    private ISaveLoadService _saveLoadService;
    private ITimeService _timeService;
    private bool _isGamePaused;

    [Inject]
    public void Construct(IStaticDataService staticData,
      IShopService shopService,
      IWalletService walletService,
      IPersistentProgressService progressService,
      ISaveLoadService saveLoadService,
      ITimeService timeService)
    {
      _saveLoadService = saveLoadService;
      _progressService = progressService;
      _staticData = staticData;
      _shopService = shopService;
      _timeService = timeService;
    }

    protected override void Initialize()
    {
      PauseGame();

      RefreshLootValue();
      List<ShopItem> savedShopItems = _progressService.Progress.WorldData.ShopItemsData.ShopItems;

      if (!SavedItemsExist(savedShopItems))
        InitializeShopItemsFromStaticData(savedShopItems);
      else
        InitializeShopItemsFromSavedProgress(savedShopItems);
    }

    private static bool SavedItemsExist(List<ShopItem> savedShopItems) => 
      savedShopItems.Count > 0;

    private void InitializeShopItemsFromStaticData(List<ShopItem> savedShopItems)
    {
      foreach (ShopItemView shopItemView in _shopItemView)
      {
        ShopItemStaticData data = _staticData.GetShopItemStaticData(shopItemView.ItemType);
        shopItemView.Initialize(data.Price,
          data.Name,
          data.Count,
          shopItemView.ItemType,
          data.Image,
          onBuy: () => TryBuyOnClickItem(shopItemView));

        _shopService.RefreshShopItemView(shopItemView);

        savedShopItems.Add(new ShopItem(data.Price, data.Name, data.Count, data.ItemType, data.Image));
        _saveLoadService.SaveProgress();
      }
    }

    private void InitializeShopItemsFromSavedProgress(List<ShopItem> savedShopItems)
    {
      foreach (ShopItemView shopItemView in _shopItemView)
      foreach (ShopItem savedShopItem in savedShopItems)
      {
        if (shopItemView.ItemType == savedShopItem.ItemType)
        {
          shopItemView.Initialize(savedShopItem.Price,
            savedShopItem.Name,
            savedShopItem.Count,
            savedShopItem.ItemType,
            savedShopItem.Image,
            onBuy: () => TryBuyOnClickItem(shopItemView));

          _shopService.RefreshShopItemView(shopItemView);
        }
      }
    }

    public void TryBuyOnClickItem(ShopItemView shopItemView)
    {
      _shopService.TryBuy(shopItemView);
    }

    protected override void SubscribeUpdates() =>
      Progress.WorldData.LootData.Changed += RefreshLootValue;

    protected override void Cleanup()
    {
      base.Cleanup();
      ResumeGame();
      Progress.WorldData.LootData.Changed -= RefreshLootValue;
    }

    private void RefreshLootValue() =>
      _lootValue.text = Progress.WorldData.LootData.LootValue.ToString();

    private void PauseGame()
    {
      if (_isGamePaused)
        return;

      _timeService.Pause();
      _isGamePaused = true;
    }

    private void ResumeGame()
    {
      if (!_isGamePaused)
        return;

      _timeService.UnPause();
      _isGamePaused = false;
    }
  }
}
