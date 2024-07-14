using UnityEngine;


public enum ItemType
{
    Material,
    Equipment
}

namespace Items
{
    [CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]
    public class ItemData : ScriptableObject
    {
        public ItemType itemType;
        public string itemName;
        public string itemNameCh;
        public Sprite icon;
        
    }
}