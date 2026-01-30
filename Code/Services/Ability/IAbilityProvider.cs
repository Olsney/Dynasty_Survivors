using System;
using System.Collections.Generic;
using Code.Hero.Abilities;

namespace Code.Services.Ability
{
    public interface IAbilityProvider
    {
        void Initialize(List<HeroAbility> abilities);
        List<HeroAbility> GetAbilities();
        bool HasAbility(AbilityType abilityType);
        bool TryGetAbility(AbilityType abilityType, out HeroAbility ability);
        void AddAbility(HeroAbility heroAbility);
        event Action<HeroAbility> AbilityAdded;
    }
}