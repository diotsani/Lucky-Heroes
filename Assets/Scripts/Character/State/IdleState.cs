using Enums;
using StateMachines;
using UnityEngine;

namespace Character
{
    public class IdleState : CharacterState
    {
        public IdleState(CharacterBrain character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            Character.Input.Continue();
            Character.Motor.Stop();
            Character.Animator.SetFloat(Character.Animator.Speed, 0);
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