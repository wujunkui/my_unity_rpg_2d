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
            [EquipmentType.Armor] = "防具",
            [EquipmentType.Amulet] = "饰品",
            [EquipmentType.Flask] = "药剂"
        };


        public void ShowToolTip(ItemData_Equipment item)
        {
            itemNameText.text = item.itemNameCh;
            itemTypeText.text = equipmentZhNameDict[item.equipmentType];
            gameObject.SetActive(true);
        }
    }
}