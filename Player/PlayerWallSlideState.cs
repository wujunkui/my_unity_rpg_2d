using UnityEngine;

namespace Player
{
    public class PlayerWallSlideState: PlayerState
    {
        public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if(Input.GetButton("Jump"))
            {
                stateMachine.ChangeState(player.wallJumpState);
                return;
            }
            rb.velocity = yInput < 0 ? new Vector2(0, rb.velocity.y ) : new Vector2(0, rb.velocity.y * .7f);
            if (!player.IsWallDetected())
                stateMachine.ChangeState(player.fallState);
            if (xInput != 0 && player.facingDirection != xInput)
            {
                stateMachine.ChangeState(player.idleState);
                return;
            }
            if (player.IsGroundedDetected())
                stateMachine.ChangeState(player.idleState);
            
        }
    }
}