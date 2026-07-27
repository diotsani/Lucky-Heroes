using Enums;
using StateMachines;

namespace Character
{
    public class RunState : CharacterState
    {
        private float _speed = 0;
        public RunState(CharacterBrain character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            _speed = Character.GetStats().RunSpeed;
            Character.Animator.SetFloat(Character.Animator.Speed, 2);
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

            if (!Character.InputRunning())
            {
                Character.ChangeState(CharacterStateType.Walk);
            }
        }
    }
}