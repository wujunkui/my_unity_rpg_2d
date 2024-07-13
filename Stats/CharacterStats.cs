using UnityEngine;

namespace Stats
{
    public class CharacterStats : MonoBehaviour
    {
        public Stat damage;
        public Stat maxHealth;

        public int currentHealth;

        public System.Action onHealthChange;

        protected EntityFX fx;

        private void Awake()
        {
            fx = GetComponent<EntityFX>();
        }

        protected virtual void Start()
        {
            currentHealth = maxHealth.GetValue();
        
        }

        public virtual void DoDamage(CharacterStats _targetStats)
        {
            int totalDamage = damage.GetValue();
            _targetStats.TakeDamage(totalDamage);
            fx.CreateHitFx(_targetStats.transform);
        }
        
        /// <summary>
        /// 被伤害多少？
        /// </summary>
        /// <param name="_damage"></param>
        public virtual void TakeDamage(int _damage)
        {
            DecreaseHealthBy(_damage);
            if(currentHealth <= 0)
                Die();
        }

        protected virtual void PopBeHurtText(int _damage)
        {
            fx.CreatePopUpText(_damage.ToString());
        }

        protected virtual void DecreaseHealthBy(int _damage)
        {
            currentHealth -= _damage;
            if (_damage > 0)
                PopBeHurtText(_damage);
            onHealthChange?.Invoke();
        }

        protected virtual void Die()
        {
        
        }
    
    }
}