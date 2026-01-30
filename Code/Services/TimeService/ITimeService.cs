using System;

namespace Code.Services.TimeService
{
    public interface ITimeService
    {
        float TimeMultiplier { get; set; }
        float DeltaTime { get; }
        DateTime UtcNow { get; }
        bool IsPaused { get; }
        void Pause();
        void UnPause();
    }
}