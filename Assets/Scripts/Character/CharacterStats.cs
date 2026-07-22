using System;
using Database;
using Interfaces;
using UnityEngine;

namespace Character
{
    public class CharacterStats : MonoBehaviour, IStats
    {
        [SerializeField] private CharacterBrain brain;
        
        public RuntimeStats RuntimeStats { get; private set; }
        
        public Action OnDeath { get; set; }

        private void Start()
        {
            InitializeStats();
        }

        private void Update()
        {
            RegenMana();
            RegenStamina();
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
                Stamina = baseStats.Stamina,
                MaxStamina = baseStats.Stamina,
                Luck = baseStats.Luck
            };
        }

        private const float RegenRate = 10;
        private void RegenMana()
        {
            if(RuntimeStats.ManaPercent >= 1)return;
            RuntimeStats.Mana = Mathf.Clamp(RuntimeStats.Mana + RegenRate * Time.deltaTime, 
                0, 
                RuntimeStats.MaxMana);
        }

        
        private void RegenStamina()
        {
            if(RuntimeStats.StaminaPercent >= 1)return;
            
            RuntimeStats.Stamina = Mathf.Clamp(RuntimeStats.Stamina + RegenRate * Time.deltaTime, 
                0, 
                RuntimeStats.MaxStamina);
        }

        public void GainExperience(float experience)
        {
            RuntimeStats.Experience += experience;
        }

        public void GainLuck(int luck)
        {
            RuntimeStats.Luck += luck;
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