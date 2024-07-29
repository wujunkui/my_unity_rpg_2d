using UnityEngine;

namespace Enemy.Slime.States
{
    public class SlimeBattleState: SlimeState
    {
        private int moveDir;
        public SlimeBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, SlimeEnemy enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
        {
        }
        
        public override void Update()
        {
            base.Update();

            var playerDetected = enemy.IsPlayerDetected();
            if (playerDetected)
            {
                AttackPlayer(playerDetected);
            }
            else
            {
                if(stateTimer < 0 || !enemy.IsGroundedDetected())
                    stateMachine.ChangeState(enemy.idleState);
            }
            
            if (player.transform.position.x > enemy.transform.position.x)
                moveDir = 1;
            else if (player.transform.position.x < enemy.transform.position.x)
                moveDir = -1;
            if (playerDetected && playerDetected.distance < enemy.attackDistance - .5f)
                return;
      
            enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
        }

        private void AttackPlayer(RaycastHit2D playerDetected)
        {
            stateTimer = enemy.battleTime;
            if (playerDetected.distance < enemy.attackDistance)
            {
                if(CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
                    
                    
            }
        }

        private bool CanAttack()
        {
            if (Time.time < enemy.lastAttackTime + enemy.attackCooldown)
                // 攻击未冷却
                return false;
            // enemy.lastAttackTime = Time.time;
            return true;
        }
    }
}