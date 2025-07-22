using Code.Data;
using Code.Infrastructure.States;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Zenject;

namespace Code.UI.Windows
{
    public class GameOverWindow : WindowBase
    {
        private const string MainScene = "Main";
        
        private GameStateMachine _stateMachine;
        private ISaveLoadService _saveLoadService;

        [Inject]
        public void Construct(IPersistentProgressService progressService, 
            GameStateMachine stateMachine,
            ISaveLoadService saveLoadService)
        {
            base.Construct(progressService);
            _stateMachine = stateMachine;
            _saveLoadService = saveLoadService;
        }

        protected override void OnCloseButtonClicked()
        {
            base.OnCloseButtonClicked();
            ProgressService.Progress = LoadProgressOrNew();
            RestartGame();
        }
        

        private PlayerProgress LoadProgressOrNew() =>
            _saveLoadService.LoadProgress() ?? NewProgress();

        private PlayerProgress NewProgress()
        {
            PlayerProgress playerProgress = new PlayerProgress(initialLevel: MainScene);

            playerProgress.HeroHealth.IsInitialized = false;
            playerProgress.HeroStats.IsInitialized = false;

            return playerProgress;
        }
        
        private void RestartGame() => 
            _stateMachine.Enter<LoadLevelState, string>(MainScene);
    }
}