using System;
using UnityEngine;

namespace Items
{
    public class ItemObjectTrigger : MonoBehaviour
    {
        private ItemObject itemObject;

        private void Awake()
        {
            itemObject = GetComponentInParent<ItemObject>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.GetComponent<Player.Player>() == null)
                return;
            itemObject.PickUpItem();
        }
    }
}