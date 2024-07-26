using Controllers;
using UnityEngine;
namespace Player
{
    public class PlayerCounterAttackState: PlayerState
    {
        private string successfulCounterAnimName = "SuccessfulCounterAttack";
        public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            stateTimer = player.counterAttackDuration;
            player.anim.SetBool(successfulCounterAnimName, false);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            player.SetZeroVelocity();
            Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadio);
            foreach (var hit in colliders)
            {
                if (hit.TryGetComponent(out ArrowController value))
                {
                    SuccessfulCounterAttack();
                    value.FlipArrow();
                }
                
                var enemy = hit.GetComponent<Enemy.Enemy>();
                if (enemy != null && enemy.CanBeStunned())
                {
                    SuccessfulCounterAttack();
                }
            }
            if (stateTimer < 0 || triggerCalled)
                stateMachine.ChangeState(player.idleState);
        }

        private void SuccessfulCounterAttack()
        {
            stateTimer = 10; // any value bigger than 1;
            player.anim.SetBool(successfulCounterAnimName, true);
        }
    }
}