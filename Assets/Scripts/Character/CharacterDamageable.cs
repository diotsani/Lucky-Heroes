using Database.Character;
using Interfaces;
using UnityEngine;

namespace Character
{
    public class CharacterDamageable : MonoBehaviour, IDamageable
    {
        [SerializeField] private CharacterBrain brain;
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