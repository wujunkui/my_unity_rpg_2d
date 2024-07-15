using Items;
using UnityEngine.EventSystems;

namespace UI
{
    public class UI_EquipmentSlot : UI_ItemSlot
    {
        public EquipmentType slotType;


        private void OnValidate()
        {
            gameObject.name = "Equipment slot - " + slotType.ToString();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            Inventory.instance.UnequipItem(item.data as ItemData_Equipment);
            CleanUpSlot();
        }
    }
}