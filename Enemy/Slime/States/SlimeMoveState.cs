namespace Enemy.Slime.States
{
    public class SlimeMoveState: SlimeGroundState
    {
        public SlimeMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, SlimeEnemy enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
        {
        }
        
        public override void Update()
        {
            base.Update();
            enemy.SetVelocity(enemy.moveSpeed * enemy.facingDirection, rb.velocity.y);
            if(enemy.IsWallDetected() || !enemy.IsGroundedDetected())
            {
                enemy.Flip();
                stateMachine.ChangeState(enemy.idleState);
            }
        }
    }
}