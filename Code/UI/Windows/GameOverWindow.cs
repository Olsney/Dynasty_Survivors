using System;
using Code.Data;
using Code.Infrastructure.States;
using Code.Services.Kill;
using Code.Services.PersistentProgress;
using Code.Services.PlayTime;
using Code.Services.SaveLoad;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Windows
{
    public class GameOverWindow : WindowBase
    {
        private const string MainScene = "Main";

        [SerializeField] private Button _loadGameButton;
        [SerializeField] private TextMeshProUGUI _text;
        
        
        private GameStateMachine _stateMachine;
        private ISaveLoadService _saveLoadService;
        private IPersistentProgressService _persistentProgress;
        private IKillsHandlerService _killsHandlerService;
        private ICurrentSessionPlayTimeService _currentSessionPlayTimeService;

        [Inject]
        public void Construct(IPersistentProgressService progressService, 
            GameStateMachine stateMachine,
            ISaveLoadService saveLoadService,
            IKillsHandlerService killsHandlerService,
            ICurrentSessionPlayTimeService currentSessionPlayTimeService
            )
        {
            _currentSessionPlayTimeService = currentSessionPlayTimeService;
            _killsHandlerService = killsHandlerService;
            base.Construct(progressService);
            _stateMachine = stateMachine;
            _saveLoadService = saveLoadService;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _loadGameButton.onClick.AddListener(LoadProgress);

            _loadGameButton.interactable = _saveLoadService.HasProgress();
            
            SetText();
        }

        private void SetText()
        {
            float seconds = _currentSessionPlayTimeService.PlayTimeSeconds;
            int kills = _killsHandlerService.KillsCount;
            
            TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
            string timeFormatted = timeSpan.ToString(@"mm\:ss");
            
            _text.text = $"Good Game!\n\n" +
                         $"Time: {timeFormatted}\n" +
                         $"Kills: {kills}";
        }

        private void LoadProgress()
        {
            PlayerProgress loadedProgress = _saveLoadService.LoadProgress();

            if (loadedProgress == null)
            {
                _loadGameButton.interactable = false;
                return;
            }

            ProgressService.Progress = loadedProgress;

            base.OnCloseButtonClicked();
            RestartGame();
        }

        protected override void OnCloseButtonClicked()
        {
            base.OnCloseButtonClicked();
            ProgressService.Progress = CreateNewProgress();
            RestartGame();
        }
        

        // private PlayerProgress LoadProgressOrNew()
        // {
        //     return _saveLoadService.LoadProgress() ?? CreateNewProgress();
        // }

        private PlayerProgress CreateNewProgress()
        {
            PlayerProgress playerProgress = new PlayerProgress(initialLevel: MainScene, _persistentProgress);

            playerProgress.HeroHealth.IsInitialized = false;
            playerProgress.HeroStats.IsInitialized = false;

            return playerProgress;
        }
        
        private void RestartGame() => 
            _stateMachine.Enter<LoadLevelState, string>(MainScene);
    }
}
