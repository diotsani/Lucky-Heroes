using StateMachines;

namespace Character
{
    public abstract class CharacterState : IState
    {
        protected readonly CharacterBrain Character;

        protected CharacterState(CharacterBrain character, StateMachine stateMachine)
        {
            Character = character;
            StateMachine = stateMachine;
        }
    }
}