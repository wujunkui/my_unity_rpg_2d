
using UnityEngine;

namespace Stats
{
    public class PlayerStats: CharacterStats
    {
        private Player.Player player;
        [SerializeField] private Color hurtTextColor;
        [SerializeField] private Color criticalHurtTextColor;
        protected override void Start()
        {
            base.Start();
            player = GetComponent<Player.Player>();
        }

        public override void TakeDamage(int _damage, bool _isCritical = false)
        {
            base.TakeDamage(_damage, _isCritical);
            player.DamageEffect();
        }

        public override void DoDamage(CharacterStats _targetStats)
        {
            base.DoDamage(_targetStats);
            int totalDamage = damage.GetValue();
            Items.Inventory.instance.GetEquipment(EquipmentType.Weapon)?.ExecuteEffect(totalDamage);
        }

        protected override void Die()
        {
            base.Die();
            player.Die();
        }

        protected override void PopBeHurtText(int _damage, bool _isCritical = false)
        {
            if(_isCritical)
                fx.CreatePopUpText(_damage.ToString(), criticalHurtTextColor, 1f);
            else
            {
                fx.CreatePopUpText(_damage.ToString(), hurtTextColor);
            }
        }

        public void PopHealText(int _healPoint)
        {
            fx.CreatePopUpText($"+{_healPoint}", Color.green);
        }
    }
}