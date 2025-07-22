using UnityEngine;

namespace Code.Services.Hero
{
    public interface IHeroProvider
    {
        void Initialize(GameObject hero);
        GameObject GetHero();
    }
}