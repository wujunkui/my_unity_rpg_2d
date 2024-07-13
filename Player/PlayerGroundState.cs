using Skills;
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
            if(Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword())
                stateMachine.ChangeState(player.aimSwordState);
            if (Input.GetKeyDown(KeyCode.Q))
                stateMachine.ChangeState(player.counterAttack);
            if (Input.GetKeyDown(KeyCode.J) || Input.GetKey(KeyCode.Mouse0))
                stateMachine.ChangeState(player.primaryAttack);
            if (!player.IsGroundedDetected())
                stateMachine.ChangeState(player.fallState);
            if (Input.GetButton("Jump") && player.IsGroundedDetected())
                stateMachine.ChangeState(player.jumpState);
            
        }

        private bool HasNoSword()
        {
            if (!player.sword) return true;
            player.sword.GetComponent<SwordSkillController>().ReturnSword();
            return false;
        }
        
    }
}