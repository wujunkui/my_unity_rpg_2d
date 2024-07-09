namespace Enemy.Skeleton
{
    public class SkeletonAttackState: SkeletonState
    {
        public SkeletonAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
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
            enemy.SetZeroVelocity();
            if(triggerCalled)
                stateMachine.ChangeState(enemy.battleState);
        }
    }
}