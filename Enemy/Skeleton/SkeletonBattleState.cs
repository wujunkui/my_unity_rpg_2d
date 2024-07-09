using UnityEngine;

namespace Enemy.Skeleton
{
    public class SkeletonBattleState: SkeletonState
    {
        private Player.Player player;
        private int moveDir;
        public SkeletonBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player = PlayerManager.instance.player;

        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            var playerDetected = enemy.IsPlayerDetected();
            if (playerDetected)
            {
                if (playerDetected.distance < enemy.attackDistance)
                {
                    stateMachine.ChangeState(enemy.attackState);
                }
            }
            
            // moveDir = player.transform.position.x > enemy.transform.position.x ? 1 : -1;
            if (player.transform.position.x > enemy.transform.position.x)
                moveDir = 1;
            else if (player.transform.position.x < enemy.transform.position.x)
                moveDir = -1;
            enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
        }
    }
}