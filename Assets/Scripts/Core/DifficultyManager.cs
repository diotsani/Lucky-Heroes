using UnityEngine;

namespace Core
{
    public class DifficultyManager : MonoBehaviour
    {
        [SerializeField] private GameManager game;
        
        [Header("Progress")]
        [SerializeField] private float difficultyRampTime = 600f;
        
        [Header("Spawn")]
        [SerializeField] private float startSpawnInterval = 2f;
        [SerializeField] private float endSpawnInterval = 0.2f;
        
        [SerializeField] private int startSpawnCount = 2;
        private int _maxSpawnCount;
        
        [Header("Enemy")]
        [SerializeField] private float startHealMultiplier = 1f;
        [SerializeField] private float endHealMultiplier = 4f;
        
        [SerializeField] private float startDamageMultiplier = 1f;
        [SerializeField] private float endDamageMultiplier = 4f;
        
        private DifficultyData _difficultyData;
        public DifficultyData Data => _difficultyData;

        public void Setup()
        {
            _difficultyData = new DifficultyData();
        }

        public void RefreshDifficultyData(float elapsedTime)
        {
            float progress = Mathf.Clamp01(elapsedTime / difficultyRampTime);
            _maxSpawnCount = Mathf.Clamp((int)elapsedTime, (int)startSpawnCount, (int)elapsedTime);
            
            _difficultyData.SpawnInterval = Mathf.Lerp(startSpawnInterval, endSpawnInterval, progress);
            _difficultyData.SpawnCount = Mathf.RoundToInt(Mathf.Lerp(startSpawnCount, _maxSpawnCount, progress));
            
            _difficultyData.EnemyHealthMultiplier = Mathf.Lerp(startHealMultiplier, endHealMultiplier, progress);
            _difficultyData.EnemyDamageMultiplier = Mathf.Lerp(startDamageMultiplier, endDamageMultiplier, progress);
        }

        public float RandomMultiplier()
        {
            float rnd = 0.2f;
            return 1 + Random.Range(-rnd, rnd);
        }
    }

    public struct DifficultyData
    {
        public float SpawnInterval;
        public int SpawnCount;
        public float EnemyHealthMultiplier;
        public float EnemyDamageMultiplier;
    }
}