using Enums;
using StateMachines;

namespace Enemy
{
    public class ChaseState : EnemyState
    {
        public ChaseState(EnemyBrain enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void Enter()
        {
            Enemy.Animator.SetFloat(Enemy.Animator.Speed, 2);
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            // Move
            Enemy.MotorMove();
            
            if (Enemy.IsInAttackRange())
            {
                Enemy.ChangeState(EnemyStateType.Attack);
            }
        }
    }
}