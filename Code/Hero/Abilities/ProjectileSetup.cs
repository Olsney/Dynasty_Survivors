using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Hero.Abilities
{
    [Serializable]
    public class ProjectileSetup
    {
        public GameObject ProjectilePrefab;
        public float Damage;
        public float Speed;
        [FormerlySerializedAs("RadiusToFindTarget")] public float Radius;
    }
}