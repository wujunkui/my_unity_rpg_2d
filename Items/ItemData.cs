using System.Text;
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
        public string itemDesc;
        public Sprite icon;
        

        [Range(0, 100)]
        public int dropChance;

        protected StringBuilder sb = new StringBuilder();

        public virtual string GetDescription()
        {
            return "";
        }
    }
}