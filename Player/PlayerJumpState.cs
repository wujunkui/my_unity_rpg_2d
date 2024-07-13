using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerJumpState : PlayerState
    {
        public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _AnimBoolName) : base(_player,
            _stateMachine, _AnimBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
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
            player.SetVelocity(xInput * player.moveSpeed, player.rb.velocity.y);
            if (rb.velocity.y < 0)
                stateMachine.ChangeState(player.fallState);
        }
    }

}