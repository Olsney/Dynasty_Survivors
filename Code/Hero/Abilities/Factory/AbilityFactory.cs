using System;
using Code.Hero.Abilities.Boomerang;
using Code.Hero.Abilities.FireAttack;
using Code.Hero.Abilities.FireShards;
using Code.Hero.Abilities.Orbiting;
using Code.Hero.Abilities.Rockfall;
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

        public HeroAbility CreateAbility(AbilityType abilityType, GameObject hero)
        {
            switch (abilityType)
            {
                case AbilityType.None:
                    throw new ArgumentException("Invalid ability type");
                
                case AbilityType.FireAttackAbility:
                    return CreateFireAttackAbility(hero);
                    
                case AbilityType.OrbitingAbility:
                    return CreateOrbitingAbility(hero);
                
                case AbilityType.BoomerangAbility:
                    return CreateBoomerangAbility(hero);
                
                case AbilityType.RockAttackAbility:
                    return CreateRockAttackAbility(hero);
                
                case AbilityType.FireShardsAbility:
                    return CreateFireShardsAbility(hero);
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(abilityType), abilityType, null);
            }
        }

        public FireAttackAbility CreateFireAttackAbility(GameObject hero)
        {
            AbilityConfig abilityConfig = _staticData.GetAbilityConfig(AbilityType.FireAttackAbility);
            // AbilitySetup setup = _staticData.GetHeroAbilitySetup(AbilityType.FireAttackAbility);
            AbilityLevel abilityLevel = _staticData.GetAbilityLevel(AbilityType.FireAttackAbility, 1);
            
            FireAttackAbility fireAttackAbility = _instantiator.InstantiateComponent<FireAttackAbility>(hero);
            fireAttackAbility.Initialize(abilityLevel);
            
            return fireAttackAbility;
        }

        public OrbitingAbility CreateOrbitingAbility(GameObject hero)
        {
            AbilityConfig abilityConfig = _staticData.GetAbilityConfig(AbilityType.OrbitingAbility);
            AbilityLevel abilityLevel = _staticData.GetAbilityLevel(AbilityType.OrbitingAbility, 1);
            
            OrbitingAbility ability = _instantiator.InstantiateComponent<OrbitingAbility>(hero);
            ability.Initialize(abilityLevel);

            return ability;
        }

        public BoomerangAbility CreateBoomerangAbility(GameObject hero)
        {
            AbilityConfig abilityConfig = _staticData.GetAbilityConfig(AbilityType.BoomerangAbility);
            AbilityLevel abilityLevel = _staticData.GetAbilityLevel(AbilityType.BoomerangAbility, 1);
            
            BoomerangAbility heroAbility = _instantiator.InstantiateComponent<BoomerangAbility>(hero);
            heroAbility.Initialize(abilityLevel);

            return heroAbility;
        }

        public RockAttackAbility CreateRockAttackAbility(GameObject hero)
        {
            AbilityConfig abilityConfig = _staticData.GetAbilityConfig(AbilityType.RockAttackAbility);
            AbilityLevel abilityLevel = _staticData.GetAbilityLevel(AbilityType.RockAttackAbility, 1);
            
            RockAttackAbility heroAbility = _instantiator.InstantiateComponent<RockAttackAbility>(hero);
            heroAbility.Initialize(abilityLevel);

            return heroAbility;
        }


        public FireShardsAbility CreateFireShardsAbility(GameObject hero)
        {
            AbilityConfig abilityConfig = _staticData.GetAbilityConfig(AbilityType.FireShardsAbility);
            AbilityLevel abilityLevel = _staticData.GetAbilityLevel(AbilityType.FireShardsAbility, 1);

            FireShardsAbility ability = _instantiator.InstantiateComponent<FireShardsAbility>(hero);
            ability.Initialize(abilityLevel);

            return ability;
        }
    }
}