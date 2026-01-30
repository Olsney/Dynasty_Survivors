using System;
using Code.Data;

namespace Code.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        private PlayerProgress _progress;
        
        public event Action LevelUp;

        public PlayerProgress Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                _progress?.Experience.Construct(this);
            }
        }
 
        public void InvokeLevelUp() => 
            LevelUp?.Invoke();
    }
}
