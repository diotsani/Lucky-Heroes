using Enums;
using StateMachines;

namespace Character
{
    public class RunState : CharacterState
    {
        public RunState(CharacterBrain character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            Character.Animator.PlayAnimation(EntityAnimationType.Run);
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

            if (!Character.InputRunning())
            {
                Character.ChangeState(CharacterStateType.Walk);
            }
        }
    }
}