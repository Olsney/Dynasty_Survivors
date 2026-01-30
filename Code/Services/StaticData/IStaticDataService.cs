using Code.Hero.Abilities;
using Code.Infrastructure.Services;
using Code.Item;
using Code.StaticData.Enemy;
using Code.StaticData.Hero;
using Code.StaticData.Inventory;
using Code.StaticData.Item;
using Code.StaticData.Level;
using Code.StaticData.Shop;
using Code.StaticData.Windows;
using Code.UI.Services.Windows;

namespace Code.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadAllEnemies();
        EnemyStaticData GetEnemy(EnemyTypeId typeId);
        void LoadAllHeroes();
        HeroStaticData GetHero(HeroTypeId typeId);
        void LoadAllLevels();
        LevelStaticData GetLevel(string sceneKey);
        WindowConfig GetWindow(WindowType shop);
        void LoadAllWindows();
        void LoadAllAbilitiesConfigs();
        void InitializeAllHeroAbilitiesSetups();
        AbilityConfig GetAbilityConfig(AbilityType type);
        AbilityLevel GetHeroAbilitySetup(AbilityType type);
        AbilityLevel GetAbilityLevel(AbilityType abilityType, int level);
        void LoadAllShopItemsConfigs();
        ShopItemStaticData GetShopItemStaticData(ItemType type);
        void LoadAllInventoryItemsConfigs();
        InventoryItemStaticData GetInventoryItemStaticData(ItemType type);
    }
}