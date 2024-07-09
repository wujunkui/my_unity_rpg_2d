using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class Enemy : Entity
    {
        [SerializeField] protected LayerMask whatIsPlayer;
        [SerializeField] protected float playerDetectDistance = 50f;
        // stunned info
        public float stunDuration = 1;
        public Vector2 stunDirection;
        
        // move info
        public float moveSpeed = 1.5f;
        public float idleTime = 2f;
        public float battleTime = 4f;
        public float giveUpDistance = 7;
        public float alertDistance = 2;
        
        public float attackDistance = 1.5f;
        public float attackCooldown;
        [HideInInspector] public float lastAttackTime;
        
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

        public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

        public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position,
            Vector2.right * facingDirection, playerDetectDistance, whatIsPlayer);

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = Color.yellow; 
            Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + facingDirection * attackDistance, wallCheck.position.y));
        }
    }
}