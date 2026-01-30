using System;
using UnityEngine;

namespace Code.Services.Hero
{
    public class HeroProvider : IHeroProvider
    {
        private GameObject _hero;
        
        public event Action<GameObject> HeroChanged;

        public void Initialize(GameObject hero)
        {
            _hero = hero;
            
            HeroChanged?.Invoke(_hero);
        }

        public GameObject GetHero() => 
            _hero;
    }
}