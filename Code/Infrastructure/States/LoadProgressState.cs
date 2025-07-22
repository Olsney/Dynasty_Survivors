using Code.Logic.Curtain;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.UI.Services.Factory;
using Code.UI.Services.Windows;

namespace Code.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IWindowService _windowService;
        private readonly LoadingCurtainProvider _loadingCurtainProvider;
        private readonly IUIFactory _uiFactory;

        public LoadProgressState(GameStateMachine stateMachine, 
            IPersistentProgressService progressService,
            ISaveLoadService saveLoadService,
            IWindowService windowService,
            LoadingCurtainProvider loadingCurtainProvider,
            IUIFactory uiFactory
            )
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _windowService = windowService;
            _loadingCurtainProvider = loadingCurtainProvider;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
            _uiFactory.CreateUIRoot();
            _windowService.Open(WindowId.MainMenu);
            _loadingCurtainProvider.Instance.Hide();
        }

        public void Exit()
        {
        }
    }
}