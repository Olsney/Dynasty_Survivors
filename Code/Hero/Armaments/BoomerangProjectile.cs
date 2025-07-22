using System.Collections.Generic;
using Code.Logic;
using UnityEngine;

namespace Code.Hero.Armaments
{
    public class BoomerangProjectile : MonoBehaviour
    {
        private float _speed;
        private float _damage;
        private int _hittableMask;
        private readonly List<Vector3> _points = new();
        private readonly List<Transform> _targets = new();

        private Transform _returnTarget;
        private int _currentIndex;
        private bool _isReturning;

        private float _targetOffsetY = 1.0f;

        public void Initialize(float speed, float damage, int hittableMask, List<Transform> targets, Transform returnTarget)
        {
            _speed = speed;
            _damage = damage;
            _hittableMask = hittableMask;
            _returnTarget = returnTarget;

            foreach (Transform target in targets)
            {
                if (target != null)
                {
                    Vector3 targetPoint = target.position + new Vector3(0, _targetOffsetY, 0);
                    _points.Add(targetPoint);
                    _targets.Add(target);
                }
            }
        }

        private void Update()
        {
            if (_isReturning)
            {
                MoveTowards(GetReturnTargetPosition());

                Vector3 flatSelf = new Vector3(transform.position.x, 0, transform.position.z);
                Vector3 flatTarget = new Vector3(_returnTarget.position.x, 0, _returnTarget.position.z);

                if (Vector3.Distance(flatSelf, flatTarget) <= 0.2f)
                    Destroy(gameObject);
            }
            else
            {
                if (_currentIndex < _points.Count)
                {
                    MoveTowards(_points[_currentIndex]);
                }
                else
                {
                    _isReturning = true;
                }
            }
        }

        private void MoveTowards(Vector3 target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);

            if (!_isReturning && Vector3.Distance(transform.position, target) <= 0.2f)
                _currentIndex++;
        }

        private Vector3 GetReturnTargetPosition() => 
            _returnTarget.position + new Vector3(0, _targetOffsetY, 0);

        private void OnTriggerEnter(Collider other)
        {
            if ((_hittableMask & (1 << other.gameObject.layer)) == 0)
                return;

            IDamageable damageable = other.GetComponentInParent<IDamageable>();
            
            if (damageable != null)
                damageable.TakeDamage(_damage);

            if (!_isReturning && _currentIndex < _targets.Count)
            {
                Transform target = _targets[_currentIndex];
                
                if (target == null || other.transform == target || other.transform.IsChildOf(target))
                    _currentIndex++;
            }
        }
    }
}