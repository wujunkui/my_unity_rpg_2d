using UnityEngine;

namespace Stats
{
    public class CharacterStats : MonoBehaviour
    {
        public Stat damage;
        public Stat maxHealth;

        public int currentHealth;
        
        public Stat critChance;
        // 暴击伤害，这里是提升的百分比
        public Stat critDamage;
        
        public Stat armor;
        // 闪避
        public Stat evasion;
        
        public System.Action onHealthChange;

        protected EntityFX fx;

        private void Awake()
        {
            fx = GetComponent<EntityFX>();
        }

        protected virtual void Start()
        {
            currentHealth = maxHealth.GetValue();
            onHealthChange?.Invoke();
        
        }

        public virtual void DoDamage(CharacterStats _targetStats)
        {
            int totalDamage = damage.GetValue();
            
            bool isCritical = false;
            if (critChance.GetValue() >= Random.Range(0, 100))
            {
                isCritical = true;
                totalDamage = critDamage.GetValue();
            }
            
            _targetStats.TakeDamage(totalDamage, isCritical);
            
        }
        
        /// <summary>
        /// 被伤害多少？
        /// </summary>
        /// <param name="_damage"></param>
        public virtual void TakeDamage(int _damage, bool _isCritical = false)
        {
            DecreaseHealthBy(_damage, _isCritical);
            if (_isCritical)
                fx.CreateCriticalHitFx(transform);
            else
                fx.CreateHitFx(transform);
            if(currentHealth <= 0)
                Die();
        }
        
        protected virtual void PopBeHurtText(int _damage, bool _isCritical = false)
        {
            if(_isCritical)
                fx.CreatePopUpText(_damage.ToString(), Color.yellow, .5f);
            else
            {
                fx.CreatePopUpText(_damage.ToString());
            }
        }

        public virtual void DecreaseHealthBy(int _damage, bool _isCritical)
        {
            currentHealth -= _damage;
            if (_damage > 0)
                PopBeHurtText(_damage, _isCritical);
            onHealthChange?.Invoke();
        }

        public virtual void IncreaseHealthBy(int _incrHP)
        {
            currentHealth += _incrHP;
            if (currentHealth > maxHealth.GetValue())
                currentHealth = maxHealth.GetValue();
            onHealthChange?.Invoke();
        }

        protected virtual void Die()
        {
        
        }

        public int GetMaxHealthValue()
        {
            return maxHealth.GetValue();
        }
    
    }
}