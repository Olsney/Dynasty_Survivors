using Code.Data;
using Code.Services.PersistentProgress;
using UnityEngine;

namespace Code.Enemy
{
    public class ExperienceReward : MonoBehaviour
    {
        private const int RewardAmount = 100;
        
        [SerializeField] private EnemyDeath _enemyDeath;

        private ExperienceData _experience;

        public void Construct(IPersistentProgressService progressService)
        {
            _experience = progressService.Progress.Experience;
        }

        private void Start() =>
            _enemyDeath.Died += OnEnemyDied;

        private void OnDestroy() =>
            _enemyDeath.Died -= OnEnemyDied;

        private void OnEnemyDied() =>
            _experience.AddExp(RewardAmount);
    }
}
