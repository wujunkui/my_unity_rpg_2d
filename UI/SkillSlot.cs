using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class SkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        public string skillName;
        public string skillNameZh;
        public string[] skillDesc;
        public int cost;
        public bool unLocked;
        [SerializeField] private Image image;
        private readonly Color lockedColor = new Color(0.5f,0.5f,0.5f);
        [SerializeField] private UI ui;

        private void Start()
        {
            ui = GetComponentInParent<UI>();
            image = GetComponent<Image>();
            SetColor();
        }

        private void SetColor()
        {
            image.color = unLocked ? Color.white : lockedColor;
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
            // todo check currency 
            unLocked = true;
            // todo play audio
            SetColor();
        }
    }
}