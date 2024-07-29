using System;
using Controllers;
using Enemy.Archer.States;
using Stats;
using UnityEngine;

namespace Enemy.Archer
{
    public class EnemyArcher: Enemy
    {
        public BoxCollider2D bd;
        [Header("Archer special")] 
        public bool canMove;
        public  Vector2 jumpVelocity;
        public float jumpCoolDown = 1;
        public GameObject arrow;
        public ArcherIdleState idleState { get; private set; }
        public ArcherMoveState moveState { get; private set; }
        public ArcherAttackState attackState { get; private set; }
        public ArcherJumpState jumpState { get; private set; }
        public ArcherFallState fallState { get; private set; }
        public ArcherBattleState battleState { get; private set; }
        public ArcherDeadState deadState { get; private set; }

        private void InitStates()
        {
            idleState = CreateNewState<ArcherIdleState>("Idle");
            moveState = CreateNewState<ArcherMoveState>("Move");
            attackState = CreateNewState<ArcherAttackState>("Attack");
            jumpState = CreateNewState<ArcherJumpState>("Jump");
            fallState = CreateNewState<ArcherFallState>("Jump");
            battleState = CreateNewState<ArcherBattleState>("Idle");
            deadState = CreateNewState<ArcherDeadState>("Dead");
        }
        

        protected override void Awake()
        {
            base.Awake();
            bd = GetComponent<BoxCollider2D>();
        }

        protected override void Start()
        {
            base.Start();
            InitStates();
            stateMachine.Initialize(idleState);
        }
        
        public override void Die()
        {
            base.Die();
            stateMachine.ChangeState(deadState);
            Destroy(gameObject, 3);
        }
        
        public bool FallTargetIsGround()
        {
            Vector3 checkPosition = new Vector3(transform.position.x  - jumpVelocity.x * facingDirection, transform.position.y - bd.size.y / 2);
            return Physics2D.Raycast(checkPosition , Vector2.down, groundCheckDistance, whatIsGround);
        }

        public void EmitArrow()
        {
            GameObject newArrow = Instantiate(arrow, attackCheck.position, Quaternion.identity);
            if (!facingRight)
            {
                var controller = newArrow.GetComponent<ArrowController>();
                controller.SetArrowDirection(false);
                controller.SetDamage(GetComponent<CharacterStats>().damage.GetValue());
                newArrow.transform.Rotate(0, 180, 0);
            }
        }
        
    }
}