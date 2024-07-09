namespace Enemy
{
    public class EnemyStateMachine
    {
        public EnemyState  currentState { get; private set; }

        public void Initialize(EnemyState _initalState)
        {
            currentState = _initalState;
            currentState.Enter();
        }

        public void ChangeState(EnemyState _newState)
        {
            currentState.Exit();
            currentState = _newState;
            currentState.Enter();
        }
    }
}