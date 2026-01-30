using Code.Logic.Curtain;
using Code.UI.Services.Factory;
using Code.UI.Services.Windows;

namespace Code.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private readonly IUIFactory _uiFactory;
        private readonly IWindowService _windowService;
        private readonly LoadingCurtainProvider _loadingCurtainProvider;

        public MainMenuState(
            IUIFactory uiFactory,
            IWindowService windowService,
            LoadingCurtainProvider loadingCurtainProvider)
        {
            _uiFactory = uiFactory;
            _windowService = windowService;
            _loadingCurtainProvider = loadingCurtainProvider;
        }

        public void Enter()
        {
            _uiFactory.CreateUIRoot();
            _windowService.Open(WindowType.MainMenu);
            _loadingCurtainProvider.Instance.Hide();
        }

        public void Exit()
        {
        }
    }
}
