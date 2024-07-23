using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class SkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        public string skillName;
        public string skillNameZh;
        public string skillDesc;
        public int cost;
        public bool isLocked;
        [SerializeField] private UI ui;

        private void Start()
        {
            ui = GetComponentInParent<UI>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ui.skillToolTip.ShowToolTip(this, transform.position);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ui.skillToolTip.HideToolTip();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Buy skill");
        }
    }
}