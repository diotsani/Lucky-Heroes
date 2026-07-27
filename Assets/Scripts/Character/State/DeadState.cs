using Enums;
using StateMachines;

namespace Character
{
    public class DeadState : CharacterState
    {
        public DeadState(CharacterBrain character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            Character.Input.Stop();
            Character.Motor.Stop();
            Character.Animator.SetTrigger(Character.Animator.Death);
            Character.Combat.Stop();
            Character.Skill.Stop();
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            
        }
    }
}