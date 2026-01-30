using System.Collections.Generic;
using Code.Hero.Abilities.Boomerang;
using Code.Infrastructure.AssetManagement;
using Code.Services.SceneProvider;
using UnityEngine;
using Zenject;

namespace Code.Hero.Armaments.Factory
{
    public class ArmamentFactory : IArmamentFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IInstantiator _instantiator;
        private readonly ISceneProvider _sceneProvider;

        public ArmamentFactory(IAssetProvider assets,
            IInstantiator instantiator,
            ISceneProvider sceneProvider)
        {
            _assets = assets;
            _instantiator = instantiator;
            _sceneProvider = sceneProvider;
        }

        public FireProjectile CreateFireProjectile(
            Vector3 at,
            Vector3 direction,
            int hittableMask,
            float damage,
            float speed,
            GameObject projectilePrefab)
        {
            // GameObject prefab = _assets.Load(AssetPath.RangeAttackProjectilePath);
            GameObject instance =
                _instantiator.InstantiatePrefab(projectilePrefab, at, Quaternion.identity, _sceneProvider.Container);
            FireProjectile fireProjectile = instance.GetComponent<FireProjectile>();
            fireProjectile.Initialize(speed, damage, hittableMask, direction);

            return fireProjectile;
        }

        public OrbitingProjectile CreateOrbitProjectile(
            GameObject owner,
            Transform center,
            GameObject projectilePrefab,
            int hittableMask,
            float damage,
            float rotationSpeed,
            float radius,
            float lifeTime,
            float startAngle)
        {
            GameObject instance = _instantiator.InstantiatePrefab(projectilePrefab, owner.transform.position,
                Quaternion.identity, _sceneProvider.Container);
            OrbitingProjectile projectile = instance.GetComponent<OrbitingProjectile>();
            projectile.Initialize(center, radius, rotationSpeed, damage, hittableMask, lifeTime, startAngle);

            return projectile;
        }

        public BoomerangProjectile CreateBoomerangProjectile(
            Vector3 at,
            System.Collections.Generic.List<Transform> targets,
            Transform returnTarget,
            int hittableMask,
            float damage,
            float speed,
            GameObject projectilePrefab)
        {
            GameObject instance =
                _instantiator.InstantiatePrefab(projectilePrefab, at, Quaternion.identity, _sceneProvider.Container);
            BoomerangProjectile projectile = instance.GetComponent<BoomerangProjectile>();
            projectile.Initialize(speed, damage, hittableMask, targets, returnTarget);

            return projectile;
        }

        public RockProjectile CreateRockProjectile(
            Vector3 at,
            int hittableMask,
            float damage,
            float radius,
            float lifeTime,
            GameObject projectilePrefab)
        {
            GameObject instance =
                _instantiator.InstantiatePrefab(projectilePrefab, at, 
                    Quaternion.identity, _sceneProvider.Container);
            
            RockProjectile projectile = instance.GetComponent<RockProjectile>();
            
            projectile.Initialize(damage, radius, hittableMask, lifeTime);

            return projectile;
        }
        
        public FireShardProjectile CreateFireShardProjectile(
            Vector3 at,
            Vector3 direction,
            int hittableMask,
            float damage,
            float speed,
            GameObject projectilePrefab)
        {
            GameObject instance =
                _instantiator.InstantiatePrefab(projectilePrefab, at, Quaternion.identity, _sceneProvider.Container);
            FireShardProjectile projectile = instance.GetComponent<FireShardProjectile>();
            projectile.Initialize(speed, damage, hittableMask, direction);

            return projectile;
        }

    }
}