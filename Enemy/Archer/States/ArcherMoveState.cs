namespace Enemy.Archer.States
{
    public class ArcherMoveState: ArcherGroundState
    {
        public ArcherMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemyArcher _enemy) : base(enemyBase, stateMachine, animBoolName, _enemy)
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