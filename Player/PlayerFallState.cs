using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerFallState : PlayerState
    {
        public PlayerFallState(Player _player, PlayerStateMachine _stateMachine, string _AnimBoolName) : base(_player,
            _stateMachine, _AnimBoolName)
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
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                stateMachine.ChangeState(player.airAttackState);
            }

            if (player.IsWallDetected())
                stateMachine.ChangeState(player.wallSlideState);

            if (player.IsGroundedDetected())
                stateMachine.ChangeState(player.idleState);
            if (xInput != 0)
                // 在空中时不容易控制方向，将空中的x轴速度降低为0.8倍
                player.SetVelocity(xInput * player.moveSpeed * 0.8f, player.rb.velocity.y);
        }
    }
}
