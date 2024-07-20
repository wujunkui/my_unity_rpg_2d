using Items.Effects;
using Stats;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask
}
namespace Items
{
    [CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
    public class ItemData_Equipment: ItemData
    {
        public EquipmentType equipmentType;
        
        public ItemEffect[] itemEffects;
        
        public int damage;
        public int critChance;
        public int critDamage;

        public int health;

        public int armor;
        // 闪避
        public int evasion;

        public ItemData_Equipment()
        {
            itemType = ItemType.Equipment;
        }

        public void AddModifies()
        {
            PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
            
            playerStats.damage.AddModifier(damage);
            playerStats.maxHealth.AddModifier(health);
            playerStats.armor.AddModifier(armor);
            playerStats.evasion.AddModifier(evasion);
            playerStats.critChance.AddModifier(critChance);
            playerStats.critDamage.AddModifier(critDamage);
        }

        public void RemoveModifies()
        {
            PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
            playerStats.damage.RemoveModifier(damage);
            playerStats.maxHealth.RemoveModifier(health);
            playerStats.armor.RemoveModifier(armor);
            playerStats.evasion.RemoveModifier(evasion);
            playerStats.critChance.RemoveModifier(critChance);
            playerStats.critDamage.RemoveModifier(critDamage);
        }

        public void ExecuteEffect(int _damage)
        {
            foreach (var itemEffect in itemEffects)
            {
                itemEffect.ExecuteEffect(_damage);
            }
        }

        public override string GetDescription()
        {
            sb.Length = 0;
            AddItemDescription(damage, "伤害");
            AddItemDescription(critChance, "暴击");
            AddItemDescription(critDamage, "暴击伤害");
            AddItemDescription(health, "最大血量");
            return sb.ToString();
        }

        private void AddItemDescription(int _value, string _name)
        {
            if (_value != 0)
            {
                if (sb.Length > 0)
                    sb.AppendLine();
                if (_value > 0)
                    sb.Append("+ " + _value + "  "  + _name);
            }
        }
    }
}