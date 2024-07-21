using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerAimSwordState: PlayerState
    {
        public PlayerAimSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.skill.sword.DotsActive(true);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            player.SetZeroVelocity();
            if(player.inputActions.Aim.WasReleasedThisFrame())
                stateMachine.ChangeState(player.idleState);

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            if(player.transform.position.x > mousePosition.x && player.facingDirection == 1)
                player.Flip();
            else if (player.transform.position.x < mousePosition.x && player.facingDirection == -1)
                player.Flip();
        }
    }
}