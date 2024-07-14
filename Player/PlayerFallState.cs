namespace Player
{
    public class PlayerFallState:PlayerAirState
    {
        public PlayerFallState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Update()
        {
            base.Update();
            if (player.IsGroundedDetected())
                stateMachine.ChangeState(player.idleState);
        }
    }
}