namespace Player
{
    public class PlayerStateMachine
    {
        public PlayerState currentState { get; private set; }

        public void Initialize(PlayerState _initalState)
        {
            currentState = _initalState;
            currentState.Enter();
        }

        public void ChangeState(PlayerState _newState)
        {
            currentState.Exit();
            currentState = _newState;
            currentState.Enter();
        }
    }
}