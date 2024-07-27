using UnityEngine;

namespace Player
{
    public class PlayerCatchSwordState: PlayerState
    {
        private Transform sword;
        public PlayerCatchSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            // 只有在地上接住剑才有尘土
            if(player.IsGroundedDetected())
                player.fx.PlayDustFX(); 
            // player.fx.ScreenShake(player.fx.swordCatchShakePower);
            sword = player.sword.transform;
            if(player.transform.position.x > sword.position.x && player.facingDirection == 1)
                player.Flip();
            else if (player.transform.position.x < sword.position.x && player.facingDirection == -1)
                player.Flip();
            rb.velocity = new Vector2(player.swordCatchImpact * -player.facingDirection, rb.velocity.y);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if(triggerCalled)
                stateMachine.ChangeState(player.idleState);
        }
    }
}