using Code.CameraLogic;
using Code.Infrastructure.Factory;
using Code.Logic;
using Code.Logic.Curtain;
using Code.Services.AbilityProgress;
using Code.Services.PersistentProgress;
using Code.Services.PlayTime;
using Code.Services.StaticData;
using Code.StaticData.Hero;
using Code.StaticData.Level;
using Code.UI.Elements;
using Code.UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private const string EmptySceneName = "Empty";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtainProvider _loadingCurtainProvider;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IStaticDataService _staticDataService;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        // private readonly IHeroLevelUpHandler _heroLevelUpHandler;
        private readonly DiContainer _container;
        private readonly HeroAbilityProgressService _heroAbilityProgressService;
        private readonly CurrentSessionPlayTimeService _currentSessionPlayTimeService;

        public LoadLevelState(
            GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            IGameFactory gameFactory,
            LoadingCurtainProvider loadingCurtainProvider,
            IPersistentProgressService persistentProgressService,
            IStaticDataService staticDataService, 
            IUIFactory uiFactory,
            DiContainer container,
            HeroAbilityProgressService heroAbilityProgressService,
            CurrentSessionPlayTimeService currentSessionPlayTimeService
            )
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _loadingCurtainProvider = loadingCurtainProvider;
            _persistentProgressService = persistentProgressService;
            _staticDataService = staticDataService;
            _uiFactory = uiFactory;
            _container = container;
            _heroAbilityProgressService = heroAbilityProgressService;
            _currentSessionPlayTimeService = currentSessionPlayTimeService;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtainProvider.Instance.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(EmptySceneName, () =>  _sceneLoader.Load(sceneName, OnLoaded));
        }

        private void LoadRequestedScene(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtainProvider.Instance.Hide();
        }

        private void OnLoaded()
        {
            InitUIRoot();
            InitGameWorld();

            _stateMachine.Enter<GameLoopState>();
        }

        private void RegisterNonMonoBehaviourProgressReaders()
        {
            _gameFactory.Register(_heroAbilityProgressService);
            _gameFactory.Register(_currentSessionPlayTimeService);
        }

        private void InitGameWorld()
        {
            InitSpawners();
            
            _gameFactory.CreateSaveTriggerContainer();
            
            GameObject hero = _gameFactory.CreateHero(HeroTypeId.Woman, at: GetHeroSpawnPosition());
            
            RegisterNonMonoBehaviourProgressReaders();
            InformProgressReaders();
            
            InitHud(hero);
            CameraFollow(hero);
        }

        private void InitUIRoot() => 
            _uiFactory.CreateUIRoot();

        private Vector3 GetHeroSpawnPosition() => 
            GameObject.FindWithTag(InitialPointTag).transform.position;

        private void InitSpawners()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticDataService.GetLevel(sceneKey);
            
            foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
            {
                _gameFactory.CreateEnemySpawner(
                    spawnerData.Position, 
                    spawnerData.Id, 
                    spawnerData.EnemyTypeId, 
                    levelData.MinSpawnInterval, 
                    levelData.MaxSpawnInterval);
            }
        }

        private void InitHud(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHud();
            
            hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<IHealth>());
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_persistentProgressService.Progress);
            }
        }

        private void CameraFollow(GameObject hero) => 
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
    }
}