using Code.Logic;
using UnityEngine;

namespace Code.Hero.Armaments
{
    public class OrbitingProjectile : MonoBehaviour
    {
        private Transform _center;
        private float _radius;
        private float _rotationSpeed;
        private float _damage;
        private int _hittableMask;
        private float _lifeTime;
        private float _angle;
        private float _spawnHeightOffset = 1.2f;


        public void Initialize(Transform center, float radius, float rotationSpeed, float damage,
            int hittableMask, float lifeTime, float startAngle)
        {
            _center = center;
            _radius = radius;
            _rotationSpeed = rotationSpeed;
            _damage = damage;
            _hittableMask = hittableMask;
            _lifeTime = lifeTime;
            _angle = startAngle;
        }

        private void Update()
        {
            if (_center == null)
            {
                Destroy(gameObject);
                
                return;
            }

            _angle += _rotationSpeed * Time.deltaTime;
            float rad = _angle * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(rad), 0f, Mathf.Sin(rad)) * _radius;
            
            Vector3 heightOffset = new Vector3(0f, _spawnHeightOffset, 0f);
            transform.position = _center.position + heightOffset + offset;
            _lifeTime -= Time.deltaTime;

            if (_lifeTime <= 0f) 
                Destroy(gameObject);
        }

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