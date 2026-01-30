using Code.Data;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Code.UI.Elements
{
  public class LootCounterView : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _counter;

    private WorldData _worldData;
    private int _currentValue;

    public void Construct(WorldData worldData)
    {
      _worldData = worldData;
      _worldData.LootData.Changed += UpdateCounter;
    }

    private void Start()
    {
      UpdateCounter();
    }

    private void UpdateCounter()
    {
      int target = _worldData.LootData.LootValue;

      DOTween.To(() => _currentValue, x =>
      {
        _currentValue = x;
        _counter.text = x.ToString();
      }, target, 0.3f).SetEase(Ease.OutQuad);
    }
  }
}