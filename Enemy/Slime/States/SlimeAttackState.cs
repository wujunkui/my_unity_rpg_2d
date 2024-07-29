using UnityEngine;

namespace Enemy.Slime.States
{
    public class SlimeAttackState: SlimeState
    {
        public SlimeAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, SlimeEnemy enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
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