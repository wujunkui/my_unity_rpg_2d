using Enemy.Slime.States;
using UnityEngine;

public enum SlimeType
{
    Big,
    Medium,
    Small
}

namespace Enemy.Slime
{
    public class SlimeEnemy : Enemy
    {

        [SerializeField] private SlimeType slimeType;
        [SerializeField] private int slimeToCreate;
        [SerializeField] private GameObject slimePrefab;
        [SerializeField] private Vector2 minCreateVelocity;
        [SerializeField] private Vector2 maxCreateVelocity;
        
        public SlimeIdleState idleState;
        public SlimeMoveState moveState;
        public SlimeAttackState attackState;
        public SlimeBattleState battleState;
        public SlimeDeadState deadState;
        public SlimeStunnedState stunnedState;

        protected override void Awake()
        {
            base.Awake();
            InitStates();
        }

        protected override void Start()
        {
            base.Start();
            stateMachine.Initialize(idleState);
        }

        private void InitStates()
        {
            idleState = CreateNewState<SlimeIdleState>("Idle");
            moveState = CreateNewState<SlimeMoveState>("Move");
            battleState = CreateNewState<SlimeBattleState>("Move");
            attackState = CreateNewState<SlimeAttackState>("Attack");
            deadState = CreateNewState<SlimeDeadState>("Dead");
            stunnedState = CreateNewState<SlimeStunnedState>("Stunned");
        }
        
        public override void Die()
        {
            base.Die();
            stateMachine.ChangeState(deadState);
            if(slimeType == SlimeType.Small)
            {
                Destroy(gameObject);
                return;
            };
            
            CreateSlimes(slimeToCreate, slimePrefab);
            Destroy(gameObject);
        }
        
        
        public override bool CanBeStunned()
        {
            if (base.CanBeStunned())
            {
                stateMachine.ChangeState(stunnedState);
                return true;
            }

            return false;
        }

        public void CreateSlimes(int _amountSlimes, GameObject _slimePrefab)
        {
            for (int i = 0; i < _amountSlimes; i++)
            {
                GameObject newSlime = Instantiate(_slimePrefab, transform.position, Quaternion.identity);
                newSlime.GetComponent<SlimeEnemy>().SetupSlime(facingDirection);
            }
        }

        public void SetupSlime(int _facingDir)
        {
            if(_facingDir != facingDirection)
                Flip();
            float xVelocity = Random.Range(minCreateVelocity.x, maxCreateVelocity.x);
            float yVelocity = Random.Range(minCreateVelocity.y, maxCreateVelocity.y);
            isKnocked = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity * - facingDirection, yVelocity);
            Invoke("CancelKnockback", 1.5f);
        }

        private void CancelKnockback() => isKnocked = false;
    }
}