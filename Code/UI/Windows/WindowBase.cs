using Code.Data;
using Code.Services.PersistentProgress;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        protected IPersistentProgressService ProgressService;

        [SerializeField] private Button _closeButton;
        protected PlayerProgress Progress => ProgressService.Progress;

        private Tween _scaleTween;

        [Inject]
        public void Construct(IPersistentProgressService progressService)
        {
            ProgressService = progressService;
        }

        private void Awake() => 
            OnAwake();

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy()
        {
            Cleanup();

            _scaleTween?.Kill();
        }

        protected virtual void OnAwake()
        {
            if (_closeButton != null)
                _closeButton.onClick.AddListener(() => OnCloseButtonClicked());

            transform.localScale = Vector3.zero;

            _scaleTween = transform
                .DOScale(1f, 0.3f)
                .SetEase(Ease.OutBack)
                .SetUpdate(true);  
        }

        protected virtual void OnCloseButtonClicked()
        {
            if (_closeButton != null)
                _closeButton.interactable = false;

            _scaleTween = transform
                .DOScale(0f, 0.25f)
                .SetEase(Ease.InBack)
                .SetUpdate(true)
                .OnComplete(() => Destroy(gameObject));
        }

        protected virtual void Initialize() { }

        protected virtual void SubscribeUpdates() { }

        protected virtual void Cleanup() { }
    }
}