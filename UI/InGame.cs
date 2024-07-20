using System;
using Stats;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InGame : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private PlayerStats playerStats;

        private void Start()
        {
            if (playerStats != null)
                playerStats.onHealthChange += UpdateHealthUI;
        }

        private void UpdateHealthUI()
        {
            slider.maxValue = playerStats.GetMaxHealthValue();
            slider.value = playerStats.currentHealth;
        }
    }
}