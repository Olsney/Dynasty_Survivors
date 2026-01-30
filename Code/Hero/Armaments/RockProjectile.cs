using System;
using Code.Logic;
using UnityEngine;

namespace Code.Hero.Armaments
{
    public class RockProjectile : MonoBehaviour
    {
        private float _damage;
        private float _radius;
        private int _hittableMask;
        private float _lifeTime;

        public void Initialize(float damage, float radius, int hittableMask, float lifeTime)
        {
            _damage = damage;
            _radius = radius;
            _hittableMask = hittableMask;
            _lifeTime = lifeTime;
        }

        // private void Start()
        // {
        //     DealDamage();
        // }

        private void Update()
        {
            _lifeTime -= Time.deltaTime;
            if (_lifeTime <= 0f)
                Destroy(gameObject);
        }

        // private void DealDamage()
        // {
        //     Collider[] hits = Physics.OverlapSphere(transform.position, _radius, _hittableMask);
        //     
        //     foreach (Collider hit in hits)
        //     {
        //         if (hit.TryGetComponent<IDamageable>(out IDamageable damageable))
        //             damageable.TakeDamage(_damage);
        //     }
        // }

        private void OnTriggerEnter(Collider other)
        {
            if ((_hittableMask & (1 << other.gameObject.layer)) == 0)
                return;

            IDamageable damageable = other.GetComponentInParent<IDamageable>();

            if (damageable != null) 
                damageable.TakeDamage(_damage);
        }
    }
}