using System.Collections.Generic; 
using Items;
using TMPro;
using UnityEngine;



namespace UI
{
    public class ItemToolTip : ToolTip
    {
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI itemTypeText;
        [SerializeField] private TextMeshProUGUI itemDescription;
        private readonly Dictionary<EquipmentType, string> equipmentZhNameDict = new()
        {
            [EquipmentType.Weapon] = "武器",
            [EquipmentType.Armor]  = "防具",
            [EquipmentType.Amulet] = "饰品",
            [EquipmentType.Flask]  = "药剂"
        };

        public string GetItemDesc(ItemData_Equipment item)
        {
            string desc = "";
            desc += $"伤害: +{item.damage}";
            desc += "\n" + $"暴击: +{item.critChance}%";
            desc += "\n" + $"暴击伤害: +{item.critDamage}%";
            return desc;
        }

        public  void ShowToolTip(ItemData_Equipment item, Vector2 position)
        {
            AdjustPosition(position);
            itemNameText.text = item.itemNameCh;
            itemTypeText.text = equipmentZhNameDict[item.equipmentType];
            itemDescription.text = GetItemDesc(item);
            gameObject.SetActive(true);
        }
    }
}