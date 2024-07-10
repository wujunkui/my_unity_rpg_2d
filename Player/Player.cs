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

        public float counterAttackDuration = .2f;
        
        public bool isBusy;
        
        [Header("Move Info")] 
        public float moveSpeed = 5f;
        public float jumpForce = 5f;
        public float wallSlideSpeed = 5f;

        [Header("Dash Info")] 
        public float dashDir;
        public float dashSpeed = 12f;
        public float dashDuration = 1f;
        [SerializeField] 
        private float dashCooldown;
        private float dashUsageTimer;
        

        public PlayerStateMachine stateMachine { get; private set; }
        public PlayerIdleState idleState { get; private set; }
        public PlayerMoveState moveState { get; private set; }
        public PlayerJumpState jumpState { get; private set; }
        public PlayerAirState airState { get; private set; }
        public PlayerDashState dashState { get; private set; }
        public PlayerWallSlideState wallSlideState { get; private set; }
        public PlayerWallJumpState wallJumpState { get; private set; }
        public PlayerPrimaryAttackState primaryAttack { get; private set; }
        public PlayerCounterAttackState counterAttack { get; private set; }
        public PlayerAimSwordState aimSwordState { get; private set; }
        public PlayerCatchSwordState catchSwordState { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();
            stateMachine = new PlayerStateMachine();
            idleState = new PlayerIdleState(this, stateMachine, "Idle");
            moveState = new PlayerMoveState(this, stateMachine, "Move");
            jumpState = new PlayerJumpState(this, stateMachine, "Jump");
            airState = new PlayerAirState(this, stateMachine, "Jump");
            dashState = new PlayerDashState(this, stateMachine, "Dash");
            wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
            wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
            primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
            counterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
            aimSwordState = new PlayerAimSwordState(this, stateMachine, "AimSword");
            catchSwordState = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
        }

        protected override void Start()
        {
            base.Start();
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
        

    }
}