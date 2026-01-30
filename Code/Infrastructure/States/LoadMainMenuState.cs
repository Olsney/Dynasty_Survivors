using Code.Infrastructure.Factory;
using Code.Logic.Curtain;
using Code.UI.Services.Factory;

namespace Code.Infrastructure.States
{
    public class LoadMainMenuState : IState
    {
        private const string EmptySceneName = "Empty";
        private const string MainMenuSceneName = "Menu";

        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtainProvider _loadingCurtainProvider;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly GameStateMachine _stateMachine;

        public LoadMainMenuState(
            SceneLoader sceneLoader,
            LoadingCurtainProvider loadingCurtainProvider,
            IGameFactory gameFactory,
            IUIFactory uiFactory,
            GameStateMachine stateMachine)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtainProvider = loadingCurtainProvider;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _loadingCurtainProvider.Instance.Show();
            _uiFactory.Cleanup();
            _gameFactory.Cleanup();
            _sceneLoader.Load(EmptySceneName, () => _sceneLoader.Load(MainMenuSceneName, OnMenuLoaded));
        }

        public void Exit()
        {
        }

        private void OnMenuLoaded() =>
            _stateMachine.Enter<MainMenuState>();
    }
}
