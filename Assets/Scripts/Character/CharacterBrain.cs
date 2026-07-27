using System;
using Core;
using Database;
using Database.Character;
using Database.Upgrade;
using Database.Weapon;
using Entity;
using Enums;
using Services;
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

        public Action OnDeath { get; set; }
        private CharacterStateType _stateType;
        private StateMachine StateMachine { get; set; }
        private IdleState _idleState;
        private WalkState _walkState;
        private RunState _runState;
        private StopState _stopState;
        
        private void Awake()
        {
            ServiceLocator.Register(this);
            StateMachine = new StateMachine();
            _idleState = new IdleState(this, StateMachine);
            _walkState = new WalkState(this, StateMachine);
            _runState = new RunState(this, StateMachine);
            _stopState = new StopState(this, StateMachine);
        }

        private void OnDestroy()
        {
            ServiceLocator.Unregister(this);
        }

        public void ChangeState(CharacterStateType type)
        {
            _stateType = type;
            switch (_stateType)
            {
                case CharacterStateType.Idle:
                    StateMachine.ChangeState(_idleState);
                    break;
                case CharacterStateType.Walk:
                    StateMachine.ChangeState(_walkState);
                    break;
                case CharacterStateType.Run:
                    StateMachine.ChangeState(_runState);
                    break;
                case CharacterStateType.Death:
                    StateMachine.ChangeState(new DeadState(this, StateMachine));
                    break;
                case CharacterStateType.Stop:
                    StateMachine.ChangeState(_stopState);
                    break;
                default:
                    StateMachine.ChangeState(_idleState);
                    break;
            }
        }

        private void OnEnable()
        {
            stats.OnGainASpd += OnGainASpd;
            input.OnAttacked += OnAttacked;
            damageable.OnTakeDamage += OnTakeDamage;
            animator.WeaponEvent.PlayAction += combat.WeaponPlay;
            //animator.WeaponEvent.StopAction += OnWeaponStop;
            animator.WeaponEvent.EndAction += combat.ForceAttackEnd;
        }

        private void OnDisable()
        {
            stats.OnGainASpd -= OnGainASpd;
            input.OnAttacked -= OnAttacked;
            damageable.OnTakeDamage -= OnTakeDamage;
            animator.WeaponEvent.PlayAction -= combat.WeaponPlay;
            //animator.WeaponEvent.StopAction -= OnWeaponStop;
            animator.WeaponEvent.EndAction -= combat.ForceAttackEnd;
        }

        private void Start()
        {
            motor.Initialize(this, Services.ServiceLocator.Get<GameManager>().Room.Arena);
        }

        private void Update()
        {
            StateMachine.Update();
        }

        #region Actions

        private void OnGainASpd(float amount)
        {
            animator.SetFloat(animator.ASpd, amount);
        }
        
        private void OnAttacked()
        {
            if (combat.ManualAttack())
            {
                Animator.SetTrigger(Animator.Attack);
            }
        }

        private void OnTakeDamage(float damage)
        {
            stats.ReduceHealth(damage);
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

