using UnityEngine;

namespace Enemy.Archer.States
{
    public class ArcherJumpState: ArcherState
    {
        public ArcherJumpState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemyArcher _enemy) : base(enemyBase, stateMachine, animBoolName, _enemy)
        {
        }

        public override void Enter()
        {
            base.Enter();
            enemy.rb.velocity = new Vector2(enemy.jumpVelocity.x * -enemy.facingDirection, enemy.jumpVelocity.y);
        }

        public override void Update()
        {
            base.Update();
            enemy.anim.SetFloat("yVelocity", rb.velocity.y);
            if(rb.velocity.y == 0 && enemy.IsGroundedDetected())
                enemy.stateMachine.ChangeState(enemy.battleState);
        }
    }
}