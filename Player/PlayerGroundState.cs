using UnityEngine;

namespace Player
{
    public class PlayerGroundState: PlayerState
    {
        public PlayerGroundState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
                stateMachine.ChangeState(player.primaryAttack);
            if (!player.IsGroundedDetected())
                stateMachine.ChangeState(player.airState);
            if (Input.GetButton("Jump") && player.IsGroundedDetected())
                stateMachine.ChangeState(player.jumpState);
            
        }
    }
}