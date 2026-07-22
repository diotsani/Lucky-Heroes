using System;
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
        
        public Action OnDeath { get; set; }

        private void Start()
        {
            InitializeStats();
        }

        public void InitializeStats()
        {
            var baseStats = brain.GetStats();
            var weapon = brain.GetWeaponData();
            RuntimeStats = new RuntimeStats
            {
                Level = 1,
                Attack = baseStats.Attack + weapon.Attack,
                MaxAttack = baseStats.Attack + weapon.Attack,
                Health = baseStats.Health,
                MaxHealth = baseStats.Health,
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
                OnDeath?.Invoke();
            }
        }
    }
}