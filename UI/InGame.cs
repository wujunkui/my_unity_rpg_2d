using System;
using Stats;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace UI
{
    public class InGame : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private PlayerStats playerStats;
        [SerializeField] private TextMeshProUGUI currencyText;
        [SerializeField] private float currencyIncrRate;
        private float currencyShow;
        private void Start()
        {
            if (playerStats != null)
                playerStats.onHealthChange += UpdateHealthUI;
        }

        private void Update()
        {
            if (currencyShow < PlayerManager.instance.currency)
                currencyShow += currencyIncrRate * Time.deltaTime;
            currencyText.text = Mathf.RoundToInt(currencyShow).ToString();
        }

        private void UpdateHealthUI()
        {
            slider.maxValue = playerStats.GetMaxHealthValue();
            slider.value = playerStats.currentHealth;
        }
    }
}