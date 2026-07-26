using Enums;
using StateMachines;

namespace Character
{
    public class WalkState : CharacterState
    {
        public WalkState(CharacterBrain character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            Character.Animator.PlayAnimation(EntityAnimationType.Walk);
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            Character.MotorMove();

            if (!Character.InputMoving())
            {
                Character.ChangeState(CharacterStateType.Idle);
                return;
            }

            if (Character.InputRunning())
            {
                Character.ChangeState(CharacterStateType.Run);
            }
        }
    }
}