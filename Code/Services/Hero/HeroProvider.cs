using UnityEngine;

namespace Code.Services.Hero
{
    public class HeroProvider : IHeroProvider
    {
        private GameObject _hero;

        public void Initialize(GameObject hero) => 
            _hero = hero;

        public GameObject GetHero() => 
            _hero;
    }
}