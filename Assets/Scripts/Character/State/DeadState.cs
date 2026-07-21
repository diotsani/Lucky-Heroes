using Enums;

namespace Character
{
    public class DeadState : CharacterState
    {
        public DeadState(CharacterBrain character, CharacterStateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            Character.InputStop();
            Character.MotorStop();
            Character.AnimatorPlayAnimation(CharacterAnimationType.Death);
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