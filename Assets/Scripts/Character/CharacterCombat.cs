using System;
using System.Collections;
using Context;
using Database.Weapon;
using Enums;
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
        [SerializeField] private CombatState state = CombatState.Ready;
        private Coroutine _attackRoutine;

        private void Start()
        {
            weapon.Data = brain.GetWeaponData();
        }

        public bool ManualAttack()
        {
            if (CanManualAttack())
            {
                _lastManualAttackTime = _currentManualAttackTime;
                state = CombatState.Attacking;
                _attackRoutine = StartCoroutine(AttackTimeout());
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

        public void Stop()
        {
            
        }

        IEnumerator AttackTimeout()
        {
            yield return new WaitForSeconds(AttackTime());
            ForceAttackEnd();
        }

        public void ForceAttackEnd()
        {
            state = CombatState.Ready;
            if (_attackRoutine != null)
            {
                StopCoroutine(_attackRoutine);
            }
            _attackRoutine = null;
        }

        private bool CanManualAttack()
        {
            /*_currentManualAttackTime = Time.time;
            return _currentManualAttackTime - _lastManualAttackTime >= AttackTime();*/
            return state == CombatState.Ready;
        }

        private float AttackTime()
        {
            return weapon.Data.AttackClip.length / brain.GetRuntimeStats().AttackSpeed;
        }
    }
}