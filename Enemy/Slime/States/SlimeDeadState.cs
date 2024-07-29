namespace Enemy.Slime.States
{
    public class SlimeDeadState: SlimeState
    {
        public SlimeDeadState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, SlimeEnemy enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
        {
        }
        
        
    }
}