using System;
using Code.Data;
using Code.Services.PersistentProgress;
using Code.Services.TimeService;
using Zenject;

namespace Code.Services.PlayTime
{
  public class CurrentSessionPlayTimeService : ICurrentSessionPlayTimeService, ITickable, ISavedProgressReader
  {
    private const string MinuteFormat = @"mm\:ss";
    
    private readonly ITimeService _timeService;

    private PlayTimeData _playTimeData;
    private bool _isStopped;

    public event Action<float> TimeChanged;

    public float PlayTimeSeconds => _playTimeData?.Seconds ?? 0f;

    public CurrentSessionPlayTimeService(ITimeService timeService)
    {
      _timeService = timeService;
    }

    public string GetPlayTime()
    {
      TimeSpan time = TimeSpan.FromSeconds(_playTimeData.Seconds);
      return time.ToString(MinuteFormat);
    }

    public void LoadProgress(PlayerProgress progress)
    {
      progress.WorldData.PlayTimeData ??= new PlayTimeData();
      _playTimeData = progress.WorldData.PlayTimeData;
      _isStopped = false;
    }

    public void Tick()
    {
      if (_isStopped)
        return;

      if (_playTimeData == null)
        return;

      if (_timeService.DeltaTime <= 0f)
        return;
      
      _playTimeData.Add(_timeService.DeltaTime);
      
      TimeChanged?.Invoke(_playTimeData.Seconds);
    }

    public void Stop()
    {
      _isStopped = true;
    }
  }
}
