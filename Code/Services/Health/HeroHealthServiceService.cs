using System;
using Code.Hero;
using Code.Services.Hero;
using UnityEngine;
using Zenject;

namespace Code.Services.Health
{
    public class HeroHealthServiceService : IHeroHealthService, IInitializable, IDisposable
    {
        private readonly IHeroProvider _heroProvider;

        private GameObject _hero;
        private HeroHealth _heroHealth;
        public event Action HealthChanged;

        public float CurrentHealth => _heroHealth.Current;
        public float MaxHealth => _heroHealth.Max;

        public HeroHealthServiceService(IHeroProvider heroProvider)
        {
            _heroProvider = heroProvider;
        }


        public void Initialize()
        {
            _heroProvider.HeroChanged += OnHeroChanged;
            
            // OnHeroChanged(_heroProvider.GetHero());
        }

        public void Dispose()
        {
            _heroProvider.HeroChanged -= OnHeroChanged;

            if (_heroHealth != null)
                _heroHealth.Changed -= OnHeroHealthChanged;

            _heroHealth = null;
            _hero = null;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void OnHeroChanged(GameObject obj)
        {
            if (_hero != null)
            {
                Unsubscribe();
            }
            
            _hero = _heroProvider.GetHero();
            _heroHealth = _hero.GetComponent<HeroHealth>();
            
            _heroHealth.Changed += OnHeroHealthChanged;
        }

        private void Unsubscribe()
        {
            _heroHealth.Changed -= OnHeroHealthChanged;
        }

        public void Heal(float amount)
        {
            if (amount <= 0f)
                return;

            if (_heroHealth == null)
                return;

            _heroHealth.Heal(amount);
            HealthChanged?.Invoke();
        }


        private void OnHeroHealthChanged()
        {
            HealthChanged?.Invoke();
        }
    }
}