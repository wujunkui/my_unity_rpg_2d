using UnityEngine;

namespace Enemy.Skeleton
{
    public class Enemy_Skeleton: Enemy
    {

        public BoxCollider2D bd;
        
        public SkeletonIdleState idleState { get; private set; }
        public SkeletonMoveState moveState { get; private set; }
        public SkeletonBattleState battleState { get; private set; }
        public SkeletonAttackState attackState { get; private set; }
        public SkeletonStunnedState stunnedState { get; private set; }
        
        public SkeletonDeadState deadState { get; private set; }
        protected override void Awake()
        {
            base.Awake();
            bd = GetComponent<BoxCollider2D>();
            InitStates();

        }

        protected virtual void InitStates()
        {
            idleState = new SkeletonIdleState(this, stateMachine, "Idle", this);
            moveState = new SkeletonMoveState(this, stateMachine, "Move", this);
            battleState = new SkeletonBattleState(this, stateMachine, "Move", this);
            attackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
            stunnedState = new SkeletonStunnedState(this, stateMachine, "Stunned", this);
            deadState = new SkeletonDeadState(this, stateMachine, "Idle", this);
        }

        protected override void Start()
        {
            base.Start();
            stateMachine.Initialize(idleState);
        }

        public override bool CanBeStunned()
        {
            if (base.CanBeStunned())
            {
                stateMachine.ChangeState(stunnedState);
                return true;
            }

            return false;
        }

        public override void Die()
        {
            base.Die();
            stateMachine.ChangeState(deadState);
        }
    }
    
    
}