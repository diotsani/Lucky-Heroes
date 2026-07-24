using System;
using System.Linq;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Database.Core
{
    [CreateAssetMenu(fileName = "WaveData", menuName = "Database/Core/WaveData")]
    public class WaveData : ScriptableObject
    {
        [SerializeField] private string waveName;
        [SerializeField] private int totalWeight;
        [SerializeField] Wave[] waves;
        [SerializeField] private int duration = 60;
        [SerializeField] private float interval = 0.6f;
        [SerializeField] private int maxEnemies = 50;
        
        public int Duration => duration;
        
        public float Interval => interval;
        
        public int MaxEnemies => maxEnemies;

        private void Refresh()
        {
            totalWeight = waves.Sum(w=> w.weight);
            foreach (var wave in waves)
            {
                wave.percent = (float)(wave.weight / (float)totalWeight * 100f);
                wave.percentage = $"{wave.percent:F1}%";
            }
        }
        
        public EnemyType GetEnemyType()
        {
            int random = Random.Range(1, totalWeight + 1);
            foreach (var wave in waves)
            {
                if (random < wave.weight) return wave.enemyType;
                random -= wave.weight;
            }

            return waves[^1].enemyType;
        }

#if UNITY_EDITOR
        [SerializeField] private bool isValidate = false;
        private void OnValidate()
        {
            if(!isValidate)return;
            if(waves == null)return;

            Refresh();
            
            isValidate = false;
        }
#endif
    }

    [Serializable]
    public class Wave
    {
        public EnemyType enemyType;
        [Range(1,100)] public int weight;
        [HideInInspector] public float percent;
        public string percentage;
    }
}