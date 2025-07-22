﻿using Code.Hero.Abilities;
using Code.Hero.Abilities.Factory;
using Code.Hero.Armaments.Factory;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Services.Identifiers;
using Code.Infrastructure.States;
using Code.Logic.Curtain;
using Code.Services.Ability;
using Code.Services.Hero;
using Code.Services.Input;
using Code.Services.PersistentProgress;
using Code.Services.Random;
using Code.Services.SaveLoad;
using Code.Services.SceneProvider;
using Code.Services.StaticData;
using Code.Services.TimeService;
using Code.UI.Services.Factory;
using Code.UI.Services.Windows;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            BindCoroutine();
            BindServices();
            BindFactories();
            BindStates();
            BindSceneLoader();
            
            BindLoadingCurtain();
        }

        private void BindFactories()
        {
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
            Container.Bind<IArmamentFactory>().To<ArmamentFactory>().AsSingle();
            Container.Bind<IAbilityFactory>().To<AbilityFactory>().AsSingle();
        }

        private void BindCoroutine() => 
            Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();

        private void BindServices()
        {
            BindInputService();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IPersistentProgressService>().To<PersistentProgressService>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
            Container.Bind<IIdentifierService>().To<IdentifierService>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<IRandomService>().To<UnityRandomService>().AsSingle();
            Container.Bind<IWindowService>().To<WindowService>().AsSingle();
            Container.Bind<ISceneProvider>().To<SceneProvider>().AsSingle();
            Container.Bind<ITimeService>().To<UnityTimeService>().AsSingle();
            Container.Bind<IAbilityProvider>().To<AbilityProvider>().AsSingle();
            Container.Bind<IHeroProvider>().To<HeroProvider>().AsSingle();
        }

        private void BindStates()
        {
            Container.Bind<GameStateMachine>().AsSingle();
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<LoadProgressState>().AsSingle();
            Container.Bind<GameLoopState>().AsSingle();
        }
        
        private void BindSceneLoader() => 
            Container.Bind<SceneLoader>().AsSingle();
        
        private void BindLoadingCurtain() => 
            Container.BindInterfacesAndSelfTo<LoadingCurtainProvider>().AsSingle();

        private void BindInputService()
        {
            if (Application.isEditor)
                Container.Bind<IInputService>().To<StandaloneInputService>().AsSingle();
            else
                Container.Bind<IInputService>().To<MobileInputService>().AsSingle();
        }
    }
}