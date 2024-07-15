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

        public int damage;
        
        public int critChance;
        public int critDamage;


        public int health;

        public int armor;
        // 闪避
        public int evasion;
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
    }
}