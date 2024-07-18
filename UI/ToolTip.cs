using UnityEngine;

namespace UI
{
    public class ToolTip : MonoBehaviour
    {
        [SerializeField] private float xLimit = 960;
        [SerializeField] private float yLimit = 540;
        [SerializeField] private float xOffset = 150;
        [SerializeField] private float yOffset = 150;
        
        
        public virtual void AdjustPosition()
        {
            Vector2 mousePosition = Input.mousePosition;
            float newXOffset = mousePosition.x > 600 ? -xOffset : xOffset;
            float newYOffset = mousePosition.y > 320 ? -yOffset : yOffset;
            transform.position = new Vector3(mousePosition.x + newXOffset, mousePosition.y + newYOffset);
        }
        
        public virtual void HideToolTip() => gameObject.SetActive(false);
    }
}