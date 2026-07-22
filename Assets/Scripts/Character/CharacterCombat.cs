using System;
using Context;
using Database.Weapon;
using UnityEngine;
using Weapons;

namespace Character
{
    public class CharacterCombat : MonoBehaviour
    {
        [SerializeField] private CharacterBrain brain;
        [SerializeField] private WeaponBase weapon;
        private float _currentManualAttackTime = 0;
        private float _lastManualAttackTime = 0;

        private void Start()
        {
            weapon.Data = brain.GetWeaponData();
        }

        public bool ManualAttack()
        {
            if (CanManualAttack())
            {
                _lastManualAttackTime = _currentManualAttackTime;
                return true;
            }
            return false;
        }

        private void AutoAttack()
        {
            
        }

        public void WeaponPlay()
        {
            AttackContext ctx = new AttackContext()
            {
                Attack = brain.GetRuntimeStats().Attack,
                Luck = brain.GetRuntimeStats().Luck,
                Level = brain.GetRuntimeStats().Level,
            };
            weapon.Attack(ctx, null);
        }

        private bool CanManualAttack()
        {
            _currentManualAttackTime = Time.time;
            return _currentManualAttackTime - _lastManualAttackTime >= weapon.Data.AttackInterval;
        }
    }
}