using System;
using System.Text;
using UnityEditor;
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
        public string itemID;
        public ItemType itemType;
        public string itemName;
        public string itemNameCh;
        public string itemDesc;
        public Sprite icon;

        private void OnValidate()
        {
#if UNITY_EDITOR
            string path = AssetDatabase.GetAssetPath(this);
            itemID = AssetDatabase.AssetPathToGUID(path);
#endif
        }

        [Range(0, 100)]
        public int dropChance;

        protected StringBuilder sb = new StringBuilder();

        public virtual string GetDescription()
        {
            return "";
        }
    }
}