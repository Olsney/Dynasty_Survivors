using System;
using System.Collections.Generic;
using System.Linq;
using Code.Hero.Abilities;
using Code.StaticData.Enemy;
using Code.StaticData.Hero;
using Code.StaticData.Level;
using Code.StaticData.Windows;
using Code.UI.Services.Windows;
using UnityEngine;

namespace Code.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string EnemiesStaticDataPath = "StaticData/Enemies";
        private const string HeroesStaticDataPath = "StaticData/Heroes";
        private const string LevelsStaticDataPath = "StaticData/Levels";
        private const string WindowsStaticDataPath = "StaticData/UI/WindowData";
        private const string AbilitiesConfigsPath = "StaticData/AbilitiesConfigs";
        
        private Dictionary<HeroTypeId, HeroStaticData> _heroes;
        private Dictionary<EnemyTypeId, EnemyStaticData> _enemies;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<WindowId, WindowConfig> _windowConfigs;
        private Dictionary<AbilityType, AbilityLevel> _abilitiesSetups;
        private Dictionary<AbilityType,AbilityConfig> _abilitiesConfigs;

        public void LoadAllEnemies()
        {
            _enemies = Resources
                .LoadAll<EnemyStaticData>(EnemiesStaticDataPath)
                .ToDictionary(x => x.EnemyType, x => x);
        }
        
        public void LoadAllHeroes()
        {
            _heroes = Resources
                .LoadAll<HeroStaticData>(HeroesStaticDataPath)
                .ToDictionary(x => x.HeroType, x => x);
        }

        public void LoadAllLevels()
        {
            _levels = Resources
                .LoadAll<LevelStaticData>(LevelsStaticDataPath)
                .ToDictionary(x => x.LevelKey, x => x);
        }
        
        public void LoadAllWindows()
        {
            _windowConfigs = Resources
                .Load<WindowStaticData>(WindowsStaticDataPath)
                .Configs
                .ToDictionary(x => x.WindowId, x => x);
        }

        public void LoadAllAbilitiesConfigs()
        {
            _abilitiesConfigs = Resources.LoadAll<AbilityConfig>(AbilitiesConfigsPath)
                .ToDictionary(x => x.AbilityType, x => x);
        }

        public void InitializeAllHeroAbilitiesSetups()
        {
            _abilitiesSetups = GetHero(HeroTypeId.Woman).AbilitySetups.
                ToDictionary(x => x.AbilityType, x => x);
        }
        
        public AbilityLevel GetHeroAbilitySetup(AbilityType type)
        {
            return _abilitiesSetups.TryGetValue(type, out AbilityLevel abilitySetup) 
                ? abilitySetup : null;
        }

        public AbilityLevel GetAbilityLevel(AbilityType abilityType, int level)
        {
            AbilityConfig config = GetAbilityConfig(abilityType);
            
            if (level > config.AbilitySetups.Count)
                level = config.AbilitySetups.Count;

            return config.AbilitySetups[level - 1];
        }

        public AbilityConfig GetAbilityConfig(AbilityType abilityId)
        {
            if (_abilitiesConfigs.TryGetValue(abilityId, out AbilityConfig config))
                return config;

            throw new Exception($"Ability config for {abilityId} was not found");
        }
        
        public EnemyStaticData GetEnemy(EnemyTypeId typeId) => 
            _enemies.TryGetValue(typeId, out EnemyStaticData staticData) 
                ? staticData 
                : throw new KeyNotFoundException($"Enemy with type: {typeId} is not found");
        
        public HeroStaticData GetHero(HeroTypeId typeId) => 
            _heroes.TryGetValue(typeId, out HeroStaticData staticData) 
                ? staticData 
                : throw new KeyNotFoundException($"Hero with type: {typeId} is not found");

        public LevelStaticData GetLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData staticData) 
                ? staticData 
                : throw new KeyNotFoundException($"Level with scene: {sceneKey} is not found");

        public WindowConfig GetWindow(WindowId windowId) =>
            _windowConfigs.TryGetValue(windowId, out WindowConfig config) 
                ? config 
                : throw new KeyNotFoundException($"Level with scene: {windowId} is not found");
    }
}