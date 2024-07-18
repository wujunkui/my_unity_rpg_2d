using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Items
{
    public class ItemObject : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private ItemData itemData;
        private SpriteRenderer sr;

        private void OnValidate()
        {
            SetupVisual();
        }

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
            if(!Inventory.instance.CanAddItem() && itemData.itemType == ItemType.Equipment)
            {
                // 装备栏满了捡不起来就抖动一下
                rb.velocity = new Vector2(0, Random.Range(7, 10));
                return;
            }
            Inventory.instance.AddItem(itemData);
            Destroy(gameObject);
        }
    }
}