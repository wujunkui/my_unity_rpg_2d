using System;
using Stats;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        private Entity entity;
        private CharacterStats myStats;
        private RectTransform myTransform;
        private Slider slider;
        private void Start()
        {
            entity = GetComponentInParent<Entity>();
            myTransform = GetComponent<RectTransform>();
            slider = GetComponentInChildren<Slider>();
            myStats = GetComponentInParent<CharacterStats>();
            
            entity.onFlipped += FlipUI;  // 委托
            myStats.onHealthChange += UpdateHealthUI;
            UpdateHealthUI();
        }
        

        private void UpdateHealthUI()
        {
            slider.maxValue = myStats.maxHealth.GetValue();
            slider.value = myStats.currentHealth;
        }

        private void FlipUI() =>  myTransform.Rotate(0, 180, 0); 

        private void OnDisable()
        {
            entity.onFlipped -= FlipUI;
            myStats.onHealthChange -= UpdateHealthUI;
        }
    }
}