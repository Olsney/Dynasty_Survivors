using System;
using Code.Data;

namespace Code.Services.PersistentProgress
{
    public interface IPersistentProgressService
    {
        PlayerProgress Progress { get; set; }
        event Action LevelUp;
        void InvokeLevelUp();
    }
}
