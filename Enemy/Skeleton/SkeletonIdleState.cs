namespace Enemy.Skeleton
{
    public class SkeletonIdleState : SkeletonGroundState
    {
        public SkeletonIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName,
            Enemy_Skeleton enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
        {
        }

        public override void Enter()
        {
            base.Enter();
            stateTimer = enemy.idleTime;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if(stateTimer < 0)
                stateMachine.ChangeState(enemy.moveState);
        }
    }
}