using System.Collections.Generic;
using Code.Services.Random;
using UnityEngine;

namespace Code.Hero.Abilities.RockAttack
{
    public class RockfallAbilityTargetFinder
    {
        private readonly IRandomService _random;

        public RockfallAbilityTargetFinder(IRandomService random)
        {
            _random = random;
        }

        public bool TryFindTarget(Vector3 origin, float radius, int hittableMask, out Transform target)
        {
            Collider[] hits = Physics.OverlapSphere(origin, radius, hittableMask);
            
            if (hits.Length == 0)
            {
                target = null;
                
                return false;
            }

            int index = _random.Next(0, hits.Length);
            target = hits[index].transform;
            
            return true;
        }

        public bool TryFindTargets(Vector3 origin, float radius, int hittableMask, int count, out List<Transform> targets)
        {
            targets = new List<Transform>();
            Collider[] hits = Physics.OverlapSphere(origin, radius, hittableMask);
            
            if (hits.Length == 0)
                return false;

            var candidates = new List<Transform>();
            
            foreach (Collider hit in hits)
                candidates.Add(hit.transform);

            for (int i = 0; i < count && candidates.Count > 0; i++)
            {
                int index = _random.Next(0, candidates.Count);
                
                targets.Add(candidates[index]);
                candidates.RemoveAt(index);
            }

            return targets.Count == count;
        }
    }
}