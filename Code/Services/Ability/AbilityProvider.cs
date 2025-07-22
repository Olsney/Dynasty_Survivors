using System.Collections.Generic;
using Code.Services.Hero;
using UnityEngine;

namespace Code.Services.Ability
{
    public class AbilityProvider : IAbilityProvider
    {
        private readonly HeroProvider _heroProvider;
        private List<Code.Hero.Abilities.HeroAbility> _abilities;
        private GameObject _hero;
        
        public void Initialize(List<Code.Hero.Abilities.HeroAbility> abilities)
        {
            _abilities = abilities;
        }
        
        public List<Code.Hero.Abilities.HeroAbility> GetAbilities() => 
            new(_abilities);
    }
}