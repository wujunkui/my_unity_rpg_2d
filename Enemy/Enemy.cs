using UnityEngine;

namespace Enemy
{
    public class Enemy : Entity
    {
        public EnemyStateMachine stateMachine { get; private set; }
        protected override void Awake()
        {
            base.Awake();
            stateMachine = new EnemyStateMachine();

        }

        protected override void Update()
        {
            base.Update();
            stateMachine.currentState.Update();
        }
    }
}