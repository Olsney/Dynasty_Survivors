using System.Collections.Generic;
using Code.Hero.Abilities.RockAttack;
using Code.Hero.Armaments.Factory;
using Code.Services.Cooldowns;
using Code.Services.Random;
using UnityEngine;
using Zenject;

namespace Code.Hero.Abilities.Rockfall
{
    public class RockAttackAbility : HeroAbility
    {
        private const string HittableLayerMask = "Hittable";
        private const float ProjectileLifeTime = 3f;

        private IArmamentFactory _armamentFactory;
        private ICooldownService _cooldown;
        private IRandomService _random;
        private RockfallAbilityTargetFinder _targetFinder;
        private AbilityLevel _abilityLevel;
        private int _hittableMask;

        [Inject]
        public void Construct(IArmamentFactory armamentFactory, IRandomService random)
        {
            _armamentFactory = armamentFactory;
            _random = random;
        }

        public void Initialize(AbilityLevel abilityLevel)
        {
            _abilityLevel = abilityLevel;
            _cooldown = new CooldownService(_abilityLevel.Cooldown);
            _targetFinder = new RockfallAbilityTargetFinder(_random);
        }

        private void Awake()
        {
            _hittableMask = 1 << LayerMask.NameToLayer(HittableLayerMask);
        }

        private void Update()
        {
            if (_cooldown == null)
                return;

            _cooldown.Tick(Time.deltaTime);

            if (_cooldown.IsReady)
            {
                if (SpawnProjectiles(_abilityLevel.ProjectilesCount))
                    _cooldown.PutOnCooldown();
            }
        }

        private bool SpawnProjectiles(int count)
        {
            float radius = _abilityLevel.ProjectileSetup.Radius;

            if (!_targetFinder.TryFindTargets(transform.position, radius,
                    _hittableMask, count, out List<Transform> targets))
                return false;

            foreach (Transform target in targets)
            {
                Vector3 spawnPoint = target.position;
                
                _armamentFactory.CreateRockProjectile(
                    spawnPoint,
                    _hittableMask,
                    _abilityLevel.ProjectileSetup.Damage,
                    _abilityLevel.ProjectileSetup.Radius,
                    ProjectileLifeTime,
                    _abilityLevel.ProjectileSetup.ProjectilePrefab);
            }

            return true;
        }
    }
}
