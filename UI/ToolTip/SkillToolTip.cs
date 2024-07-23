using UnityEngine;
using UnityEngine.UI;
using TMPro;
    
namespace UI.ToolTip
{
    public class SkillToolTip : ToolTip
    {
        [SerializeField] private TextMeshProUGUI skillNameText;
        [SerializeField] private TextMeshProUGUI itemTypeText;
        [SerializeField] private TextMeshProUGUI skillDescription;
        [SerializeField] private TextMeshProUGUI costText;

        public void ShowToolTip()
        {
            
        }
    }
}