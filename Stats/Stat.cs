using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Stats
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] private int baseValue;
        public List<int> modifiers;

        public int GetValue()
        {
            int finalVal = baseValue + modifiers.Sum();
        
            return finalVal;
        }

        public void AddModifier(int _modifier)
        {
            modifiers.Add(_modifier);
        }

        public void RemoveModifier(int _modifier)
        {
            modifiers.Remove(_modifier);
        }
    }
}