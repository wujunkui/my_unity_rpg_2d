namespace Enemy.Archer.States
{
    public class ArcherFallState: ArcherState
    {
        public ArcherFallState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemyArcher _enemy) : base(enemyBase, stateMachine, animBoolName, _enemy)
        {
        }
    }
}