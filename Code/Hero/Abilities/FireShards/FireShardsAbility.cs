using Code.Hero.Armaments.Factory;
using Code.Services.Cooldowns;
using UnityEngine;
using Zenject;

namespace Code.Hero.Abilities.FireShards
{
    public class FireShardsAbility : HeroAbility
    {
        private const string HittableLayerMask = "Hittable";
        private readonly Vector3 _spawnOffset = new Vector3(0, 2f, 0);

        private IArmamentFactory _armamentFactory;
        private ICooldownService _cooldown;
        private int _hittableLayerMask;

        [Inject]
        public void Construct(IArmamentFactory armamentFactory)
        {
            _armamentFactory = armamentFactory;
        }

        public override void Initialize(AbilityLevel abilityLevel)
        {
            base.Initialize(abilityLevel);
            _cooldown = new CooldownService(AbilityLevel.Cooldown);
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
            Vector3 spawnPoint = transform.position + _spawnOffset;

            int count = AbilityLevel.ProjectilesCount;
            float angleStep = 360f / count;

            for (int i = 0; i < count; i++)
            {
                float angle = angleStep * i;
                Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;

                _armamentFactory.CreateFireShardProjectile(
                    spawnPoint,
                    direction,
                    _hittableLayerMask,
                    AbilityLevel.ProjectileSetup.Damage,
                    AbilityLevel.ProjectileSetup.Speed,
                    AbilityLevel.ProjectileSetup.ProjectilePrefab);
            }
        }
    }
}