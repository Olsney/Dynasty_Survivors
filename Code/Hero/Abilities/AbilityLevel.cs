using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Hero.Abilities
{
    [Serializable]
    public class AbilityLevel
    {
        public int Level;
        public int MaxLevel;
        public AbilityType AbilityType;
        public Sprite Icon;
        public float Cooldown;
        public int ProjectilesCount;
        public ProjectileSetup ProjectileSetup;
    }
}