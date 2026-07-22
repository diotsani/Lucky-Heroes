using System;
using Database.Weapon;
using UnityEngine;

namespace Database
{
    public class EntityData : ScriptableObject
    {
        [SerializeField] private string entityName;
        [SerializeField] private Stats entityStats;
        [SerializeField] private WeaponData weapon;
        
        public Stats EntityStats => entityStats;

        public WeaponData WeaponData => weapon;
    }
    
    [Serializable]
    public class Stats
    {
        public float Attack;
        public float Health;
        public float Mana;
        public float Stamina;
        public float AttackSpeed;
        public int Luck;
        public float WalkSpeed;
        public float RunSpeed;
    }
    
    [Serializable]
    public class RuntimeStats
    {
        public int Level;
        public float Experience;
        public float Attack;
        public float MaxAttack;
        public float Health;
        public float MaxHealth;
        public float Mana;
        public float MaxMana;
        public float Stamina;
        public float MaxStamina;
        public float AttackSpeed;
        public int Luck;

        public float AttackPercent => Attack / MaxAttack;
        public float HealthPercent => Health / MaxHealth;
        public float ManaPercent => Mana / MaxMana;
        public float StaminaPercent => Stamina / MaxStamina;
    }
}