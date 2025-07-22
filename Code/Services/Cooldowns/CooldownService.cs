﻿namespace Code.Services.Cooldowns
{
    public class CooldownService : ICooldownService
    {
        private float _delayDuration;
        private float _timeLeft;

        public CooldownService(float delayDuration)
        {
            _delayDuration = delayDuration;
        }

        public bool IsReady => _timeLeft <= 0f;
        public float TimeLeft => _timeLeft;
        public float DelayDuration => _delayDuration;

        public void Tick(float deltaTime)
        {
            if (IsOnCooldown())
                _timeLeft -= deltaTime;
        }

        public bool IsOnCooldown() => 
            _timeLeft > 0f;

        public void PutOnCooldown()
        {
            _timeLeft = _delayDuration;
        }

        public void SetCooldown(float newDelay)
        {
            _delayDuration = newDelay;
            _timeLeft = _delayDuration;
        }
    }
}