using UnityEngine;

namespace Enemy
{
    public class EnemyState
    {
        protected Enemy enemy;
        protected EnemyStateMachine stateMachine;
        private string animBoolName;
        protected float stateTimer;
        protected bool triggerCalled;
        

        public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName)
        {
            this.enemy = enemy;
            this.stateMachine = stateMachine;
            this.animBoolName = animBoolName;
        }

        public virtual void Enter()
        {
            enemy.anim.SetBool(animBoolName, true);
        }

        public virtual void Exit()
        {
            enemy.anim.SetBool(animBoolName, false);
        }

        public virtual void Update()
        {
            stateTimer -= Time.deltaTime;

        }
    }
}