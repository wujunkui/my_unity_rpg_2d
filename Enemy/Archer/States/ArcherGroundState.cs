using UnityEngine;

namespace Enemy.Archer.States
{
    public class ArcherGroundState: ArcherState
    {
        public ArcherGroundState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemyArcher _enemy) : base(enemyBase, stateMachine, animBoolName, _enemy)
        {
        }
        
        public override void Update()
        {
            base.Update();
            if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.position) < enemy.alertDistance)
                stateMachine.ChangeState(enemy.battleState);
        }
    }
}