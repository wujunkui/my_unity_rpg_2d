namespace Enemy.Skeleton
{
    public class SkeletonState: EnemyState
    {
        protected Enemy_Skeleton enemy;
        public SkeletonState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton enemy) : base(enemyBase, stateMachine, animBoolName)
        {
            this.enemy = enemy;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        
    }
}