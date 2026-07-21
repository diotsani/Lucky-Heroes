using UnityEngine;

namespace Character
{
    public class CharacterStateMachine
    {
        public CharacterState CurrentState { get; private set; }

        private CharacterBrain _character;
        
        public CharacterStateMachine(CharacterBrain character)
        {
            _character = character;
        }

        public void ChangeState(CharacterState newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        public void Update()
        {
            CurrentState?.Update();
        }
    }
}
