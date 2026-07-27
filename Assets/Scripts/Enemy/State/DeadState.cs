using Enums;
using StateMachines;

namespace Enemy
{
    public class DeadState : EnemyState
    {
        public DeadState(EnemyBrain enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void Enter()
        {
            Enemy.Animator.SetTrigger(Enemy.Animator.Death);
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            
        }
    }
}