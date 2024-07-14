using System;
using Items;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class UI_ItemSlot : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI itemText;

        public InventoryItem item;

        public void UpdateSlot(InventoryItem _newItem)
        {
            item = _newItem;
            itemImage.color = Color.white;
            if (item == null) return;
            itemImage.sprite = item.data.icon;
            itemText.text = item.stackSize > 1 ? item.stackSize.ToString() : "";

        }
    }
}