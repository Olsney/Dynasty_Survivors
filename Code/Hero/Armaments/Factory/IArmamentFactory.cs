using System.Collections.Generic;
using Code.Hero.Abilities.Boomerang;
using UnityEngine;

namespace Code.Hero.Armaments.Factory
{
    public interface IArmamentFactory
    {
        FireProjectile CreateFireProjectile(
            Vector3 at,
            Vector3 direction,
            int hittableMask, 
            float damage,
            float speed,
            GameObject projectilePrefab);

        OrbitingProjectile CreateOrbitProjectile(
            GameObject owner,
            Transform center,
            GameObject projectilePrefab,
            int hittableMask,
            float damage,
            float rotationSpeed,
            float radius,
            float lifeTime,
            float startAngle);

        BoomerangProjectile CreateBoomerangProjectile(
            Vector3 at,
            System.Collections.Generic.List<Transform> targets,
            Transform returnTarget,
            int hittableMask,
            float damage,
            float speed,
            GameObject projectilePrefab);

        RockProjectile CreateRockProjectile(
            Vector3 spawnPoint, 
            int hittableMask, 
            float projectileSetupDamage, 
            float projectileSetupRadius, 
            float projectileLifeTime, 
            GameObject projectileSetupProjectilePrefab);
        
        FireShardProjectile CreateFireShardProjectile(
            Vector3 at,
            Vector3 direction,
            int hittableMask,
            float damage,
            float speed,
            GameObject projectilePrefab);
    }
}