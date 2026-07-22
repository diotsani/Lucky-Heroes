namespace StateMachines
{
    public abstract class IState
    {
        protected StateMachine StateMachine;

        public abstract void Enter();
        
        public abstract void Exit();
        
        public abstract void Update();
    }
}