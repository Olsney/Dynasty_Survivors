using System;
using Code.Hero.Abilities.Factory;
using Code.Hero.Armaments;
using Code.Hero.Armaments.Factory;
using Code.Services.Cooldowns;
using Code.Services.SceneProvider;
using UnityEngine;
using Zenject;

namespace Code.Hero.Abilities.Orbiting
{
    public class OrbitingAbility : HeroAbility
    {
        private const string HittableLayerMask = "Hittable";
        private const float ProjectileLifeTime = 15f;
        
        private IInstantiator _instantiator;
        private ISceneProvider _sceneProvider;
        private ICooldownService _cooldown;
        private AbilityLevel _abilityLevel;
        private int _hittableLayerMask;
        private IArmamentFactory _armamentFactory;

        [Inject]
        public void Construct(IInstantiator instantiator, 
            ISceneProvider sceneProvider,
            IArmamentFactory armamentFactory)
        {
            _instantiator = instantiator;
            _sceneProvider = sceneProvider;
            _armamentFactory = armamentFactory;
        }

        public void Initialize(AbilityLevel abilitylevel)
        {
            _abilityLevel = abilitylevel;
            _cooldown = new CooldownService(_abilityLevel.Cooldown);
        }

        private void Awake()
        {
            _hittableLayerMask = 1 << LayerMask.NameToLayer(HittableLayerMask);
        }

        private void Update()
        {
            if (_cooldown == null)
                return;
            
            _cooldown.Tick(Time.deltaTime);
            
            if (_cooldown.IsReady)
            {
                SpawnProjectiles();
                _cooldown.PutOnCooldown();
            }
        }
        
        
        private void SpawnProjectiles()
        {
            for (int i = 0; i < _abilityLevel.ProjectilesCount; i++)
            {
                float angle = 360f / _abilityLevel.ProjectilesCount * i;
                
                _armamentFactory.CreateOrbitProjectile(
                    owner: gameObject,
                    center: transform,
                    projectilePrefab: _abilityLevel.ProjectileSetup.ProjectilePrefab,
                    hittableMask: _hittableLayerMask,
                    damage: _abilityLevel.ProjectileSetup.Damage,
                    rotationSpeed: _abilityLevel.ProjectileSetup.Speed,
                    radius: _abilityLevel.ProjectileSetup.Radius,
                    ProjectileLifeTime,
                    angle);
            }
        }
    }
}