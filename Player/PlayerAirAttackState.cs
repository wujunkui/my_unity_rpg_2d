using UnityEngine;

namespace Player
{
    public class PlayerAirAttackState: PlayerState
    {
        public PlayerAirAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
        }

        public override void Update()
        {
            base.Update();
            if(player.IsGroundedDetected())
                player.SetZeroVelocity();
                // stateMachine.ChangeState(player.idleState);
            if(triggerCalled)
                stateMachine.ChangeState(player.idleState);
        }
    }
}