using Items;
using UnityEngine;

namespace Stats
{
    public class EnemyStats: CharacterStats
    {
        private Enemy.Enemy enemy;

        [Header("Level Detail")]
        [SerializeField] public int level = 1;

        [Range(0f, 1f)] 
        [SerializeField] private float percentageModifier = 0.1f;

        private ItemDrop myDropSystem;
        
        protected override void Start()
        {
            ApplyLevelModifiers();
            base.Start();
            enemy = GetComponent<Enemy.Enemy>();
            myDropSystem = GetComponent<ItemDrop>();
        }

        private void ApplyLevelModifiers()
        {
            Modify(damage);
            Modify(maxHealth);
        }

        private void Modify(Stat _stat)
        {
            for (int i = 1; i < level; i++)
            {
                float modifier = _stat.GetValue() * percentageModifier;
                
                _stat.AddModifier(Mathf.RoundToInt(modifier));
            }
        }

        public override void TakeDamage(int _damage)
        {
            base.TakeDamage(_damage);
            enemy.DamageEffect();
        }

        protected override void Die()
        {
            base.Die();
            enemy.Die();
            myDropSystem.GenerateDrop();
        }
    }
}