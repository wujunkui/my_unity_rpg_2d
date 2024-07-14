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
    }
}