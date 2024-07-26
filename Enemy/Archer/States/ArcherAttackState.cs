using UnityEngine;
namespace Enemy.Archer.States
{
    public class ArcherAttackState: ArcherState
    {
        public ArcherAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemyArcher _enemy) : base(enemyBase, stateMachine, animBoolName, _enemy)
        {
        }
        
        public override void Exit()
        {
            base.Exit();
            enemy.lastAttackTime = Time.time;
        }

        public override void Update()
        {
            base.Update();
            enemy.SetZeroVelocity();
            if (triggerCalled)
            {
                stateMachine.ChangeState(enemy.battleState);
            }
                
        }
    }
}