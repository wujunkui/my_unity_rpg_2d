using UnityEngine;

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
            var player = PlayerManager.instance.player.transform;
            if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.position) < enemy.alertDistance)
                stateMachine.ChangeState(enemy.battleState);
        }
    }
}