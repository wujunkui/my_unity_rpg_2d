namespace Enemy.Skeleton
{
    public class SkeletonMoveState: SkeletonGroundState
    {
        public SkeletonMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
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
            enemy.SetVelocity(enemy.moveSpeed * enemy.facingDirection, rb.velocity.y);
            if(enemy.IsWallDetected() || !enemy.IsGroundedDetected())
            {
                enemy.Flip();
                stateMachine.ChangeState(enemy.idleState);
            }
        }


    }
}