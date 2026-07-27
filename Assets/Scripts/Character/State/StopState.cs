using Enums;
using StateMachines;

namespace Character
{
    public class StopState : CharacterState
    {
        public StopState(CharacterBrain character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            Character.Input.Stop();
            Character.Motor.Stop();
            Character.Animator.SetFloat(Character.Animator.Speed, 0);
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            
        }
    }
}