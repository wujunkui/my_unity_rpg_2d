namespace Player
{
    public class PlayerIdleState: PlayerGroundState
    {
        public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.SetZeroVelocity();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if (xInput != 0 && player.IsGroundedDetected())
                stateMachine.ChangeState(player.moveState);
        }
    }
}