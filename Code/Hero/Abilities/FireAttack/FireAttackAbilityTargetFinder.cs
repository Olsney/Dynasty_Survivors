using Code.Data;
using UnityEngine;

namespace Code.Hero.Abilities.FireAttack
{
    public class FireAttackAbilityTargetFinder
    {
        public bool TryFindTargets(Vector3 producerPoint, float radius, 
            int hittableLayerMask, out Transform target)
        {
            float minDistance = float.MaxValue;

            Collider[] hits = Physics.OverlapSphere(producerPoint, radius, hittableLayerMask);
            target = null;

            if (hits.Length == 0)
                return false;
            
            foreach (Collider hit in hits)
            {
                float distance = producerPoint.SqrDistance(hit.transform.position);

                if (distance < minDistance)
                {
                    target = hit.transform;
                    minDistance = distance;
                }
            }

            return true;
        }
    }
}