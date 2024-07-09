using UnityEngine;

namespace Player
{
    public class PlayerPrimaryAttackState: PlayerState
    {
        private int comboCounter;
        private float lastTimeAttacked;
        private float comboWindow = 2;
        public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            xInput = 0; // 修复可能出现攻击反向问题，但是不能在攻击中改变方向了
            if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
                comboCounter = 0;
            player.anim.SetInteger("ComboCounter", comboCounter);

            float attackDir = player.facingDirection;
            if (xInput != 0)
                attackDir = xInput;
            
            player.SetVelocity(player.attackMovements[comboCounter].x * attackDir,
                player.attackMovements[comboCounter].y);
            stateTimer = .1f;
        }

        public override void Exit()
        {
            base.Exit();
            comboCounter++;
            lastTimeAttacked = Time.time;
        }

        public override void Update()
        {
            base.Update();
            if (stateTimer < 0)
                player.SetZeroVelocity();
            if (triggerCalled)
                stateMachine.ChangeState(player.idleState);
        }
    }
}