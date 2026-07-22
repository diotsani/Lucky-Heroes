using Enums;
using StateMachines;
using UnityEngine;

namespace Enemy
{
    public class IdleState : EnemyState
    {
        public IdleState(EnemyBrain enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void Enter()
        {
            Enemy.MotorStop();
            Enemy.AnimatorPlayAnimation(EntityAnimationType.Idle);

            Interval = Enemy.Data.DetectInterval;
            Timer = Interval;
        }

        public override void Exit()
        {
            
        }
        
        public override void Update()
        {
            Timer -= Time.deltaTime;
            if(Timer > 0)return;
            
            if (Enemy.IsInDetectRange())
            {
                Enemy.ChangeState(EnemyStateType.Chase);
            }

            if (Enemy.IsInAttackRange())
            {
                Enemy.ChangeState(EnemyStateType.Attack);
            }

            Timer = Interval;
        }
    }
}