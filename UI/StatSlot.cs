using UnityEngine;
using TMPro;
using Stats;
using Utils;


namespace UI
{
    
    public class StatSlot : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI labelText;
        [SerializeField] private TextMeshProUGUI valueText;
        [SerializeField] private Define.StatType statType;

        private PlayerStats playerStats => PlayerManager.instance.playerStats;

        private void OnValidate()
        {
            gameObject.name = "Stats Slot - " + statType.ToString();
            labelText.text = Define.StatTypeZhName[statType];
        }

        private void Start()
        {
            Items.Inventory.instance.onEquipmentChange += UpdateValueText;
            UpdateValueText();

        }

        private void UpdateValueText()
        {
            valueText.text = GetStatValue().ToString();
        }

        public int GetStatValue()
        {
            Stat stat = statType switch
            {
                Define.StatType.Damage => playerStats.damage,
                Define.StatType.CriticalChance => playerStats.critChance,
                Define.StatType.CriticalDamage => playerStats.critDamage,
                Define.StatType.Armor => playerStats.armor,
                Define.StatType.MaxHealth => playerStats.maxHealth,
                Define.StatType.Evasion => playerStats.evasion,
                _ => new Stat()
            };

            return stat.GetValue();
        }
        
        
    }
}