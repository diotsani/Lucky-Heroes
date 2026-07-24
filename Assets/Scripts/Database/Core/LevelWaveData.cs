using Enums;
using UnityEngine;

namespace Database.Core
{
    [CreateAssetMenu(fileName = "LevelWaveData", menuName = "Database/Core/LevelWaveData")]
    public class LevelWaveData : ScriptableObject
    {
        [SerializeField] private string levelName;
        [SerializeField] private WaveData[] waves;
        
        public WaveData GetWaveData(int i) => waves[i];
        
        //public EnemyType GetEnemyType(int i) => waves[i].GetEnemyType();
        
        public int TotalWave => waves.Length;
    }
}