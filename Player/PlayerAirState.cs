using UnityEngine;
namespace Player
{
    public class PlayerAirState: PlayerState
    {
        public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }


        public override void Update()
        {
            base.Update();
            if (player.inputActions.Attack.WasPressedThisFrame())
            {
                stateMachine.ChangeState(player.airAttackState);
            }
            if (player.IsWallDetected() && xInput != 0)
                stateMachine.ChangeState(player.wallSlideState);


            if (xInput != 0)
                // 在空中时不容易控制方向，将空中的x轴速度降低为0.8倍
                player.SetVelocity(xInput * player.moveSpeed * 0.8f, player.rb.velocity.y);
        }
    }
}