using System;
using Battlefield;
using Character;
using Core;
using Database;
using Database.Enemy;
using Database.Weapon;
using Entity;
using Enums;
using Pool;
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

        public EntityAnimator Animator => animator;
        private SpawnManager _spawn;
        private Transform _player;
        private GameManager _game;
        
        public Action OnDeath { get; set; }
            
        private EnemyStateType _stateType;
        private StateMachine StateMachine { get; set; }
        
        private void Awake()
        {
            StateMachine = new StateMachine();
        }
        
        public void Initialize(SpawnManager spawn, Vector2 position, DifficultyData diffData)
        {
            // Temp
            if(_spawn == null) _spawn = spawn;
            if(_player == null) _player = Services.ServiceLocator.Get<CharacterBrain>().transform;
            if(_game == null) _game = Services.ServiceLocator.Get<GameManager>();
            stats.InitializeStats(diffData);
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
            OnDeath += Death;
            damageable.OnTakeDamage += OnTakeDamage;
            animator.WeaponEvent.PlayAction += combat.WeaponPlay;
            animator.WeaponEvent.EndAction += OnWeaponEnd;
        }

        private void OnDisable()
        {
            OnDeath -= Death;
            damageable.OnTakeDamage -= OnTakeDamage;
            animator.WeaponEvent.PlayAction -= combat.WeaponPlay;
            animator.WeaponEvent.EndAction -= OnWeaponEnd;
        }
        
        private void Update()
        {
            StateMachine.Update();
        }

        // Action
        private void Death()
        {
            _game.Loot.Roll(data.Loot, transform.position);
            _spawn.Remove(this);
            Services.ServiceLocator.Get<PoolManager>().Release(this);
        }

        public void ForceDespawn()
        {
            Services.ServiceLocator.Get<PoolManager>().Release(this);
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
            animator.SetTrigger(animator.Attack);
            combat.ManualAttack();
        }
        
        private void OnWeaponEnd()
        {
            Animator.SetFloat(Animator.Speed, 0);
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