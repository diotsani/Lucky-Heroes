using Database.Character;
using Interfaces;
using UnityEngine;

namespace Enemy
{
    public class EnemyDamageable : MonoBehaviour, IDamageable
    {
        [SerializeField] private EnemyBrain brain;
        public void TakeDamage(float damage)
        {
            
        }

        public void Death()
        {
            
        }

        public RuntimeStats GetRuntimeStats()
        {
            return brain.GetRuntimeStats();
        }
    }
}