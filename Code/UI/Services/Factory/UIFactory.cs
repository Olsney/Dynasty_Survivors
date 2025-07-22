using Code.Infrastructure.AssetManagement;
using Code.Services.SceneProvider;
using Code.Services.StaticData;
using Code.StaticData.Windows;
using Code.UI.Services.Windows;
using UnityEngine;
using Zenject;

namespace Code.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IInstantiator _container;
        private readonly ISceneProvider _sceneProvider;

        private Transform _uiRoot;

        public UIFactory(
            IAssetProvider assets, 
            IStaticDataService staticData, 
            IInstantiator container,
            ISceneProvider sceneProvider
            )
        {
            _assets = assets;
            _staticData = staticData;
            _container = container;
            _sceneProvider = sceneProvider;
        }

        public void CreateUIRoot()
        {
            if (_uiRoot != null)
                return;
            
            GameObject prefab = _assets.Load(Constants.UIRootPath);
            _uiRoot = _container.InstantiatePrefab(prefab, _sceneProvider.Container).transform;
        }

        public void CreateMainMenu()
        {
            WindowConfig config = _staticData.GetWindow(WindowId.MainMenu);
            _container.InstantiatePrefab(config.Prefab, _uiRoot);
        }

        public void CreateShop()
        {
            WindowConfig config = _staticData.GetWindow(WindowId.Shop);
            _container.InstantiatePrefab(config.Prefab, _uiRoot);
        }

        public void CreateGameOverWindow()
        {
            WindowConfig config = _staticData.GetWindow(WindowId.GameOver);
            _container.InstantiatePrefab(config.Prefab, _uiRoot);
        }

        public void CreateSettingsWindow()
        {
            WindowConfig config = _staticData.GetWindow(WindowId.Settings);
            _container.InstantiatePrefab(config.Prefab, _uiRoot);

        }

        public void CreateAuthorWindow()
        {
            WindowConfig config = _staticData.GetWindow(WindowId.Author);
            _container.InstantiatePrefab(config.Prefab, _uiRoot);
        }
    }
}