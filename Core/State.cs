using UnityEngine;
namespace Core
{
    public abstract class State
    {
        protected string animBoolName;
        protected float stateTimer;
        protected bool triggerCalled;
        public Rigidbody2D rb;
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
    }
}