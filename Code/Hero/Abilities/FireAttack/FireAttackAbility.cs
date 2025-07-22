using Code.Hero.Armaments.Factory;
using Code.Services.Cooldowns;
using UnityEngine;
using Zenject;

namespace Code.Hero.Abilities.FireAttack
{
    public class FireAttackAbility : HeroAbility
    {
        private const string HittableLayerMask = "Hittable";
        
        private readonly Vector3 _spawnOffset = new Vector3(0, 2f, 0);

        private IArmamentFactory _armamentFactory;

        private ICooldownService _cooldown;

        private int _hittableLayerMask;
        private FireAttackAbilityTargetFinder _targetFinder;
        private AbilityLevel _abilityLevel;
        
        [Inject]
        public void Construct(IArmamentFactory armamentFactory)
        {
            _armamentFactory = armamentFactory;
        }

        public void Initialize(AbilityLevel abilityLevel)
        {
            _abilityLevel = abilityLevel;
            
            _cooldown = new CooldownService(_abilityLevel.Cooldown);
            _targetFinder = new FireAttackAbilityTargetFinder();
        }
        
        private void Awake()
        {
            _hittableLayerMask = 1 << LayerMask.NameToLayer(HittableLayerMask);
        }

        private void Update()
        {
            UpdateCooldown();
            
            if (_cooldown.IsReady)
            {
                Shoot();
                _cooldown.PutOnCooldown();
            }
        }

        private void Shoot()
        {
            if (!_targetFinder.TryFindTargets(transform.position,
                    _abilityLevel.ProjectileSetup.Radius,
                    _hittableLayerMask,
                    out Transform target))
            {
                return;
            }

            if (target == null)
                return;
            
            Vector3 targetPosition = target.position;
            targetPosition.y = transform.position.y;
            Vector3 direction = (targetPosition - transform.position).normalized;

            Vector3 spawnPoint = transform.position + _spawnOffset;
            
            _armamentFactory.CreateFireProjectile(spawnPoint,
                direction, 
                _hittableLayerMask, 
                _abilityLevel.ProjectileSetup.Damage, 
                _abilityLevel.ProjectileSetup.Speed,
                _abilityLevel.ProjectileSetup.ProjectilePrefab);
            
        }

        private void UpdateCooldown() => 
            _cooldown.Tick(Time.deltaTime);
    }
}