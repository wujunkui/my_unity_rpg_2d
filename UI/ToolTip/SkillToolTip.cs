using UnityEngine;
using UnityEngine.UI;
using TMPro;
    
namespace UI.ToolTip
{
    public class SkillToolTip : ToolTip
    {
        [SerializeField] private TextMeshProUGUI skillNameText;
        [SerializeField] private TextMeshProUGUI skillDescription;
        [SerializeField] private TextMeshProUGUI costText;
        

        public void ShowToolTip(SkillSlot skill, Vector2 position)
        {
            AdjustPosition(position);
            skillNameText.text = skill.skillNameZh;
            skillDescription.text = string.Join("\n", skill.skillDesc);
            costText.text = skill.cost.ToString();
            gameObject.SetActive(true);
        }
    }
}