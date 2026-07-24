using System;
using Battlefield;
using Character;
using Database;
using Database.Enemy;
using Database.Weapon;
using Entity;
using Enums;
using StateMachines;
using UnityEngine;

namespace Enemy
{
    public class EnemyBrain : MonoBehaviour
    {
        [SerializeField] private EnemyType type;
        public EnemyType Type => type;
        [Header("Data")]
        [SerializeField] private EnemyData data;
        [Header("Controller")]
        [SerializeField] private EnemyStats stats;
        [SerializeField] private EnemyMotor motor;
        [SerializeField] private EnemyCombat combat;
        [SerializeField] private EnemyDamageable damageable;
        [SerializeField] private EntityAnimator animator;

        private Transform _player;
        private EnemyStateType _stateType;
        private StateMachine StateMachine { get; set; }
        
        private void Awake()
        {
            StateMachine = new StateMachine();
        }
        
        public void Initialize(Vector2 position)
        {
            // Temp
            if(_player == null)_player = GameObject.FindWithTag("Player").transform;
            
            transform.position = position;
            gameObject.SetActive(true);
            ChangeState(data.FirstState);
        }
        
        public void ChangeState(EnemyStateType type)
        {
            _stateType = type;
            switch (_stateType)
            {
                case EnemyStateType.Idle:
                    StateMachine.ChangeState(new IdleState(this, StateMachine));
                    break;
                case EnemyStateType.Patrol:
                    StateMachine.ChangeState(new PatrolState(this, StateMachine));
                    break;
                case EnemyStateType.Chase:
                    StateMachine.ChangeState(new ChaseState(this, StateMachine));
                    break;
                case EnemyStateType.Attack:
                    StateMachine.ChangeState(new AttackState(this, StateMachine));
                    break;
                case EnemyStateType.Dead:
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
            damageable.OnTakeDamage += OnTakeDamage;
            animator.WeaponEvent.PlayAction += combat.WeaponPlay;
            animator.WeaponEvent.EndAction += OnWeaponEnd;
        }

        private void OnDisable()
        {
            stats.OnDeath -= OnDeath;
            damageable.OnTakeDamage -= OnTakeDamage;
            animator.WeaponEvent.PlayAction -= combat.WeaponPlay;
            animator.WeaponEvent.EndAction -= OnWeaponEnd;
        }
        
        private void Update()
        {
            StateMachine.Update();
        }

        // Action
        private void OnDeath()
        {
            Debug.Log("Dead");
        }

        // Controller
        public void MotorMove()
        {
            motor.MoveTo(_player.position, true);
        }
        
        public void MotorMoveTo(Vector2 position)
        {
            motor.MoveTo(position, false);
        }
        
        public void MotorStop()
        {
            motor.Stop();
        }

        public bool SuccessMove(Vector2 dir)
        {
            return motor.SuccessMove(dir);
        }

        public void ManualAttack()
        {
            AnimatorPlayAnimation(EntityAnimationType.Attack);
            combat.ManualAttack();
        }
        
        private void OnWeaponEnd()
        {
            AnimatorPlayAnimation(EntityAnimationType.Idle);
        }
        
        public void AnimatorPlayAnimation(EntityAnimationType animationType)
        {
            animator.PlayAnimation(animationType);
        }
        
        private void OnTakeDamage(float damage)
        {
            stats.ReduceHealth(damage);
        }

        public bool IsInDetectRange()
        {
            return TargetFinder.IsInRange(_player, transform, data.DetectRange);
        }

        public bool IsInAttackRange()
        {
            return TargetFinder.IsInRange(_player, transform, data.AttackRange);
        }
        
        public Vector2 Position => transform.position;
        
        public Stats GetStats()
        {
            return data.EntityStats;
        }
        public EnemyData Data => data;
        public WeaponData GetWeaponData()
        {
            return data.WeaponData;
        }
        public RuntimeStats GetRuntimeStats()
        {
            return stats.RuntimeStats;
        }
    }
}