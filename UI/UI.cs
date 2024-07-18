using UnityEngine;

namespace UI
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private Transform header;
        [SerializeField] private Transform contentParent;

        public ItemToolTip itemToolTip;
        public void SwitchTo(GameObject _menu)
        {
            for (int i = 0; i < contentParent.childCount; i++)
            {
                contentParent.GetChild(i).gameObject.SetActive(false);
            }

            if (_menu != null)
                _menu.SetActive(true);
        }
    }
}