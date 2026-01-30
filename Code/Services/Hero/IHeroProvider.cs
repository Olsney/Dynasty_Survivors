using System;
using UnityEngine;

namespace Code.Services.Hero
{
    public interface IHeroProvider
    {
        void Initialize(GameObject hero);
        GameObject GetHero();
        event Action<GameObject> HeroChanged;
    }
}