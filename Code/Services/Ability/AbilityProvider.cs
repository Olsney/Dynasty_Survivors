using System;
using System.Collections.Generic;
using Code.Hero.Abilities;
using Code.Services.Hero;
using UnityEngine;

namespace Code.Services.Ability
{
    public class AbilityProvider : IAbilityProvider
    {
        private List<HeroAbility> _abilities = new();
        private GameObject _hero;

        public event Action<HeroAbility> AbilityAdded;

        public void Initialize(List<HeroAbility> abilities)
        {
            _abilities = abilities ?? new List<HeroAbility>();
        }

        public List<HeroAbility> GetAbilities() =>
            new(_abilities);

        public bool HasAbility(AbilityType abilityType)
        {
            foreach (HeroAbility heroAbility in _abilities)
            {
                if (heroAbility.AbilityType == abilityType)
                    return true;
            }

            return false;
        }

        public bool TryGetAbility(AbilityType abilityType, out HeroAbility ability)
        {
            foreach (HeroAbility heroAbility in _abilities)
            {
                if (heroAbility.AbilityType == abilityType)
                {
                    ability = heroAbility;
                    return true;
                }
            }

            ability = default;
            return false;
        }

        public void AddAbility(HeroAbility heroAbility)
        {
            if (_abilities.Contains(heroAbility))
                return;

            _abilities.Add(heroAbility);
            AbilityAdded?.Invoke(heroAbility);
        }
    }
}