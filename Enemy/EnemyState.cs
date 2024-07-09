using UnityEngine;

namespace Enemy
{
    public class EnemyState
    {
        protected Enemy enemyBase;
        protected EnemyStateMachine stateMachine;
        private string animBoolName;
        protected float stateTimer;
        protected bool triggerCalled;
        public Rigidbody2D rb;
        

        public EnemyState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName)
        {
            this.enemyBase = enemyBase;
            this.stateMachine = stateMachine;
            this.animBoolName = animBoolName;
        }

        public virtual void Enter()
        {
            enemyBase.anim.SetBool(animBoolName, true);
            rb = enemyBase.rb;
        }

        public virtual void Exit()
        {
            enemyBase.anim.SetBool(animBoolName, false);
        }

        public virtual void Update()
        {
            stateTimer -= Time.deltaTime;

        }

        public virtual void AnimationFinishTrigger()
        {
            triggerCalled = true;
        }
    }
}