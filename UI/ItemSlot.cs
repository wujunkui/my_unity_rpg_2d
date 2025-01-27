using Items;
using UI.ToolTip;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace UI
{
    public class ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI itemText;
        [SerializeField] private UI ui;
        [SerializeField] private Sprite emptyImage;
        public InventoryItem item;

        private void Start()
        {
            ui = GetComponentInParent<UI>();
        }

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
            itemImage.sprite = emptyImage;
            // itemImage.sprite = null;
            // itemImage.color = Color.clear;

            itemText.text = "";
        }


        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                // todo 右键菜单，丢弃什么的
                return;
            }
            HideItemInfo();
            if(item.data.itemType != ItemType.Equipment)
                return;
            Inventory.instance.EquipItem(item.data);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(item == null || item?.data == null)
                return;
            ShowItemInfo();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(item == null|| item?.data == null)
                return;
            ui.itemToolTip.HideToolTip();
        }

        public void ShowItemInfo()
        {
            
            ui.itemToolTip.ShowToolTip(item.data as ItemData_Equipment, transform.position);
        }

        public void HideItemInfo()
        {
            ui.itemToolTip.HideToolTip();
        }
    }
}