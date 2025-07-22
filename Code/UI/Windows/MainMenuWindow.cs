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

        private GameStateMachine _stateMachine;
        private IWindowService _windowService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        public void Construct(
            IPersistentProgressService persistentProgressService,
            GameStateMachine gameStateMachine,
            IWindowService windowService,
            ISaveLoadService saveLoadService
        )
        {
            base.Construct(persistentProgressService);
            _stateMachine = gameStateMachine;
            _windowService = windowService;
            _saveLoadService = saveLoadService;
        }

        protected override void Initialize()
        {
            _startGameButton.onClick.AddListener(StartGame);
            _settingsButton.onClick.AddListener(OpenSettings);
            _authorButton.onClick.AddListener(OpenAuthorWindow);
            _exitButton.onClick.AddListener(ExitGame);
        }

        private void StartGame()
        {
            // _stateMachine.Enter<LoadLevelState, string>(Payload);
            
            LoadProgressOrInitNew();
            
            _stateMachine.Enter<LoadLevelState, string>(Progress.WorldData.PositionOnLevel.Level);

            OnCloseButtonClicked();
        }
        
        private void LoadProgressOrInitNew()
        {
            ProgressService.Progress =
                _saveLoadService.LoadProgress()
                ?? NewProgress();
        }

        private PlayerProgress NewProgress()
        {
            PlayerProgress playerProgress = new PlayerProgress(initialLevel: MainScene);
    
            playerProgress.HeroHealth.IsInitialized = false;
            playerProgress.HeroStats.IsInitialized = false;

            return playerProgress;
        }

        private void OpenSettings() => 
            _windowService.Open(WindowId.Settings);

        private void OpenAuthorWindow() => 
            _windowService.Open(WindowId.Author);

        private void ExitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}