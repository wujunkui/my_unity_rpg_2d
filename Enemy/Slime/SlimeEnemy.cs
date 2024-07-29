using Enemy.Slime.States;
using UnityEngine;

namespace Enemy.Slime
{
    public class SlimeEnemy : Enemy
    {
        public SlimeIdleState idleState;
        public SlimeMoveState moveState;
        public SlimeAttackState attackState;
        public SlimeBattleState battleState;
        public SlimeDeadState deadState;

        protected override void Awake()
        {
            base.Awake();
            InitStates();
        }

        protected override void Start()
        {
            base.Start();
            stateMachine.Initialize(idleState);
        }

        private void InitStates()
        {
            idleState = CreateNewState<SlimeIdleState>("Idle");
            moveState = CreateNewState<SlimeMoveState>("Move");
            battleState = CreateNewState<SlimeBattleState>("Move");
            attackState = CreateNewState<SlimeAttackState>("Attack");
            deadState = CreateNewState<SlimeDeadState>("Dead");
        }
        
        public override void Die()
        {
            base.Die();
            stateMachine.ChangeState(deadState);
            Destroy(gameObject, 3);
        }
    }
}