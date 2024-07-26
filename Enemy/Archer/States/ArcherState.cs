using UnityEngine;
namespace Enemy.Archer.States
{
    public class ArcherState: EnemyState
    {
        protected Transform player;
        protected EnemyArcher enemy;


        public ArcherState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemyArcher _enemy) : base(enemyBase, stateMachine, animBoolName)
        {
            enemy = _enemy;
        }

        public override void Enter()
        {
            base.Enter();
            player = PlayerManager.instance.player.transform;
        }
    }
}