using Code.Logic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        [FormerlySerializedAs("healthBarView")] [FormerlySerializedAs("_healthBar")] [SerializeField] private HealthBarView _healthBarView;

        private IHealth _heroHealth;

        public void Construct(IHealth health)
        {
            _heroHealth = health;

            _heroHealth.Changed += OnHealthChanged;
            OnHealthChanged();
        }
        
        private void Start()
        {
            IHealth health = GetComponent<IHealth>();

            if (health != null)
                Construct(health);
        }

        private void OnHealthChanged() => 
            _healthBarView.SetValue(_heroHealth.Current, _heroHealth.Max);

        private void OnDestroy()
        {
            _heroHealth.Changed -= OnHealthChanged;
        }
    }
}