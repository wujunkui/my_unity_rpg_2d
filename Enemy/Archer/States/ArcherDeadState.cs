namespace Enemy.Archer.States
{
    public class ArcherDeadState: ArcherState
    {
        public ArcherDeadState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemyArcher _enemy) : base(enemyBase, stateMachine, animBoolName, _enemy)
        {
        }
    }
}