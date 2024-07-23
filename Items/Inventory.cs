using System.Collections.Generic;
using System.Linq;
using SaveSystem;
using UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    public class Inventory: MonoBehaviour, ISaveManager
    {
        public static Inventory instance;

        public List<ItemData> startingEquipments = new();
        
        public List<InventoryItem> equipments = new ();
        public Dictionary<ItemData_Equipment, InventoryItem> equipmentDict = new();
        
        public List<InventoryItem> inventoryItems = new List<InventoryItem>();
        public Dictionary<ItemData, InventoryItem> inventoryDict = new();
        
        public List<InventoryItem> stash = new();
        public Dictionary<ItemData, InventoryItem> stashDict = new();


        [SerializeField] private Transform inventorySlotParent;
        [SerializeField] private Transform stashSlotParent;
        [SerializeField] private Transform equipmentSlotParent;
        private ItemSlot[] inventoryItemSlots;
        private ItemSlot[] stashItemSlots;
        private EquipmentSlot[] equipmentSlots;
        
        public List<InventoryItem> loadedItems;
        public List<ItemData_Equipment> loadedEquipments;
        
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
            inventoryItemSlots = inventorySlotParent.GetComponentsInChildren<ItemSlot>();
            stashItemSlots = stashSlotParent.GetComponentsInChildren<ItemSlot>();
            equipmentSlots = equipmentSlotParent.GetComponentsInChildren<EquipmentSlot>();
            InitInventory();
        }

        private void InitInventory()
        {
            foreach (var equipment in loadedEquipments)
            {
                EquipItem(equipment);
            }
            
            // 加载存档中的物品
            if (loadedItems.Count > 0)
            {
                foreach (var item in loadedItems)
                {
                    for (int i = 0; i < item.stackSize; i++)
                    {
                        AddItem(item.data);
                    }
                }
                return;
            }
            
            // 没有存档的物品，证明是新开游戏，要添加初始道具
            foreach (var equipment in startingEquipments.Where(equipment => equipment != null))
            {
                AddItem(equipment);
            }
        }

        public void EquipItem(ItemData _item)
        {
            ItemData_Equipment newEquipment = _item as ItemData_Equipment;
            InventoryItem newItem = new InventoryItem(_item);

            ItemData_Equipment oldEquipment = null;
            
            foreach (var item in equipmentDict)
            {
                if (item.Key.equipmentType == newEquipment.equipmentType)
                    oldEquipment = item.Key;
            }
            if (oldEquipment != null)
            {
                UnequipItem(oldEquipment);
            }
            
            
            equipments.Add(newItem);
            equipmentDict.Add(newEquipment, newItem);
            newEquipment.AddModifies();
            RemoveItem(_item);

        }

        public void UnequipItem(ItemData_Equipment equipmentToRemove)
        {
            UnequipItem(equipmentToRemove, true);
        }
        
        public void UnequipItem(ItemData_Equipment equipmentToRemove, bool addToInventory)
        {
            if (equipmentDict.TryGetValue(equipmentToRemove, out InventoryItem value))
            {
                equipments.Remove(value);
                equipmentDict.Remove(equipmentToRemove);
                equipmentToRemove.RemoveModifies();
                AddItem(equipmentToRemove);
            }
            
        }

        private void UpdateSlotUI()
        {
            foreach (var slot in equipmentSlots)
            {
                foreach (var item in equipmentDict)
                {
                    if (item.Key.equipmentType == slot.slotType)
                        slot.UpdateSlot(item.Value);
                }
            }

            foreach (var t in inventoryItemSlots)
            {
                t.CleanUpSlot();
            }
            
            foreach (var t in stashItemSlots)
            {
                t.CleanUpSlot();
            }
            
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
            if (item.itemType == ItemType.Equipment && CanAddItem())
                AddToInventory(item);
            else if (item.itemType == ItemType.Material) 
                AddToStash(item);

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
            if (_item.itemType == ItemType.Material)
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

        public bool CanAddItem()
        {
            if (inventoryDict.Count >= inventoryItemSlots.Length)
            {
                return false;
            }

            return true;
        }
        
        public List<InventoryItem> GetEquipmentList() => equipments;

        public List<InventoryItem> GetStashList() => stash;

        public ItemData_Equipment GetEquipment(EquipmentType _type)
        {
            ItemData_Equipment equipmentItem = null;
            foreach (var item in equipmentDict)
            {
                if (item.Key.equipmentType == _type)
                    equipmentItem = item.Key;
            }

            return equipmentItem;
        }

        public void LoadData(GameData _data)
        {
            foreach (var pair in _data.inventory)
            {
                foreach (var item in GetItemDatabase())
                {
                    if (item != null && item.itemID == pair.Key)
                    {
                        var itemToLoad = new InventoryItem(item);
                        itemToLoad.stackSize = pair.Value;
                        loadedItems.Add(itemToLoad);
                    }
                }
            }

            foreach (var loadedItemId in _data.equipmentIds)
            {
                foreach (var item in GetItemDatabase())
                {
                    if (item != null && loadedItemId == item.itemID)
                    {
                        loadedEquipments.Add(item as ItemData_Equipment);
                    }
                }
            }
            
            
        }

        public void SaveData(ref GameData _data)
        {
            _data.inventory.Clear();
            _data.equipmentIds.Clear();
            foreach (var pair in inventoryDict)
            {
                _data.inventory.Add(pair.Key.itemID, pair.Value.stackSize);
            }

            AddToGameData(ref _data, ref stashDict);
            // foreach (var pair in stashDict)
            // {
            //     _data.inventory.Add(pair.Key.itemID, pair.Value.stackSize);
            // }
            foreach (var pair in equipmentDict)
            {
                _data.equipmentIds.Add(pair.Key.itemID);
            }
        }

        private static void AddToGameData(ref GameData _data, ref Dictionary<ItemData, InventoryItem> _dict)
        {
            foreach (var pair in _dict)
            {
                _data.inventory.Add(pair.Key.itemID, pair.Value.stackSize);
            }
        }

        private List<ItemData> GetItemDatabase()
        {
            List<ItemData> itemDataBase = new();
            string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/Data/Items" });
            foreach (var SOName in assetNames)
            {
                var SOPath = AssetDatabase.GUIDToAssetPath(SOName);
                var itemData = AssetDatabase.LoadAssetAtPath<ItemData>(SOPath);
                itemDataBase.Add(itemData);
            }

            return itemDataBase;
        }
    }
}