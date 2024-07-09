using UnityEngine;

namespace Enemy.Skeleton
{
    public class SkeletonStunnedState: SkeletonState
    {
        
        public SkeletonStunnedState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
        {
        }

        public override void Enter()
        {
            base.Enter();
            stateTimer = enemy.stunDuration;
            enemy.SetVelocity(-enemy.facingDirection * enemy.stunDirection.x, enemy.stunDirection.y);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if (stateTimer < 0)
                stateMachine.ChangeState(enemy.idleState);
        }
    }
}