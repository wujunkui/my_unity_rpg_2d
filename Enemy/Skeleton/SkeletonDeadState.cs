using UnityEngine;

namespace Enemy.Skeleton
{
    public class SkeletonDeadState: SkeletonState
    {
        public SkeletonDeadState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
        {
        }

        public override void Enter()
        {
            base.Enter();
            enemy.anim.SetBool(enemy.lastAnimName, true);
            enemy.anim.speed = 0;
            enemy.bd.enabled = false; // 移除碰撞

            stateTimer = .1f;

        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if (stateTimer > 0)
                rb.velocity = new Vector2(0, 10);
        }
    }
}