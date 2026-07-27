using System;
using Database;
using UnityEngine;

namespace Interfaces
{
    public interface IDamageable
    {
        public Action<float> OnTakeDamage { get; set; }

        void TakeDamage(float damage)
        {
            //Debug.Log($"Take Damage {damage}");
            OnTakeDamage?.Invoke(damage);
        }
        
        void Death();
        
        public RuntimeStats GetRuntimeStats();
    }
}