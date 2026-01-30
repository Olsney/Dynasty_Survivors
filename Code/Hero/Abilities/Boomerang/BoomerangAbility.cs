using System.Collections.Generic;
using Code.Hero.Armaments.Factory;
using Code.Services.Cooldowns;
using Code.Services.Random;
using UnityEngine;
using Zenject;

namespace Code.Hero.Abilities.Boomerang
{
    public class BoomerangAbility : HeroAbility
    {
        private const string HittableLayerMask = "Hittable";
        private readonly Vector3 _spawnOffset = new Vector3(0, 2f, 0);

        private IArmamentFactory _armamentFactory;
        private IRandomService _random;
        private ICooldownService _cooldown;
        private BoomerangAbilityTargetFinder _targetFinder;
        private int _hittableLayerMask;

        [Inject]
        public void Construct(IArmamentFactory armamentFactory, IRandomService random)
        {
            _armamentFactory = armamentFactory;
            _random = random;
        }

        public override void Initialize(AbilityLevel abilityLevel)
        { 
            base.Initialize(abilityLevel);
            _cooldown = new CooldownService(AbilityLevel.Cooldown);
            _targetFinder = new BoomerangAbilityTargetFinder(_random);
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
                Shoot();
                _cooldown.PutOnCooldown();
            }
        }

        private void Shoot()
        {
            if (!_targetFinder.TryFindTargets(transform.position,
                    AbilityLevel.ProjectileSetup.Radius,
                    _hittableLayerMask,
                    3,
                    out List<Transform> targets))
                return;

            if (targets.Count == 0)
                return;

            Vector3 spawnPoint = transform.position + _spawnOffset;

            _armamentFactory.CreateBoomerangProjectile(spawnPoint,
                targets,
                transform,
                _hittableLayerMask,
                AbilityLevel.ProjectileSetup.Damage,
                AbilityLevel.ProjectileSetup.Speed,
                AbilityLevel.ProjectileSetup.ProjectilePrefab);
        }    }
}