using System;
using Code.Services.Ability;
using Code.Services.PersistentProgress;
using Code.UI.Services.Windows;
using UnityEngine;
using Zenject;

namespace Code.Services.HeroLevelUp
{
    public class HeroLevelUpHandler : IHeroLevelUpHandler, IInitializable, IDisposable
    {
        private readonly IPersistentProgressService _persistentProgress;
        private readonly IWindowService _windowService;
        private readonly IAbilityService _abilityService;

        public HeroLevelUpHandler(IPersistentProgressService persistentProgress,
            IWindowService windowService,
            IAbilityService abilityService)
        {
            _persistentProgress = persistentProgress;
            _windowService = windowService;
            _abilityService = abilityService;
        }

        public void Initialize()
        {
            _persistentProgress.LevelUp += OnLevelUp;
        }

        public void Dispose()
        {
            _persistentProgress.LevelUp -= OnLevelUp;
        }

        public void OnLevelUp()
        {
            if (!_abilityService.CanChooseAnyAbility())
                return;
            
            _windowService.Open(WindowType.AbilityChoice);
        }
    }
}