using Code.Hero.Abilities;
using Code.Services.Hero;
using Code.Services.PlayTime;
using Code.UI.Services.Windows;
using UnityEngine;
using Zenject;

namespace Code.Hero
{
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth _health;
        [SerializeField] private HeroMove _move;
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private GameObject _deathEffectPrefab;
        [SerializeField] private Transform _deathEffectSpawnPoint;

        private bool _isDead;
        private IWindowService _windowService;
        private IHeroProvider _heroProvider;
        private ICurrentSessionPlayTimeService _currentSessionPlayTimeService;

        [Inject]
        public void Construct(
            IWindowService windowService,
            IHeroProvider heroProvider,
            ICurrentSessionPlayTimeService currentSessionPlayTimeService)
        {
            _windowService = windowService;
            _heroProvider = heroProvider;
            _currentSessionPlayTimeService = currentSessionPlayTimeService;
        }


        private void Start() =>
            _health.Changed += OnHealthChanged;

        private void OnDestroy() =>
            _health.Changed -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (!_isDead && _health.Current <= 0)
                Die();
        }

        private void Die()
        {
            float effectVisualDuration = 3f;

            _move.enabled = false;
            _attack.enabled = false;
            _animator.PlayDeath();
            
            GameObject deathEffect =
                Instantiate(_deathEffectPrefab, _deathEffectSpawnPoint.position, Quaternion.identity);
            
            DeactivateHeroAbilities();

            _isDead = true;

            Destroy(deathEffect, effectVisualDuration);

            _windowService.Open(WindowType.GameOver);
            _currentSessionPlayTimeService.Stop();
        }

        private void DeactivateHeroAbilities()
        {
            HeroAbility[] heroAbilities = _heroProvider.GetHero().GetComponents<HeroAbility>();

            foreach (var ability in heroAbilities) 
                ability.enabled = false;
        }
    }
}
