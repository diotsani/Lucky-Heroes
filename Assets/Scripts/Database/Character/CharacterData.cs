using System;
using Database.Weapon;
using UnityEngine;

namespace Database.Character
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Database/CharacterData", order = 1)]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private string characterName;
        [SerializeField] private Stats characterStats;
        [SerializeField] private WeaponData weapon;
        
        public Stats CharacterStats => characterStats;

        public WeaponData WeaponData => weapon;
    }

    [Serializable]
    public class Stats
    {
        public float Attack;
        public float Health;
        public float Defense;
        public float Mana;
        public float Stamina;
        public float Luck;
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
        public float Defense;
        public float MaxDefense;
        public float Mana;
        public float MaxMana;
        public float Stamina;
        public float MaxStamina;
        public float Luck;
    }
}