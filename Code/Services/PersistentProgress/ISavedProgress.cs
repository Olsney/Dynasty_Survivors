using Code.Data;

namespace Code.Services.PersistentProgress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        public void UpdateProgress(PlayerProgress progress);
    }
}