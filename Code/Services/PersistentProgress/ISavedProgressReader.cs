using Code.Data;

namespace Code.Services.PersistentProgress
{
    public interface ISavedProgressReader
    {
        public void LoadProgress(PlayerProgress progress);
    }
}