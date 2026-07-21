using System;
using Database.Character;
using Database.Weapon;
using Enums;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterInput), typeof(CharacterMotor), typeof(CharacterAnimator))]
    public class CharacterBrain : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private CharacterData data;
        [Header("Controller")]
        [SerializeField] private CharacterStats stats;
        [SerializeField] private CharacterInput input;
        [SerializeField] private CharacterMotor motor;
        [SerializeField] private CharacterCombat combat;
        [SerializeField] private CharacterSkill skill;
        [SerializeField] private CharacterAnimator animator;

        private CharacterStateType _stateType;
        private CharacterStateMachine StateMachine { get; set; }
        
        private void Awake()
        {
            StateMachine = new CharacterStateMachine(this);
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
            input.OnAttacked += OnAttacked;
            animator.WeaponEvent.PlayAction += OnWeaponPlay;
            animator.WeaponEvent.StopAction += OnWeaponStop;
            animator.WeaponEvent.EndAction += OnWeaponEnd;
        }

        private void OnDisable()
        {
            input.OnAttacked -= OnAttacked;
            animator.WeaponEvent.PlayAction -= OnWeaponPlay;
            animator.WeaponEvent.StopAction -= OnWeaponStop;
            animator.WeaponEvent.EndAction -= OnWeaponEnd;
        }

        private void Update()
        {
            StateMachine.Update();
        }

        #region Actions
        private void OnAttacked()
        {
            if (combat.ManualAttack())
            {
                AnimatorPlayAnimation(CharacterAnimationType.Attack);
                animator.Hold();
            }
        }

        private void OnWeaponPlay()
        {
            combat.WeaponPlay();
        }
        
        private void OnWeaponStop()
        {
            
        }
        
        private void OnWeaponEnd()
        {
            animator.Continue(_stateType);
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

        public void InputStop()
        {
            input.Stop();
        }
        
        public void MotorMove()
        {
            motor.Move(input.Move, input.Running);
        }

        public void MotorStop()
        {
            motor.Stop();
        }
        
        public void AnimatorPlayAnimation(CharacterAnimationType animationType)
        {
            animator.PlayAnimation(animationType);
        }

        public void CombatStop()
        {
            
        }

        public void SkillStop()
        {
            
        }
        #endregion

        #region Data
        public Stats GetStats()
        {
            return data.CharacterStats;
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

