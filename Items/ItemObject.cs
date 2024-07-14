using UnityEngine;

namespace Items
{
    public class ItemObject : MonoBehaviour
    {
        [SerializeField] private ItemData itemData;
        private SpriteRenderer sr;

        private void OnValidate()
        {
            GetComponent<SpriteRenderer>().sprite = itemData.icon;
            gameObject.name = "Item - " + itemData.itemName;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.GetComponent<Player.Player>() == null)
                return;
            Inventory.instance.AddItem(itemData);
            Destroy(gameObject);
        }
    }
}