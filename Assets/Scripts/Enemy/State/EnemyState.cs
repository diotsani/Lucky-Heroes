using StateMachines;

namespace Enemy
{
    public abstract class EnemyState : IState
    {
        protected readonly EnemyBrain Enemy;
        protected float Timer;
        protected float Interval;

        protected EnemyState(EnemyBrain enemy, StateMachine stateMachine)
        {
            Enemy = enemy;
            StateMachine = stateMachine;
        }
    }
}