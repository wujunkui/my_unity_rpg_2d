using System;
using Items;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace UI
{
    public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler
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


        public void CleanUpSlot()
        {
            item = null;
            itemImage.sprite = null;
            itemImage.color = Color.clear;

            itemText.text = "";
        }


        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if(item.data.itemType != ItemType.Equipment)
                return;
            Inventory.instance.EquipItem(item.data);
        }
    }
}