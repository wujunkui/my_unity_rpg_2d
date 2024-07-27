using UnityEngine;

namespace Enemy.Archer.States
{
    public class ArcherDeadState: ArcherState
    {
        public ArcherDeadState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemyArcher _enemy) : base(enemyBase, stateMachine, animBoolName, _enemy)
        {
        }

        public override void Enter()
        {
            base.Enter();
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            enemy.bd.enabled = false;
        }
    }
}