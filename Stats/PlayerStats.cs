
using UnityEngine;

namespace Stats
{
    public class PlayerStats: CharacterStats
    {
        private Player.Player player;

        protected override void Start()
        {
            base.Start();
            player = GetComponent<Player.Player>();
        }

        public override void TakeDamage(int _damage)
        {
            base.TakeDamage(_damage);
            player.DamageEffect();
        }

        public override void DoDamage(CharacterStats _targetStats)
        {
            base.DoDamage(_targetStats);
            int totalDamage = damage.GetValue();
            Items.Inventory.instance.GetEquipment(EquipmentType.Weapon).ExecuteEffect(totalDamage);
        }

        protected override void Die()
        {
            base.Die();
            player.Die();
        }

        protected override void PopBeHurtText(int _damage)
        {
            fx.CreatePopUpText(_damage.ToString(), Color.yellow);
        }

        public void PopHealText(int _healPoint)
        {
            fx.CreatePopUpText($"+{_healPoint}", Color.green);
        }
    }
}