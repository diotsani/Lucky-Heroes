using Enums;
using StateMachines;
using UnityEngine;

namespace Enemy
{
    public class AttackState : EnemyState
    {
        public AttackState(EnemyBrain enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void Enter()
        {
            Enemy.MotorStop();
            Enemy.AnimatorPlayAnimation(EntityAnimationType.Idle);

            Interval = Enemy.Data.AttackInterval;
            //Timer = Interval;
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            Timer -= Time.deltaTime;
            if(Timer > 0)return;

            Enemy.ManualAttack();
            
            if (!Enemy.IsInAttackRange())
            {
                Enemy.ChangeState(EnemyStateType.Chase);
            }

            Timer = Interval;
        }
    }
}