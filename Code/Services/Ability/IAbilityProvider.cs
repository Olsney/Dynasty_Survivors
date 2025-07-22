using System.Collections.Generic;

namespace Code.Services.Ability
{
    public interface IAbilityProvider
    {
        void Initialize(List<Code.Hero.Abilities.HeroAbility> abilities);
        List<Code.Hero.Abilities.HeroAbility> GetAbilities();
    }
}