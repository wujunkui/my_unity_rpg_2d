namespace Player
{
    public class PlayerWallJumpState: PlayerState
    {
        public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            stateTimer = .4f;
            player.SetVelocity(5 * -player.facingDirection, player.jumpForce);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if(stateTimer < 0)
                stateMachine.ChangeState(player.fallState);
            if(player.IsGroundedDetected())
                stateMachine.ChangeState(player.idleState);
        }
    }
}