using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Hero.Abilities
{
    [CreateAssetMenu(fileName = "AbilityData", menuName = "StaticData/AbilityData")]
    [Serializable]
    public class AbilityConfig : ScriptableObject
    {
        public AbilityType AbilityType;
        public List<AbilityLevel> AbilitySetups;
    }
}