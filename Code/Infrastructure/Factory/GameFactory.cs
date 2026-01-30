using System.Collections.Generic;
using Code.Enemy;
using Code.Hero;
using Code.Hero.Abilities;
using Code.Hero.Abilities.Factory;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Services.Identifiers;
using Code.Inventory;
using Code.Logic;
using Code.Logic.EnemySpawners;
using Code.Services.Ability;
using Code.Services.AbilityProgress;
using Code.Services.Hero;
using Code.Services.PersistentProgress;
using Code.Services.Random;
using Code.Services.SceneProvider;
using Code.Services.StaticData;
using Code.StaticData.Enemy;
using Code.StaticData.Hero;
using Code.UI.Elements;
using Code.UI.Elements.AbilityPanel;
using Code.UI.Services.Windows;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Code.Infrastructure.Factory
{
    public class GameFactory : IGameFactory, IInitializable
    {
        private readonly IAssetProvider _assets;
        private readonly IInstantiator _container;
        private readonly IStaticDataService _staticData;
        private readonly IRandomService _randomService;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IIdentifierService _identifierService;
        private readonly IWindowService _windowService;
        private readonly ISceneProvider _sceneProvider;
        private readonly IAbilityFactory _abilityFactory;
        private readonly IAbilityProvider _abilityProvider;

        private GameObject _heroGameObject;
        private IHeroProvider _heroProvider;

        public List<ISavedProgress> ProgressWriters { get; } = new();

        public List<ISavedProgressReader> ProgressReaders { get; } = new();
        
        public GameFactory(
            IAssetProvider assets, 
            IInstantiator container, 
            IStaticDataService staticData, 
            IRandomService randomService,
            IPersistentProgressService persistentProgressService, 
            IIdentifierService identifierService, 
            IWindowService windowService,
            ISceneProvider sceneProvider,
            IAbilityFactory abilityFactory, 
            IAbilityProvider abilityProvider,
            IHeroProvider heroProvider)
        {
            _assets = assets;
            _container = container;
            _staticData = staticData;
            _randomService = randomService;
            _persistentProgressService = persistentProgressService;
            _identifierService = identifierService;
            _windowService = windowService;
            _sceneProvider = sceneProvider;
            _abilityFactory = abilityFactory;
            _abilityProvider = abilityProvider;
            _heroProvider = heroProvider;
        }

        public void Initialize()
        {
        }

        public GameObject CreateSaveTriggerContainer() => 
            InstantiateRegistered(AssetPath.SaveTriggerContainerPath);

        public GameObject CreateHero(HeroTypeId heroTypeId, Vector3 at)
        {
            HeroStaticData heroData = _staticData.GetHero(heroTypeId);
            
            _heroGameObject = InstantiateRegistered(heroData.Prefab, at);

            IHealth health = _heroGameObject.GetComponent<IHealth>();
            health.Initialize(
                heroData.CurrentHealth, 
                heroData.MaxHealth
                );

            HeroMove heroMove = _heroGameObject.GetComponent<HeroMove>();
            heroMove.Initialize(heroData.MovementSpeed);

            HeroAttack heroAttack = _heroGameObject.GetComponent<HeroAttack>();
            heroAttack.Initialize(
                heroData.Damage,
                heroData.DamageRadius,
                heroData.AttackCooldown
                );
            
            // List<HeroAbility> abilities = new List<HeroAbility>();
            
            // abilities.Add(_abilityFactory.CreateAbility(AbilityType.FireAttackAbility, _heroGameObject, level: 1));
            // abilities.Add(_abilityFactory.CreateAbility(AbilityType.OrbitingAbility, _heroGameObject));
            // abilities.Add(_abilityFactory.CreateAbility(AbilityType.BoomerangAbility, _heroGameObject));
            // abilities.Add(_abilityFactory.CreateAbility(AbilityType.RockAttackAbility, _heroGameObject));
            // abilities.Add(_abilityFactory.CreateAbility(AbilityType.FireShardsAbility, _heroGameObject));

            // _abilityProvider.Initialize(abilities);
            _heroProvider.Initialize(_heroGameObject);
            
            return _heroGameObject;
        }

        public GameObject CreateHud()
        {
            GameObject hud = InstantiateRegistered(AssetPath.HudPath);

            hud.GetComponentInChildren<LootCounterView>()
                .Construct(_persistentProgressService.Progress.WorldData);

            hud.GetComponentInChildren<ExpirienceProgressBar>()
                .Construct(_persistentProgressService.Progress.Experience);

            foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
                openWindowButton.Construct(_windowService);
            
            hud.GetComponentInChildren<AbilityPanelView>()
                .Initialize();
            
            hud.GetComponentInChildren<InventoryInteraction>()
                .Initialize();
            
            return hud;
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public GameObject CreateCurtain()
        {
            GameObject prefab = _assets.Load(AssetPath.LoadCurtainPath);
            
            return _container.InstantiatePrefab(prefab);
        }

        public GameObject CreateEnemy(EnemyTypeId enemyTypeId, Transform container)
        {
            EnemyStaticData enemyData = _staticData.GetEnemy(enemyTypeId);

            GameObject enemy = _container.InstantiatePrefab(enemyData.Prefab, container.position, Quaternion.identity, container);

            IHealth health = enemy.GetComponent<IHealth>();
            health.Initialize(enemyData.Health, enemyData.Health);
            
            enemy.GetComponent<ActorUI>().Construct(health);
            enemy.GetComponent<EnemyMoveToHero>().Construct(_heroGameObject.transform, _randomService);
            enemy.GetComponent<NavMeshAgent>().speed = enemyData.MoveSpeed;

            LootSpawner lootSpawner = enemy.GetComponentInChildren<LootSpawner>();
            lootSpawner.SetLoot(enemyData.MinLootValue, enemyData.MaxLootValue);
            lootSpawner.Construct(this, _randomService);

            switch (enemyTypeId)
            {
                case EnemyTypeId.Skeleton:
                    CreateSkeleton(enemy, enemyData);
                    break;
                case EnemyTypeId.Giant:
                    CreateGiant(enemy, enemyData);
                    break;
                case EnemyTypeId.DarkKnight:
                    CreateDarkKnight(enemy, enemyData);
                    break;
            }
            
            enemy.GetComponent<EnemyRotateToHero>()?.Construct(_heroGameObject.transform);

            ExperienceReward enemyReward = enemy.GetComponent<ExperienceReward>();
            enemyReward.Construct(_persistentProgressService);
            
            return enemy;
        }

        private void CreateGiant(GameObject enemy, EnemyStaticData enemyData)
        {
            EnemyMeleeAttack enemyMeleeAttack = enemy.GetComponent<EnemyMeleeAttack>();
            enemyMeleeAttack.Construct(_heroGameObject.transform);
            enemyMeleeAttack.Initialize(
                enemyData.Damage, 
                enemyData.AttackCooldown, 
                enemyData.AttackOffsetY, 
                enemyData.AttackOffsetForward, 
                enemyData.AttackCleavage
                );
        }

        private void CreateDarkKnight(GameObject enemy, EnemyStaticData enemyData)
        {
            EnemyMeleeAttack enemyMeleeAttack = enemy.GetComponent<EnemyMeleeAttack>();
            enemyMeleeAttack.Construct(_heroGameObject.transform);
            enemyMeleeAttack.Initialize(
                enemyData.Damage, 
                enemyData.AttackCooldown, 
                enemyData.AttackOffsetY, 
                enemyData.AttackOffsetForward, 
                enemyData.AttackCleavage
            );
        }

        private static void CreateSkeleton(GameObject enemy, EnemyStaticData enemyData)
        {
            EnemyAreaPassiveAttack enemyAreaPassiveAttack = enemy.GetComponent<EnemyAreaPassiveAttack>();
            enemyAreaPassiveAttack.Initialize(
                enemyData.Damage, 
                enemyData.AttackCooldown
                );
        }

        public LootPiece CreateLoot()
        {
            LootPiece lootPiece = InstantiateRegistered(AssetPath.LootPath)
                .GetComponent<LootPiece>();
            lootPiece.Construct(_persistentProgressService.Progress.WorldData);
            
            return lootPiece;
        }

        public void CreateEnemySpawner(Vector3 at, string spawnerId, EnemyTypeId enemyTypeId, float minSpawnInterval, float maxSpawnInterval)
        {
            EnemySpawner enemySpawner = InstantiateRegistered(AssetPath.EnemySpawnerPath, at)
                .GetComponent<EnemySpawner>();

            enemySpawner.Initialize(enemyTypeId, minSpawnInterval, maxSpawnInterval);
        }

        private GameObject InstantiateRegistered(GameObject prefab, Vector3 at)
        {
            GameObject instance = _container.InstantiatePrefab(prefab, at, Quaternion.identity, _sceneProvider.Container);
      
            RegisterProgressWatchers(instance);
            return instance;
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject prefab = _assets.Load(path: prefabPath);
            GameObject instance = _container.InstantiatePrefab(prefab, at, Quaternion.identity, _sceneProvider.Container);
            RegisterProgressWatchers(instance);

            return instance;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject prefab = _assets.Load(path: prefabPath);
            GameObject instance = _container.InstantiatePrefab(prefab, _sceneProvider.Container);
            RegisterProgressWatchers(instance);

            return instance;
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);
            
            ProgressReaders.Add(progressReader);
        }

        private void RegisterProgressWatchers(GameObject hero)
        {
            foreach (ISavedProgressReader progressReader in hero.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }
    }
}
