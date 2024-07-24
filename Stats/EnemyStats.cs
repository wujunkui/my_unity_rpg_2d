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
        [SerializeField] private Stat dropCurrency;
        
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
            Modify(dropCurrency);
            Modify(critChance); // todo fix 暴击都超100了
            Modify(critDamage);
        }

        private void Modify(Stat _stat)
        {
            float totalModify = _stat.GetValue();
            for (int i = 1; i < level; i++)
            {
                float modifier = totalModify * percentageModifier;
                totalModify += modifier;

            }
            _stat.AddModifier(Mathf.RoundToInt(totalModify));
        }

        public override void TakeDamage(int _damage, bool _isCritical = false)
        {
            base.TakeDamage(_damage, _isCritical);
            enemy.DamageEffect();
        }

        protected override void Die()
        {
            base.Die();
            enemy.Die();
            myDropSystem.GenerateDrop();
            // todo 添加金钱掉落特效
            PlayerManager.instance.currency += dropCurrency.GetValue();
        }
    }
}