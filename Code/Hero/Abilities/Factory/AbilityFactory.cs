using System;
using Code.Hero.Abilities.Boomerang;
using Code.Hero.Abilities.FireAttack;
using Code.Hero.Abilities.FireShards;
using Code.Hero.Abilities.Orbiting;
using Code.Hero.Abilities.RockAttack;
using Code.Services.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Hero.Abilities.Factory
{
    public class AbilityFactory : IAbilityFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IStaticDataService _staticData;

        public AbilityFactory(IInstantiator instantiator,
            IStaticDataService staticData)
        {
            _instantiator = instantiator;
            _staticData = staticData;
        }

        public HeroAbility CreateAbility(AbilityType abilityType, GameObject hero, int level)
        {
            switch (abilityType)
            {
                case AbilityType.None:
                    throw new ArgumentException("Invalid ability type");
                
                case AbilityType.FireAttackAbility:
                    return CreateFireAttackAbility(hero, level);
                    
                case AbilityType.OrbitingAbility:
                    return CreateOrbitingAbility(hero, level);
                
                case AbilityType.BoomerangAbility:
                    return CreateBoomerangAbility(hero, level);
                
                case AbilityType.RockAttackAbility:
                    return CreateRockAttackAbility(hero, level);
                
                case AbilityType.FireShardsAbility:
                    return CreateFireShardsAbility(hero, level);
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(abilityType), abilityType, null);
            }
        }

        public FireAttackAbility CreateFireAttackAbility(GameObject hero, int level)
        {
            AbilityConfig abilityConfig = _staticData.GetAbilityConfig(AbilityType.FireAttackAbility);
            // AbilitySetup setup = _staticData.GetHeroAbilitySetup(AbilityType.FireAttackAbility);
            AbilityLevel abilityLevel = _staticData.GetAbilityLevel(AbilityType.FireAttackAbility, level);
            
            FireAttackAbility fireAttackAbility = _instantiator.InstantiateComponent<FireAttackAbility>(hero);
            fireAttackAbility.Initialize(abilityLevel);
            
            return fireAttackAbility;
        }

        public OrbitingAbility CreateOrbitingAbility(GameObject hero, int level)
        {
            AbilityConfig abilityConfig = _staticData.GetAbilityConfig(AbilityType.OrbitingAbility);
            AbilityLevel abilityLevel = _staticData.GetAbilityLevel(AbilityType.OrbitingAbility, level);
            
            OrbitingAbility ability = _instantiator.InstantiateComponent<OrbitingAbility>(hero);
            ability.Initialize(abilityLevel);

            return ability;
        }

        public BoomerangAbility CreateBoomerangAbility(GameObject hero, int level)
        {
            AbilityConfig abilityConfig = _staticData.GetAbilityConfig(AbilityType.BoomerangAbility);
            AbilityLevel abilityLevel = _staticData.GetAbilityLevel(AbilityType.BoomerangAbility, level);
            
            BoomerangAbility heroAbility = _instantiator.InstantiateComponent<BoomerangAbility>(hero);
            heroAbility.Initialize(abilityLevel);

            return heroAbility;
        }

        public RockAttackAbility CreateRockAttackAbility(GameObject hero, int level)
        {
            AbilityConfig abilityConfig = _staticData.GetAbilityConfig(AbilityType.RockAttackAbility);
            AbilityLevel abilityLevel = _staticData.GetAbilityLevel(AbilityType.RockAttackAbility, level);
            
            RockAttackAbility heroAbility = _instantiator.InstantiateComponent<RockAttackAbility>(hero);
            heroAbility.Initialize(abilityLevel);

            return heroAbility;
        }


        public FireShardsAbility CreateFireShardsAbility(GameObject hero, int level)
        {
            AbilityConfig abilityConfig = _staticData.GetAbilityConfig(AbilityType.FireShardsAbility);
            AbilityLevel abilityLevel = _staticData.GetAbilityLevel(AbilityType.FireShardsAbility, level);

            FireShardsAbility ability = _instantiator.InstantiateComponent<FireShardsAbility>(hero);
            ability.Initialize(abilityLevel);

            return ability;
        }
    }
}