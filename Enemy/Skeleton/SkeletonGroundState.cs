namespace Enemy.Skeleton
{
    public class SkeletonGroundState: SkeletonState
    {
        public SkeletonGroundState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
        {
        }
        
        public override void Update()
        {
            base.Update();
            if(enemy.IsPlayerDetected())
                stateMachine.ChangeState(enemy.battleState);
        }
    }
}