using System;
using Code.Hero.Abilities;
using Code.Hero.Abilities.Factory;
using Code.Services.Hero;
using Code.Services.StaticData;
using UnityEngine;

namespace Code.Services.Ability
{
    public class AbilityService : IAbilityService
    {
        private readonly IAbilityProvider _abilityProvider;
        private readonly IStaticDataService _staticData;
        private readonly IHeroProvider _heroProvider;
        private readonly IAbilityFactory _abilityFactory;

        public AbilityService(IAbilityProvider abilityProvider,
            IStaticDataService staticData,
            IHeroProvider heroProvider,
            IAbilityFactory abilityFactory)
        {
            _abilityProvider = abilityProvider;
            _staticData = staticData;
            _heroProvider = heroProvider;
            _abilityFactory = abilityFactory;
        }

        public bool CanChooseAnyAbility()
        {
            foreach (AbilityType abilityType in Enum.GetValues(typeof(AbilityType)))
            {
                if (abilityType == AbilityType.None)
                    continue;

                if (CanChooseAbility(abilityType))
                    return true;
            }

            return false;
        }

        public bool CanChooseAbility(AbilityType abilityType)
        {
            if (!_abilityProvider.TryGetAbility(abilityType, out HeroAbility ability))
                return true;

            AbilityConfig abilityConfig = _staticData.GetAbilityConfig(abilityType);
            int maxLevel = abilityConfig.AbilitySetups.Count;
            int nextLevel = ability.Level + 1;

            if (nextLevel > maxLevel)
                return false;

            return true;
        }

        public void HandleChosenAbility(AbilityType abilityType)
        {
            if (!CanChooseAbility(abilityType))
                return;

            GameObject hero = _heroProvider.GetHero();

            if (_abilityProvider.TryGetAbility(abilityType, out HeroAbility abilityToUpgrade))
            {
                int nextLevel = abilityToUpgrade.Level + 1;

                AbilityLevel abilityLevelConfig = _staticData.GetAbilityLevel(
                    abilityType: abilityToUpgrade.AbilityType,
                    level: nextLevel);

                abilityToUpgrade.Initialize(abilityLevelConfig);
            }
            else
            {
                HeroAbility newAbility = _abilityFactory.CreateAbility(abilityType, hero, level: 1);
                _abilityProvider.AddAbility(newAbility);
            }
        }
    }
}