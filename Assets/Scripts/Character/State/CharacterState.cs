namespace Character
{
    public abstract class CharacterState
    {
        protected CharacterBrain Character;
        protected CharacterStateMachine StateMachine;

        protected CharacterState(CharacterBrain character, CharacterStateMachine stateMachine)
        {
            Character = character;
            StateMachine = stateMachine;
        }

        public abstract void Enter();
        
        public abstract void Exit();
        
        public abstract void Update();
    }
}