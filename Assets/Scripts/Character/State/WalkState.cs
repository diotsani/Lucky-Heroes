using Enums;
using StateMachines;

namespace Character
{
    public class WalkState : CharacterState
    {
        private float _speed = 0;
        public WalkState(CharacterBrain character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            _speed = Character.GetStats().WalkSpeed;
            Character.Animator.SetFloat(Character.Animator.Speed, 1);
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            var input = Character.Input.Move;
            Character.Motor.Move(input, _speed);

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