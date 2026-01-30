using System.Collections.Generic;
using Code.Data;
using Code.Hero.Abilities;
using Code.Hero.Abilities.Factory;
using Code.Services.Ability;
using Code.Services.Hero;
using Code.Services.PersistentProgress;
using UnityEngine;

namespace Code.Services.AbilityProgress
{
    public class HeroAbilityProgressService : IHeroAbilityProgressService, ISavedProgress
    {
        private IAbilityProvider _abilityProvider;

        private readonly IHeroProvider _heroProvider;

        private readonly IAbilityFactory _abilityFactory;

        public HeroAbilityProgressService(IAbilityProvider abilityProvider,
            IHeroProvider heroProvider,
            IAbilityFactory abilityFactory)
        {
            _abilityProvider = abilityProvider;
            _heroProvider = heroProvider;
            _abilityFactory = abilityFactory;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            GameObject hero = _heroProvider.GetHero();
            List<HeroAbility> heroAbilities = new();
            
            AbilitiesData savedAbilities = progress.Abilities;

            if (savedAbilities.Abilities.Count == 0)
            {
                heroAbilities.Add(_abilityFactory.CreateAbility(
                    AbilityType.FireAttackAbility,
                    hero,
                    level: 1));
            }
            else
            {
                foreach (AbilityData abilityData in savedAbilities.Abilities)
                {
                    heroAbilities.Add(_abilityFactory.CreateAbility(
                        abilityData.AbilityType,
                        hero,
                        abilityData.Level));
                }
            }
            
            _abilityProvider.Initialize(heroAbilities);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            List<AbilityData> abilities = progress.Abilities.Abilities;
            abilities.Clear();

            foreach (HeroAbility heroAbility in _abilityProvider.GetAbilities())
            {
                abilities.Add(new AbilityData
                {
                    AbilityType = heroAbility.AbilityType,
                    Level = heroAbility.Level,
                });
            }
        }
    }
}