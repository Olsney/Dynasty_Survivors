using Code.Services.Kill;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.UI.Elements
{
  public class KillCounterView : MonoBehaviour
  {
    private IKillsHandlerService _killsHandlerService;
    
    [SerializeField] private TextMeshProUGUI _counter;
    private float _currentValue;
    private Vector3 _defaultScale;
    private Sequence _refreshSequence;

    [Inject]
    public void Construct(IKillsHandlerService killsHandlerService)
    {
      _killsHandlerService = killsHandlerService;
    }
    
    private void Awake()
    {
      _defaultScale = _counter.transform.localScale;

      _killsHandlerService.KillCountChanged += Refresh;
    }

    private void OnDestroy()
    {
      _refreshSequence?.Kill();
      
      _killsHandlerService.KillCountChanged -= Refresh;
    }

    private void Start()
    {
      Refresh();
    }

    public void Refresh()
    {
      int targetValue = _killsHandlerService.KillsCount;

      _refreshSequence?.Kill();

      Tween countTween = DOTween.To(
        () => _currentValue,
        x =>
        {
          _currentValue = x;
          _counter.text = Mathf.RoundToInt(_currentValue).ToString();
        },
        targetValue,
        0.3f).SetEase(Ease.OutQuad);

      _refreshSequence = DOTween.Sequence();
      _refreshSequence.Append(_counter.transform.DOScale(_defaultScale * 1.2f, 0.15f).SetEase(Ease.OutBack));
      _refreshSequence.Insert(0, countTween);
      _refreshSequence.Append(_counter.transform.DOScale(_defaultScale, 0.15f).SetEase(Ease.OutBack));
    }
  }
}
