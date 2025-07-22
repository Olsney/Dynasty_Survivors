using Code.Hero.Abilities.Boomerang;
using Code.Hero.Abilities.FireAttack;
using Code.Hero.Abilities.Orbiting;
using UnityEngine;

namespace Code.Hero.Abilities.Factory
{
    public interface IAbilityFactory
    {
        FireAttackAbility CreateFireAttackAbility(GameObject hero);
        OrbitingAbility CreateOrbitingAbility(GameObject hero);
        HeroAbility CreateAbility(AbilityType abilityType, GameObject hero);
        BoomerangAbility CreateBoomerangAbility(GameObject hero);
    }
}