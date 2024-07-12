using UnityEngine;

namespace Stats
{
    public class CharacterStats : MonoBehaviour
    {
        public Stat damage;
        public Stat maxHealth;

        public int currentHealth;

        public System.Action onHealthChange;
        
        protected virtual void Start()
        {
            currentHealth = maxHealth.GetValue();
        
        }

        public virtual void DoDamage(CharacterStats _targetStats)
        {
            int totalDamage = damage.GetValue();
            _targetStats.TakeDamage(totalDamage);
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

        protected virtual void DecreaseHealthBy(int _damage)
        {
            currentHealth -= _damage;
            onHealthChange?.Invoke();
        }

        protected virtual void Die()
        {
        
        }
    
    }
}