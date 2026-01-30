using System;
using Code.Infrastructure.Services;

namespace Code.Services.PlayTime
{
  public interface ICurrentSessionPlayTimeService : IService
  {
    event Action<float> TimeChanged;
    float PlayTimeSeconds { get; }
    void Stop();
    string GetPlayTime();
  }
}
