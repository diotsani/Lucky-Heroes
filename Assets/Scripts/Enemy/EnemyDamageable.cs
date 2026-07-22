using System;
using Database;
using Interfaces;
using UnityEngine;

namespace Enemy
{
    public class EnemyDamageable : MonoBehaviour, IDamageable
    {
        [SerializeField] private EnemyBrain brain;

        public Action<float> OnTakeDamage { get; set; }

        public void Death()
        {
            
        }

        public RuntimeStats GetRuntimeStats()
        {
            return brain.GetRuntimeStats();
        }
    }
}