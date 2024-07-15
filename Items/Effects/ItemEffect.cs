using UnityEngine;

namespace Items.Effects
{
    
    public class ItemEffect: ScriptableObject
    {
        public virtual void ExecuteEffect(int _damage)
        {
            Debug.Log("effect executed");
        }
    }
}