using UnityEngine;


namespace Player
{
    public class PlayerState
    {
        protected Player player;
        protected PlayerStateMachine stateMachine;
        protected Rigidbody2D rb;

        protected float xInput;
        protected float yInput;
        protected string animBoolName;
        protected float stateTimer;
        protected bool triggerCalled;

        public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        {
            player = _player;
            stateMachine = _stateMachine;
            animBoolName = _animBoolName;
        }

        public virtual void Enter()
        {
            player.anim.SetBool(animBoolName, true);
            rb = player.rb;
            triggerCalled = false;
        }

        public virtual void Exit()
        {
            player.anim.SetBool(animBoolName, false);
        }

        public virtual void Update()
        {
            // xInput = Input.GetAxisRaw("Horizontal");
            // yInput = Input.GetAxisRaw("Vertical");
            SetInput();
            player.anim.SetFloat("yVelocity", rb.velocity.y);
            stateTimer -= Time.deltaTime;
        }

        public virtual void AnimationFinishTrigger()
        {
            triggerCalled = true;
        }

        protected void SetInput()
        {
            var _xInput = player.xInput;
            var _yInput = player.yInput;
            xInput = _xInput < -.5f ? -1 : _xInput > .5f ? 1 : 0;
            yInput = _yInput < -.5f ? -1 : _yInput > .5f ? 1 : 0;
        }
    }
}