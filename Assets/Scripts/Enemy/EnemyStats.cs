using System;
using Core;
using Database;
using Interfaces;
using UnityEngine;

namespace Enemy
{
    public class EnemyStats : MonoBehaviour, IStats
    {
        [SerializeField] private EnemyBrain brain;
        public RuntimeStats RuntimeStats { get; private set; }
        
        public RuntimeStats GetRuntimeStats => RuntimeStats;

        public void InitializeStats()
        {
            
        }

        public void InitializeStats(DifficultyData diffData)
        {
            var baseStats = brain.GetStats();
            var weapon = brain.GetWeaponData();
            RuntimeStats = new RuntimeStats
            {
                Level = 0,
                Attack = (baseStats.Attack + weapon.Attack) * diffData.EnemyDamageMultiplier,
                Health = baseStats.Health * diffData.EnemyHealthMultiplier,
                Mana = baseStats.Mana,
                MaxMana = baseStats.Mana,
                /*Stamina = baseStats.Stamina,
                MaxStamina = baseStats.Stamina,
                Luck = baseStats.Luck*/
            };
        }

        public void ReduceHealth(float amount)
        {
            RuntimeStats.Health -= amount;
            if (RuntimeStats.Health <= 0)
            {
                RuntimeStats.Health = 0;
                brain.OnDeath?.Invoke();
            }
        }
    }
}