using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Player : Entity
    {

        [Header("Attack details")] 
        public Vector2[] attackMovements;
        
        public bool isBusy;
        [Header("Move Info")] 
        
        public float moveSpeed = 5f;
        public float jumpForce = 5f;
        public float wallSlideSpeed = 5f;

        [Header("Dash Info")] 
        public float dashDir;
        public float dashSpeed = 12f;
        public float dashDuration = 1f;
        [SerializeField] private float dashCooldown;
        private float dashUsageTimer;
        public int facingDirection { get; private set; } = 1;
        private bool facingRight = true;
        
        

        [Header("Detected check")] 
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private Transform wallCheck;
        [SerializeField] private float wallCheckDistance;

        public Rigidbody2D rb { get; private set; }
        private CapsuleCollider2D cd;

        public PlayerStateMachine stateMachine { get; private set; }
        public PlayerIdleState idleState { get; private set; }
        public PlayerMoveState moveState { get; private set; }
        public PlayerJumpState jumpState { get; private set; }
        public PlayerAirState airState { get; private set; }
        public PlayerDashState dashState { get; private set; }
        public PlayerWallSlideState wallSlideState { get; private set; }
        public PlayerWallJumpState wallJumpState { get; private set; }
        public PlayerPrimaryAttackState primaryAttack { get; private set; }
        private void Awake()
        {
            stateMachine = new PlayerStateMachine();
            idleState = new PlayerIdleState(this, stateMachine, "Idle");
            moveState = new PlayerMoveState(this, stateMachine, "Move");
            jumpState = new PlayerJumpState(this, stateMachine, "Jump");
            airState = new PlayerAirState(this, stateMachine, "Jump");
            dashState = new PlayerDashState(this, stateMachine, "Dash");
            wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
            wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
            primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        }

        protected override void Start()
        {
            base.Start();
            rb = GetComponent<Rigidbody2D>();
            cd = GetComponent<CapsuleCollider2D>();
            stateMachine.Initialize(idleState);
        }

        protected override void Update()
        {
            base.Update();
            stateMachine.currentState.Update();
            CheckForDashInput();
        }

        public IEnumerable BusyFor(float _seconds)
        {
            isBusy = true;
            yield return new WaitForSeconds(_seconds);
            isBusy = false;
        }

        public void SetVelocity(float _xVelocity, float _yVelocity)
        {
            rb.velocity = new Vector2(_xVelocity, _yVelocity);
            FlipController(_xVelocity);
        }

        public void SetZeroVelocity()
        {
            rb.velocity = Vector2.zero;
        }

        private void CheckForDashInput()
        {
            dashUsageTimer -= Time.deltaTime;
            if (IsWallDetected()) return;
            if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0)
            {
                dashUsageTimer = dashCooldown;
                var xInput = Input.GetAxisRaw("Horizontal");
                dashDir = xInput != 0 ? xInput : facingDirection;
                stateMachine.ChangeState(dashState);
            }
        }

        public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

        #region Collider check

        public bool IsGroundedDetected() =>
            Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

        public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection,
            wallCheckDistance, whatIsGround);

        #endregion

        #region Flip

        public void Flip()
        {
            facingDirection *= -1;
            facingRight = !facingRight;
            transform.Rotate(0, 180, 0);
        }

        public void FlipController(float _x)
        {
            if (_x > 0 && !facingRight)
                Flip();
            else if (_x < 0 && facingRight)
                Flip();
        }

        #endregion

        #region Gizmos
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(groundCheck.position,
                new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
            Gizmos.DrawLine(wallCheck.position,
                new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        }
        
        #endregion
    }
}