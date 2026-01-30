using Code.Data;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Elements
{
    public class ExpirienceProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _progressImage;
        [SerializeField] private TextMeshProUGUI _levelText;

        private ExperienceData _experience;

        public void Construct(ExperienceData experience)
        {
            _experience = experience;
            _experience.Changed += UpdateView;
            UpdateView();
        }

        private void OnDestroy()
        {
            if (_experience != null)
                _experience.Changed -= UpdateView;
        }

        private void UpdateView()
        {
            float target = (float)_experience.CurrentExp / ExperienceData.ExpPerLevel;
            
            if (_progressImage != null)
                _progressImage.DOFillAmount(target, 0.3f).SetEase(Ease.Linear);   
            
            if (_levelText != null)
                _levelText.text = _experience.Level.ToString();
        }
    }
}
