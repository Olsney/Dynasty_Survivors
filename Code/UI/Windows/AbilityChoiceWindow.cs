using System.Collections.Generic;
using Code.Hero.Abilities;
using Code.Hero.Abilities.Factory;
using Code.Services.Ability;
using Code.Services.Hero;
using Code.Services.PersistentProgress;
using Code.Services.StaticData;
using Code.Services.TimeService;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Windows
{
    public class AbilityChoiceWindow : WindowBase
    {
        [SerializeField] private Button _fireAbilityButton;
        [SerializeField] private Button _orbitingAbilityButton;
        [SerializeField] private Button _boomerangAbilityButton;
        [SerializeField] private Button _rockAbilityButton;
        [SerializeField] private Button _fireShardsAbilityButton;
        
        private IAbilityFactory _abilityFactory;
        private IHeroProvider _heroProvider;
        private IAbilityProvider _abilityProvider;
        private IStaticDataService _staticDataService;
        private IAbilityService _abilityService;
        private ITimeService _timeService;

        [Inject]
        public void Construct(IPersistentProgressService progressService, 
            IAbilityFactory abilityFactory, 
            IHeroProvider heroProvider,
            IAbilityProvider abilityProvider,
            IStaticDataService staticDataService,
            IAbilityService abilityService,
            ITimeService timeService)
        {
            base.Construct(progressService);
            _abilityFactory = abilityFactory;
            _heroProvider = heroProvider;
            _abilityProvider = abilityProvider;
            _staticDataService = staticDataService;
            _abilityService = abilityService;
            _timeService = timeService;
        }

        protected override void Initialize()
        {
            _timeService.Pause();
            
            InitializeButton(_fireAbilityButton, AbilityType.FireAttackAbility);
            InitializeButton(_orbitingAbilityButton, AbilityType.OrbitingAbility);
            InitializeButton(_boomerangAbilityButton, AbilityType.BoomerangAbility);
            InitializeButton(_rockAbilityButton, AbilityType.RockAttackAbility);
            InitializeButton(_fireShardsAbilityButton, AbilityType.FireShardsAbility);
        }

        private void InitializeButton(Button button, AbilityType abilityType)
        {
            if (!_abilityService.CanChooseAbility(abilityType))
                button.interactable = false;
            
            button.onClick.AddListener(() => ChooseAbility(abilityType));
        }

        private void ChooseAbility(AbilityType abilityType)
        {
            _abilityService.HandleChosenAbility(abilityType);
            
            _timeService.UnPause();
            OnCloseButtonClicked();
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _timeService.UnPause();
        }
    }
}
