using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    public class Inventory: MonoBehaviour
    {
        public static Inventory instance;

        public List<InventoryItem> inventoryItems = new List<InventoryItem>();
        public Dictionary<ItemData, InventoryItem> inventoryDict = new();

        public List<InventoryItem> stash = new();
        public Dictionary<ItemData, InventoryItem> stashDict = new();


        [SerializeField] private Transform inventorySlotParent;
        [SerializeField] private Transform stashSlotParent;
        private UI_ItemSlot[] inventoryItemSlots;
        private UI_ItemSlot[] stashItemSlots;
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(instance.gameObject);
            }
        }

        private void Start()
        {
            inventoryItemSlots = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
            stashItemSlots = stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        }

        private void UpdateSlotUI()
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                inventoryItemSlots[i].UpdateSlot(inventoryItems[i]);
            }
            
            for (int i = 0; i < stash.Count; i++)
            {
                stashItemSlots[i].UpdateSlot(stash[i]);
            }
        }

        public void AddItem(ItemData item)
        {
            if (item.itemType == ItemType.Equipment)
                AddToInventory(item);
            else if (item.itemType == ItemType.Material) AddToStash(item);

            UpdateSlotUI();
        }

        private void AddToInventory(ItemData item)
        {
            if (inventoryDict.TryGetValue(item, out InventoryItem value))
            {
                value.AddStack();
            }
            else
            {
                InventoryItem newItem = new InventoryItem(item);
                inventoryItems.Add(newItem);
                inventoryDict.Add(item, newItem);
            }
        }
        
        private void AddToStash(ItemData item)
        {
            if (stashDict.TryGetValue(item, out InventoryItem value))
            {
                value.AddStack();
            }
            else
            {
                InventoryItem newItem = new InventoryItem(item);
                stash.Add(newItem);
                stashDict.Add(item, newItem);
            }
        }
        

        public void RemoveItem(ItemData _item)
        {
            ref var itemDict = ref inventoryDict;
            ref var itemList = ref inventoryItems;
            // if (_item.itemType == ItemType.Material)
            // {
            //     itemDict = ref inventoryDict;
            //     itemList = inventoryItems;
            // }
            if (_item.itemType == ItemType.Equipment)
            {
                itemDict = ref stashDict;
                itemList = ref stash;
            }
            if (!itemDict.TryGetValue(_item, out InventoryItem value)) 
                return;
            if (value.stackSize <= 1)
            {
                itemList.Remove(value);
                itemDict.Remove(_item);
            }
            else
            {
                value.RemoveStack();
            }

            UpdateSlotUI();

        }
    }
}