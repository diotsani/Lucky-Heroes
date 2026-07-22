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
            Enemy.AnimatorPlayAnimation(EntityAnimationType.Death);
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            
        }
    }
}