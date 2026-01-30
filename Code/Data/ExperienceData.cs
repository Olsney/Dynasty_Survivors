using System;
using Code.Services.PersistentProgress;

namespace Code.Data
{
    [Serializable]
    public class ExperienceData
    {
        public const int ExpPerLevel = 500;
        
        private IPersistentProgressService _progressService;
        
        public int CurrentExp;
        public int Level = 1;
        public Action Changed;

        public ExperienceData(IPersistentProgressService progressService)
        {
            _progressService = progressService;
        }
        
        public void Construct(IPersistentProgressService progressService) =>
            _progressService = progressService;


        public void AddExp(int amount)
        {
            CurrentExp += amount;
            
            while (CurrentExp >= ExpPerLevel)
            {
                CurrentExp -= ExpPerLevel;
                Level++;
                _progressService.InvokeLevelUp();
            }
            
            Changed?.Invoke();
        }
    }
}
