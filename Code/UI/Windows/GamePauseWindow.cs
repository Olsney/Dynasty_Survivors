using Code.Infrastructure.States;
using Code.Services.PersistentProgress;
using Code.Services.TimeService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Windows
{
    public class GamePauseWindow : WindowBase
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _mainMenuButton;

        private ITimeService _timeService;
        private GameStateMachine _stateMachine;
        private bool _isGamePaused;

        [Inject]
        public void Construct(
            IPersistentProgressService progressService,
            ITimeService timeService,
            GameStateMachine stateMachine)
        {
            base.Construct(progressService);
            _timeService = timeService;
            _stateMachine = stateMachine;
        }

        protected override void Initialize()
        {
            base.Initialize();

            PauseGame();

            if (_continueButton != null)
                _continueButton.onClick.AddListener(OnContinueClicked);

            if (_mainMenuButton != null)
                _mainMenuButton.onClick.AddListener(OnMainMenuClicked);
        }

        protected override void Cleanup()
        {
            base.Cleanup();

            if (_continueButton != null)
                _continueButton.onClick.RemoveListener(OnContinueClicked);

            if (_mainMenuButton != null)
                _mainMenuButton.onClick.RemoveListener(OnMainMenuClicked);

            ResumeGame();
        }

        protected override void OnCloseButtonClicked()
        {
            ResumeGame();
            base.OnCloseButtonClicked();
        }

        private void OnContinueClicked() =>
            OnCloseButtonClicked();

        private void OnMainMenuClicked()
        {
            OnCloseButtonClicked();
            _stateMachine.Enter<LoadMainMenuState>();
        }

        private void PauseGame()
        {
            if (_isGamePaused)
                return;

            _timeService.Pause();
            _isGamePaused = true;
        }

        private void ResumeGame()
        {
            if (_isGamePaused == false)
                return;

            _timeService.UnPause();
            _isGamePaused = false;
        }
    }
}
