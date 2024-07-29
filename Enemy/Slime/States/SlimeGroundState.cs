using UnityEngine;

namespace Enemy.Slime.States
{
    public class SlimeGroundState: SlimeState
    {
        public SlimeGroundState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, SlimeEnemy enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
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