using System;

namespace Code.Data
{
    [Serializable]
    public class ExperienceData
    {
        public const int ExpPerLevel = 500;
        public int CurrentExp;
        public int Level = 1;
        public Action Changed;

        public void AddExp(int amount)
        {
            CurrentExp += amount;
            
            while (CurrentExp >= ExpPerLevel)
            {
                CurrentExp -= ExpPerLevel;
                Level++;
            }
            
            
            Changed?.Invoke();
        }
    }
}
