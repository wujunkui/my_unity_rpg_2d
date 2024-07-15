using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemDrop : MonoBehaviour
    {
        [SerializeField] private int amountOfDrop;
        [SerializeField] private ItemData[] possibleDrops;
        private List<ItemData> dropList = new();
        [SerializeField] private GameObject dropPrefab;
        [SerializeField] private ItemData item;


        public void GenerateDrop()
        {
            foreach (var itemData in possibleDrops)
            {
                if(Random.Range(0, 100) <= itemData.dropChance)
                    dropList.Add(itemData);
            }

            for (int i = 0; i < amountOfDrop; i++)
            {
                if(dropList.Count < 1)
                    break;
                ItemData randomDrop = dropList[Random.Range(0, dropList.Count - 1)];
                dropList.Remove(randomDrop);
                DropItem(randomDrop);
            }
        }
        
        public void DropItem(ItemData _item)
        {
            GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);

            Vector2 randomVelocity = new Vector2(Random.Range(-5, 5), Random.Range(15, 20));
            
            newDrop.GetComponent<ItemObject>().SetupItem(_item, randomVelocity);
        }
    }
}