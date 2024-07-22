using System.Collections.Generic;
using Items;

namespace SaveSystem
{
    [System.Serializable]
    public class GameData
    {
        public int currency = 0;
        public SerializableDictionary<string, int> inventory = new();
        public List<string> equipmentIds = new();
    }
}