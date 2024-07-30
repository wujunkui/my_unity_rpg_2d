namespace Enemy.Slime.States
{
    public class SlimeStunnedState:SlimeState
    {
        public SlimeStunnedState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, SlimeEnemy enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            
            enemy.fx.InvokeRepeating("RedColorBlink", 0, .1f);
            stateTimer = enemy.stunDuration;
            // enemy.rb.velocity = new Vector2(-enemy.facingDirection * enemy.stunDirection.x, enemy.stunDirection.y);
            enemy.SetVelocity(-enemy.facingDirection * enemy.stunDirection.x, enemy.stunDirection.y, false);
        }

        public override void Exit()
        {
            base.Exit();
            enemy.fx.Invoke("CancelRedBlink", 0);
        }

        public override void Update()
        {
            base.Update();
            if (stateTimer < 0)
                stateMachine.ChangeState(enemy.battleState);
        }
    }
}