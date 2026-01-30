using Code.Hero.Abilities.Boomerang;
using Code.Hero.Abilities.FireAttack;
using Code.Hero.Abilities.Orbiting;
using UnityEngine;

namespace Code.Hero.Abilities.Factory
{
    public interface IAbilityFactory
    {
        HeroAbility CreateAbility(AbilityType abilityType, GameObject hero, int level);
    }
}