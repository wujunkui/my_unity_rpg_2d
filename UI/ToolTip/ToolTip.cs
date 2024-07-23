using UnityEngine;

namespace UI.ToolTip
{
    public class ToolTip : MonoBehaviour
    {
        [SerializeField] private float xLimit = 960;
        [SerializeField] private float yLimit = 540;
        [SerializeField] private float xOffset = 150;
        [SerializeField] private float yOffset = 150;
        
        
        public virtual void AdjustPosition(Vector2 position)
        {
            float newXOffset = position.x > xLimit ? -xOffset : xOffset;
            float newYOffset = position.y > yLimit ? -yOffset : yOffset;
            transform.position = new Vector3(position.x + newXOffset, position.y + newYOffset);
        }
        
        
        public virtual void HideToolTip() => gameObject.SetActive(false);
    }
}