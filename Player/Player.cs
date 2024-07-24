using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Player : Entity
    {
        
        [Header("PopUp Text")]
        
        
        [Header("Attack details")] 
        public Vector2[] attackMovements;

        public float counterAttackDuration = .2f;
        
        public bool isBusy;

        [Header("Move Info")]
        public float xInput;
        public float yInput;
        public float moveSpeed = 5f;
        public float jumpForce = 5f;
        public float wallSlideSpeed = 5f;

        [Header("Dash Info")] 
        public float dashDir;
        public float dashSpeed = 12f;
        public float dashDuration = 1f;
        [SerializeField] private float dashCooldown;
        private float dashUsageTimer;

        public float swordCatchImpact;

        public SkillManager skill;

        public GameObject sword;
        
        public PlayerStateMachine stateMachine { get; private set; }
        public PlayerIdleState idleState { get; private set; }
        public PlayerMoveState moveState { get; private set; }
        public PlayerJumpState jumpState { get; private set; }
        public PlayerFallState fallState { get; private set; }
        public PlayerDashState dashState { get; private set; }
        public PlayerWallSlideState wallSlideState { get; private set; }
        public PlayerWallJumpState wallJumpState { get; private set; }
        public PlayerPrimaryAttackState primaryAttack { get; private set; }
        public PlayerCounterAttackState counterAttack { get; private set; }
        public PlayerAimSwordState aimSwordState { get; private set; }
        public PlayerCatchSwordState catchSwordState { get; private set; }
        public PlayerDeadState deadState { get; private set; }
        public PlayerAirAttackState airAttackState { get; private set; }

        public PlayerControls.PlayerActions inputActions;
        
        protected override void Awake()
        {
            base.Awake();
            skill = SkillManager.instance;

            inputActions = new PlayerControls().Player;
            inputActions.Enable();
            
            stateMachine = new PlayerStateMachine();
            idleState = new PlayerIdleState(this, stateMachine, "Idle");
            moveState = new PlayerMoveState(this, stateMachine, "Move");
            jumpState = new PlayerJumpState(this, stateMachine, "Jump");
            fallState = new PlayerFallState(this, stateMachine, "Jump");
            dashState = new PlayerDashState(this, stateMachine, "Dash");
            wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
            wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
            primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
            counterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
            aimSwordState = new PlayerAimSwordState(this, stateMachine, "AimSword");
            catchSwordState = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
            deadState = new PlayerDeadState(this, stateMachine, "Die");
            // airAttackState = new PlayerAirAttackState(this, stateMachine, "AirAttack");
            airAttackState = CreateNewState<PlayerAirAttackState>("AirAttack");
        }

        protected virtual TState CreateNewState<TState>(string _animName) where TState: PlayerState
        {
            return (TState)Activator.CreateInstance(typeof(TState), this, stateMachine, _animName);
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

        public void AssignNewSword(GameObject _newSword)
        {
            sword = _newSword;
        }

        public void CatchTheSword()
        {
            stateMachine.ChangeState(catchSwordState);
            Destroy(sword);
        }
        
        private void CheckForDashInput()
        {
            dashUsageTimer -= Time.deltaTime;
            if (IsWallDetected()) return;
            if (inputActions.Dash.WasPressedThisFrame() && dashUsageTimer < 0)
            {
                dashUsageTimer = dashCooldown;
                dashDir = xInput != 0 ? xInput : facingDirection;
                stateMachine.ChangeState(dashState);
            }
        }

        public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

        public override void Die()
        {
            base.Die();
            stateMachine.ChangeState(deadState);
        }
        
        
        private void OnMove(InputValue value)
        {
            xInput = value.Get<Vector2>().x;
            yInput = value.Get<Vector2>().y;
        }
    }
}