namespace Enemy
{
    public class EnemyIdleState: EnemyState
    {
        public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
        {
        }
    }
}