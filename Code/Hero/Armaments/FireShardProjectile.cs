using Code.Logic;
using UnityEngine;

namespace Code.Hero.Armaments
{
    public class FireShardProjectile : MonoBehaviour
    {
        private const float LifeTime = 3f;
        private const int MaxHits = 2;

        private float _speed;
        private float _damage;
        private int _hittableMask;
        private Vector3 _direction;
        private float _timeLeft;
        private int _hits;

        public void Initialize(float speed, float damage, int hittableMask, Vector3 direction)
        {
            _speed = speed;
            _damage = damage;
            _hittableMask = hittableMask;
            _direction = direction.normalized;
            _timeLeft = LifeTime;
            _hits = 0;
        }

        private void Update()
        {
            transform.position += _direction * (_speed * Time.deltaTime);
            _timeLeft -= Time.deltaTime;

            if (_timeLeft <= 0f)
                Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((_hittableMask & (1 << other.gameObject.layer)) == 0)
                return;

            IDamageable damageable = other.GetComponentInParent<IDamageable>();
            
            if (damageable == null)
                return;

            damageable.TakeDamage(_damage);
            _hits++;

            if (_hits >= MaxHits)
                Destroy(gameObject);
        }
    }
}