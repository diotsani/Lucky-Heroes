using Enums;
using StateMachines;

namespace Character
{
    public class IdleState : CharacterState
    {
        public IdleState(CharacterBrain character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            Character.MotorStop();
            Character.AnimatorPlayAnimation(EntityAnimationType.Idle);
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            if (!Character.InputMoving()) return;
            if (Character.InputRunning())
            {
                Character.ChangeState(CharacterStateType.Run);
            }
            else
            {
                Character.ChangeState(CharacterStateType.Walk);
            }
        }
    }
}