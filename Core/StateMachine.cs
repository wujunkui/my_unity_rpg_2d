using UnityEngine;
namespace Core
{
    public class StateMachine<TState> where TState: State
    {
        public TState currentState { get; private set; }

        public virtual void Initialize(TState _initalState)
        {
            currentState = _initalState;
            currentState.Enter();
        }

        public virtual void ChangeState(TState _newState)
        {
            currentState.Exit();
            currentState = _newState;
            currentState.Enter();
        }
    }
}