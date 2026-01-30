using Code.UI.Elements;

namespace Code.Services.Shop
{
    public interface IShopService
    {
        bool TryBuy(ShopItemView shopItemView);
        void RefreshShopItemView(ShopItemView shopItemView);
    }
}