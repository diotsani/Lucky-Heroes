using System;
using Database;
using Database.Upgrade;
using Enums;
using Interfaces;
using UnityEngine;

namespace Character
{
    public class CharacterStats : MonoBehaviour, IStats
    {
        [SerializeField] private CharacterBrain brain;
        private const float RegenRate = 10;
        public RuntimeStats RuntimeStats { get; private set; }
        public Action<float> OnHealthChanged { get; set; }
        public Action<float> OnGainASpd { get; set; }

        private void Awake()
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
                Level = 0,
                Attack = baseStats.Attack + weapon.Attack,
                MaxAttack = baseStats.Attack + weapon.Attack,
                Health = baseStats.Health,
                MaxHealth = baseStats.Health,
                Mana = baseStats.Mana,
                MaxMana = baseStats.Mana,
                Stamina = baseStats.Stamina,
                MaxStamina = baseStats.Stamina,
                BaseAttackSpeed = baseStats.AttackSpeed,
                AttackSpeed = baseStats.AttackSpeed,
                Luck = baseStats.Luck
            };
        }

        private void GainAttack(float amount)
        {
            RuntimeStats.Attack += amount;
        }

        private void GainHealth(float amount)
        {
            float percentage = RuntimeStats.HealthPercent;
            RuntimeStats.MaxHealth += amount;
            RuntimeStats.Health = RuntimeStats.MaxHealth * percentage;
        }

        private void GainMana(float amount)
        {
            float percentage = RuntimeStats.ManaPercent;
            RuntimeStats.MaxMana += amount;
            RuntimeStats.Mana = RuntimeStats.MaxMana * percentage;
        }

        private void GainStamina(float amount)
        {
            float percentage = RuntimeStats.StaminaPercent;
            RuntimeStats.MaxStamina += amount;
            RuntimeStats.Stamina = RuntimeStats.MaxStamina * percentage;
        }

        private void GainLuck(int amount)
        {
            RuntimeStats.Luck += amount;
        }

        private void GainASpd(float amount)
        {
            // value in percentage
            // SPD = 1, Multiplier = 1 > low multiplier, high spd 
            RuntimeStats.AttackSpeed += amount;
            OnGainASpd?.Invoke(RuntimeStats.AttackSpeed);
        }
        
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

        public void ReduceHealth(float amount)
        {
            RuntimeStats.Health -= amount;
            OnHealthChanged?.Invoke(RuntimeStats.HealthPercent);
            if (RuntimeStats.Health <= 0)
            {
                RuntimeStats.Health = 0;
                brain.OnDeath?.Invoke();
            }
        }
        
        public void Upgrade(UpgradeData data)
        {
            UpgradeValueType type = data.upgradeValueType;
            float value = data.upgradeValue;
            switch (data.upgradeStat)
            {
                case StatType.Attack:
                    GainAttack(FinalUpgradeValue(type, value, RuntimeStats.Attack));
                    break;
                case StatType.Health:
                    GainHealth(FinalUpgradeValue(type, value, RuntimeStats.MaxHealth));
                    break;
                case StatType.Mana:
                    GainMana(FinalUpgradeValue(type, value, RuntimeStats.MaxMana));
                    break;
                case StatType.Stamina:
                    GainStamina(FinalUpgradeValue(type, value, RuntimeStats.MaxStamina));
                    break;
                case StatType.AttackSpeed:
                    GainASpd(FinalUpgradeValue(type, value ,RuntimeStats.BaseAttackSpeed));
                    break;
                case StatType.Luck:
                    GainLuck((int)FinalUpgradeValue(type, value, RuntimeStats.Luck));
                    break;
            }
        }

        private float FinalUpgradeValue(UpgradeValueType valueType, float value, float baseValue)
        {
            return valueType switch
            {
                UpgradeValueType.Flat => value,
                UpgradeValueType.Percentage => (baseValue * value / 100),
                _ => value
            };
        }

        public float GetValue(StatType type)
        {
            return type switch
            {
                StatType.Attack => RuntimeStats.Attack,
                StatType.Health => RuntimeStats.MaxHealth,
                StatType.Mana => RuntimeStats.MaxMana,
                StatType.Stamina => RuntimeStats.MaxStamina,
                StatType.AttackSpeed => RuntimeStats.AttackSpeed,
                StatType.Speed => RuntimeStats.Speed,
                StatType.Luck => RuntimeStats.Luck,
                StatType.Level => RuntimeStats.Level,
                _ => 0
            };
        }
    }
}