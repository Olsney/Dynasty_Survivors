using UnityEngine;

namespace Code.Hero.Abilities
{
    public class HeroAbility : MonoBehaviour
    {
        protected AbilityLevel AbilityLevel;
        
        public AbilityType AbilityType => AbilityLevel.AbilityType;
        public int Level => AbilityLevel.Level;
        public int MaxLevel => AbilityLevel.MaxLevel;

        public virtual void Initialize(AbilityLevel abilityLevel)
        {
            AbilityLevel = abilityLevel;
        }
    }
}