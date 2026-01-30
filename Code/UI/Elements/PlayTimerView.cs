using System;
using Code.Services.PlayTime;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.UI.Elements
{
  public class PlayTimerView : MonoBehaviour
  {
    private const string MinuteFormat = @"mm\:ss";
    
    [SerializeField] private TextMeshProUGUI _time;
    
    private ICurrentSessionPlayTimeService _currentSessionPlayTimeService;

    [Inject]
    public void Construct(ICurrentSessionPlayTimeService currentSessionPlayTimeService)
    {
      _currentSessionPlayTimeService = currentSessionPlayTimeService;
    }

    private void Start()
    {
      _currentSessionPlayTimeService.TimeChanged += OnTimeChanged;
      
      OnTimeChanged(_currentSessionPlayTimeService.PlayTimeSeconds);
    }

    private void OnDestroy()
    {
      _currentSessionPlayTimeService.TimeChanged -= OnTimeChanged;
    }

    private void OnTimeChanged(float totalSeconds)
    {
      TimeSpan time = TimeSpan.FromSeconds(totalSeconds);
      _time.text = time.ToString(MinuteFormat);
    }
  }
}
