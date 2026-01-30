using System;

namespace Code.Services.TimeService
{
    public class UnityTimeService : ITimeService
    {
        private int _pauseRequests;
        private float _storedTimeScale = 1f;
        
        public float TimeMultiplier { set; get; } = 1f;
        public bool IsPaused => _pauseRequests > 0;

        public float DeltaTime =>
            !IsPaused
                ? UnityEngine.Time.deltaTime * TimeMultiplier
                : 0;

        public DateTime UtcNow => DateTime.UtcNow;

        public void Pause()
        {
            if (_pauseRequests == 0)
                _storedTimeScale = UnityEngine.Time.timeScale;

            _pauseRequests++;

            UnityEngine.Time.timeScale = 0f;
        }

        public void UnPause()
        {
            if (_pauseRequests == 0)
                return;

            _pauseRequests--;

            if (_pauseRequests == 0)
                UnityEngine.Time.timeScale = _storedTimeScale;
        }
    }
}
