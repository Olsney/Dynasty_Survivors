using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Elements
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField]
        private Image _currentHealth;

        public void SetValue(float current, float max)
        {
            float target = current / max;
            
            if (_currentHealth != null)
                _currentHealth.DOFillAmount(target, 0.1f).SetEase(Ease.Linear);
        }
    }
}