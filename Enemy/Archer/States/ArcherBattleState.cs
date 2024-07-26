using UnityEngine;
namespace Enemy.Archer.States
{
    public class ArcherBattleState: ArcherState
    {
        private int moveDir;
        private float jumpTimer;
        public ArcherBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemyArcher _enemy) : base(enemyBase, stateMachine, animBoolName, _enemy)
        {
        }
        
        public override void Update()
        {
            base.Update();
            jumpTimer -= Time.deltaTime;
            
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
            
            if (player.transform.position.x > enemy.transform.position.x && enemy.facingDirection == -1)
                enemy.Flip();
            if (player.transform.position.x < enemy.transform.position.x && enemy.facingDirection == 1)
                enemy.Flip();

            if (jumpTimer > 0)
                return;
            if (playerDetected && playerDetected.distance < 3f && enemy.FallTargetIsGround())
            {
                jumpTimer = enemy.jumpCoolDown;
                stateMachine.ChangeState(enemy.jumpState);
            }
      
            // enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
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