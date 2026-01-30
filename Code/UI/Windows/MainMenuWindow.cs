using Code.Data;
using Code.Infrastructure.States;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.UI.Services.Windows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Windows
{
    public class MainMenuWindow : WindowBase
    {
        private const string MainScene = "Main";
        
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _authorButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _loadGameButton;

        private GameStateMachine _stateMachine;
        private IWindowService _windowService;
        private ISaveLoadService _saveLoadService;
        private IPersistentProgressService _persistentProgress;

        [Inject]
        public void Construct(
            IPersistentProgressService persistentProgressService,
            GameStateMachine gameStateMachine,
            IWindowService windowService,
            ISaveLoadService saveLoadService
        )
        {
            base.Construct(persistentProgressService);
            _persistentProgress = persistentProgressService;
            _stateMachine = gameStateMachine;
            _windowService = windowService;
            _saveLoadService = saveLoadService;
        }

        protected override void Initialize()
        {
            _startGameButton.onClick.AddListener(StartNewGame);
            _settingsButton.onClick.AddListener(OpenSettings);
            _authorButton.onClick.AddListener(OpenAuthorWindow);
            _exitButton.onClick.AddListener(ExitGame);
            _loadGameButton.onClick.AddListener(LoadSavedGame);

            _loadGameButton.interactable = _saveLoadService.HasProgress();
        }

        private void StartNewGame()
        {
            // _stateMachine.Enter<LoadLevelState, string>(Payload);
            
            InitNewProgress();
            
            _stateMachine.Enter<LoadLevelState, string>(Progress.WorldData.PositionOnLevel.Level);

            OnCloseButtonClicked();
        }

        private void LoadSavedGame()
        {
            PlayerProgress loadedProgress = _saveLoadService.LoadProgress();

            if (loadedProgress == null)
            {
                _loadGameButton.interactable = false;
                return;
            }

            ProgressService.Progress = loadedProgress;

            _stateMachine.Enter<LoadLevelState, string>(Progress.WorldData.PositionOnLevel.Level);

            OnCloseButtonClicked();
        }

        private void InitNewProgress()
        {
            ProgressService.Progress = GetNewProgress();
        }

        private PlayerProgress GetNewProgress()
        {
            PlayerProgress playerProgress = new PlayerProgress(initialLevel: MainScene, _persistentProgress);
    
            playerProgress.HeroHealth.IsInitialized = false;
            playerProgress.HeroStats.IsInitialized = false;

            return playerProgress;
        }

        private void OpenSettings() => 
            _windowService.Open(WindowType.Settings);

        private void OpenAuthorWindow() => 
            _windowService.Open(WindowType.Author);

        private void ExitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
