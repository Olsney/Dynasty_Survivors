using System;
using Code.Services.PersistentProgress;

namespace Code.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public HealthData HeroHealth;
        public HeroStats HeroStats;
        public ExperienceData Experience;
        public WorldData WorldData;
        public AbilitiesData Abilities;

        public PlayerProgress(string initialLevel, IPersistentProgressService progressService)
        {
            WorldData = new WorldData(initialLevel);
            HeroHealth = new HealthData();
            HeroStats = new HeroStats();
            Experience = new ExperienceData(progressService);
            Abilities = new AbilitiesData();
        }
    }
}