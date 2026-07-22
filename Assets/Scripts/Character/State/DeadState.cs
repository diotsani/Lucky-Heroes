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
            Character.InputStop();
            Character.MotorStop();
            Character.AnimatorPlayAnimation(EntityAnimationType.Death);
            Character.CombatStop();
            Character.SkillStop();
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            
        }
    }
}