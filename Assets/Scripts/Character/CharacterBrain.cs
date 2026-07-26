using System;
using Database;
using Database.Character;
using Database.Upgrade;
using Database.Weapon;
using Entity;
using Enums;
using StateMachines;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character
{
    [RequireComponent(typeof(CharacterInput), typeof(CharacterMotor), typeof(EntityAnimator))]
    public class CharacterBrain : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private CharacterData data;
        [Header("Controller")]
        [SerializeField] private CharacterStats stats;
        [SerializeField] private CharacterLevel level;
        [SerializeField] private CharacterResources resources;
        [SerializeField] private CharacterInput input;
        [SerializeField] private CharacterMotor motor;
        [SerializeField] private CharacterCombat combat;
        [SerializeField] private CharacterSkill skill;
        [SerializeField] private CharacterDamageable damageable;
        [SerializeField] private EntityAnimator animator;
        public CharacterStats Stats => stats;
        public CharacterLevel Level => level;
        public CharacterResources Resources => resources;
        public CharacterInput Input => input;
        public CharacterMotor Motor => motor;
        public CharacterCombat Combat => combat;
        public CharacterSkill Skill => skill;
        public EntityAnimator Animator => animator;

        private CharacterStateType _stateType;
        private StateMachine StateMachine { get; set; }
        
        private void Awake()
        {
            Services.Services.Register(this);
            StateMachine = new StateMachine();
            ChangeState(CharacterStateType.Idle);
        }

        public void ChangeState(CharacterStateType type)
        {
            _stateType = type;
            switch (_stateType)
            {
                case CharacterStateType.Idle:
                    StateMachine.ChangeState(new IdleState(this, StateMachine));
                    break;
                case CharacterStateType.Walk:
                    StateMachine.ChangeState(new WalkState(this, StateMachine));
                    break;
                case CharacterStateType.Run:
                    StateMachine.ChangeState(new RunState(this, StateMachine));
                    break;
                case CharacterStateType.Dead:
                    StateMachine.ChangeState(new DeadState(this, StateMachine));
                    break;
                default:
                    StateMachine.ChangeState(new IdleState(this, StateMachine));
                    break;
            }
        }

        private void OnEnable()
        {
            stats.OnDeath += OnDeath;
            stats.OnGainASpd += OnGainASpd;
            input.OnAttacked += OnAttacked;
            damageable.OnTakeDamage += OnTakeDamage;
            animator.WeaponEvent.PlayAction += combat.WeaponPlay;
            animator.WeaponEvent.StopAction += OnWeaponStop;
            animator.WeaponEvent.EndAction += OnWeaponEnd;
        }

        private void OnDisable()
        {
            stats.OnDeath -= OnDeath;
            stats.OnGainASpd -= OnGainASpd;
            input.OnAttacked -= OnAttacked;
            damageable.OnTakeDamage -= OnTakeDamage;
            animator.WeaponEvent.PlayAction -= combat.WeaponPlay;
            animator.WeaponEvent.StopAction -= OnWeaponStop;
            animator.WeaponEvent.EndAction -= OnWeaponEnd;
        }

        private void Update()
        {
            StateMachine.Update();
        }

        #region Actions
        private void OnDeath()
        {
            ChangeState(CharacterStateType.Dead);
        }

        private void OnGainASpd(float amount)
        {
            animator.UpdateASpdValue(amount);
        }
        
        private void OnAttacked()
        {
            if (combat.ManualAttack())
            {
                Animator.PlayAnimation(EntityAnimationType.Attack);
                Animator.Hold();
            }
        }

        private void OnTakeDamage(float damage)
        {
            stats.ReduceHealth(damage);
        }
        
        private void OnWeaponStop()
        {
            
        }
        
        private void OnWeaponEnd()
        {
            animator.Continue(_stateType);
            combat.WeaponEnd();
        }
        #endregion

        #region Controller
        public bool InputMoving()
        {
            return input.Moving();
        }

        public bool InputRunning()
        {
            return input.Running;
        }
        
        public void MotorMove()
        {
            motor.Move(input.Move, input.Running);
        }
        #endregion

        #region Data
        public Stats GetStats()
        {
            return data.EntityStats;
        }
        
        public RuntimeStats GetRuntimeStats()
        {
            return stats.RuntimeStats;
        }

        public WeaponData GetWeaponData()
        {
            return data.WeaponData;
        }
        #endregion
        
    }
}

