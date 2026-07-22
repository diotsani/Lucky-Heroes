using Enums;
using StateMachines;
using UnityEngine;

namespace Enemy
{
    public class PatrolState : EnemyState
    {
        private Vector2 _nextPatrolPoint;
        public PatrolState(EnemyBrain enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void Enter()
        {
            Enemy.AnimatorPlayAnimation(EntityAnimationType.Walk);
            _nextPatrolPoint = NextPoint(Enemy.Position, Enemy.Data.PatrolRange);
            Interval = Enemy.Data.PatrolInterval;
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            if (Enemy.SuccessMove(_nextPatrolPoint - Enemy.Position))
            {
                Enemy.AnimatorPlayAnimation(EntityAnimationType.Idle);
            }
            else
            {
                Enemy.MotorMoveTo(_nextPatrolPoint);
            }
            
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
            
            _nextPatrolPoint = NextPoint(Enemy.Position, Enemy.Data.PatrolRange);
            Enemy.AnimatorPlayAnimation(EntityAnimationType.Walk);

            Timer = Interval;
        }

        private Vector2 NextPoint(Vector2 pos, float range)
        {
            return new Vector2(pos.x + Random.Range(-range, range), pos.y + Random.Range(-range, range));
        }
    }
}