using Enums;

namespace Character
{
    public class WalkState : CharacterState
    {
        public WalkState(CharacterBrain character, CharacterStateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            Character.AnimatorPlayAnimation(CharacterAnimationType.Walk);
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