namespace Player
{
    public class PlayerDashState: PlayerState
    {
        public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            stateTimer = player.dashDuration;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            player.SetVelocity(player.dashSpeed * player.dashDir, 0);
            if (player.IsWallDetected())
            {
                stateMachine.ChangeState(player.wallSlideState);
                return;
            }
            if (stateTimer < 0)
                stateMachine.ChangeState(player.idleState);
        }
    }
}