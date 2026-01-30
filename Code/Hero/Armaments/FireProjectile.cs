using Code.Logic;
using UnityEngine;

namespace Code.Hero.Armaments
{
    public class FireProjectile : MonoBehaviour
    {
        private const float LifeTime = 3f;

        private float _speed;
        private float _damage;
        private int _hittableMask;
        private Vector3 _direction;
        private float _timeLeft;
        
        public void Initialize(float speed, float damage, int hittableMask, Vector3 direction)
        {
            _speed = speed;
            _damage = damage;
            _hittableMask = hittableMask;
            _direction = direction.normalized;
            _timeLeft = LifeTime;
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

            if (damageable != null)
            {
                damageable.TakeDamage(_damage);
                
                Destroy(gameObject);
            }
        }
    }
}