using System;
using Database;
using Interfaces;
using UnityEngine;

namespace Character
{
    public class CharacterDamageable : MonoBehaviour, IDamageable
    {
        [SerializeField] private CharacterBrain brain;
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