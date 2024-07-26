
namespace Enemy.Archer.States
{
    public class ArcherIdleState: ArcherGroundState
    {
        public ArcherIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemyArcher _enemy) : base(enemyBase, stateMachine, animBoolName, _enemy)
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