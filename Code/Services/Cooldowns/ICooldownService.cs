﻿namespace Code.Services.Cooldowns
{
    public interface ICooldownService
    {
        bool IsReady { get; }
        float TimeLeft { get; }
        float DelayDuration { get; }
        void Tick(float deltaTime);
        void PutOnCooldown();
        void SetCooldown(float newDelay);
        bool IsOnCooldown();
    }
}