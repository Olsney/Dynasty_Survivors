using System.Collections.Generic;
using Code.Services.Random;
using UnityEngine;

namespace Code.Hero.Abilities.Boomerang
{
    public class BoomerangAbilityTargetFinder
    {
        private readonly IRandomService _random;

        public BoomerangAbilityTargetFinder(IRandomService random)
        {
            _random = random;
        }

        public bool TryFindTargets(Vector3 origin, float radius, int hittableMask, int count, out List<Transform> targets)
        {
            targets = new List<Transform>();
            Collider[] hits = Physics.OverlapSphere(origin, radius, hittableMask);
            if (hits.Length == 0)
                return false;

            List<Transform> candidates = new List<Transform>();
            foreach (Collider hit in hits)
                candidates.Add(hit.transform);

            for (int i = 0; i < count && candidates.Count > 0; i++)
            {
                int index = _random.Next(0, candidates.Count);
                targets.Add(candidates[index]);
                candidates.RemoveAt(index);
            }

            return targets.Count > 0;
        }    }
}