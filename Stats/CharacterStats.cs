using UnityEngine;

namespace Stats
{
    public class CharacterStats : MonoBehaviour
    {
        public Stat damage;
        public Stat maxHealth;

        [SerializeField] private int currentHealth;

        private void Start()
        {
            currentHealth = maxHealth.GetValue();
        
        }

        public virtual void DoDamage(CharacterStats _targetStats)
        {
            int totalDamage = damage.GetValue();
            _targetStats.TakeDamage(totalDamage);
        }

        public virtual void TakeDamage(int _damage)
        {
            currentHealth -= _damage;
        }

        protected virtual void Die()
        {
        
        }
    
    }
}