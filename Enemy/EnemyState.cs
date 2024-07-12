using Core;
using UnityEngine;

namespace Enemy
{
    public class EnemyState: State
    {
        protected EnemyStateMachine stateMachine;
        protected Enemy enemyBase;
        public EnemyState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName)
        {
            this.enemyBase = enemyBase;
            this.stateMachine = stateMachine;
            this.animBoolName = animBoolName;
        }

        public override void Enter()
        {
            enemyBase.anim.SetBool(animBoolName, true);
            rb = enemyBase.rb;
            triggerCalled = false;
        }

        public override void Exit()
        {
            enemyBase.anim.SetBool(animBoolName, false);
            enemyBase.AssignLastAnimName(animBoolName);
            
        }

        public override void Update()
        {
            stateTimer -= Time.deltaTime;

        }

        public virtual void AnimationFinishTrigger()
        {
            triggerCalled = true;
        }
    }
}