using Code.Data;
using Code.Infrastructure.Factory;
using Code.Services.PersistentProgress;
using UnityEngine;

namespace Code.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        
        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;
        
        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
                progressWriter.UpdateProgress(_progressService.Progress);

            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress()
        {
            string progressJson = PlayerPrefs.GetString(ProgressKey);

            if (string.IsNullOrEmpty(progressJson))
                return null;

            return progressJson.ToDeserialized<PlayerProgress>();
        }

        public bool HasProgress()
        {
            if (!PlayerPrefs.HasKey(ProgressKey))
                return false;

            return !string.IsNullOrEmpty(PlayerPrefs.GetString(ProgressKey));
        }
    }
}
