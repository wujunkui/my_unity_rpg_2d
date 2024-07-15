using System;
using UnityEngine;

namespace Items
{
    public class ItemObject : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private ItemData itemData;
        private SpriteRenderer sr;

        private void SetupVisual()
        {
            if(itemData == null)
                return;
            GetComponent<SpriteRenderer>().sprite = itemData.icon;
            gameObject.name = "Item - " + itemData.itemName;
            rb = GetComponent<Rigidbody2D>();
        }

        public void SetupItem(ItemData _item, Vector2 _velocity)
        {
            itemData = _item;
            rb.velocity = _velocity;
            SetupVisual();
        }
        
        public void PickUpItem()
        {
            Inventory.instance.AddItem(itemData);
            Destroy(gameObject);
        }
    }
}