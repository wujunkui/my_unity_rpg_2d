using UnityEngine;

namespace Enemy.Slime.States
{
    public class SlimeState: EnemyState
    {
        protected SlimeEnemy enemy;
        protected Transform player;
        public SlimeState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, SlimeEnemy enemy) : base(enemyBase, stateMachine, animBoolName)
        {
            this.enemy = enemy;
        }


        public override void Enter()
        {
            base.Enter();
            player = PlayerManager.instance.player.transform;
        }
    }
}