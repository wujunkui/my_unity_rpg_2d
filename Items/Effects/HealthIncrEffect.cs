using Stats;
using UnityEngine;
namespace Items.Effects
{
    [CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item-Effect/HealEffect")]
    public class HealthIncrEffect: ItemEffect
    {

        public float percentageHealPoint = 0.1f;
        public override void ExecuteEffect(int _damage)
        {
            PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
            var healPoint = Mathf.RoundToInt(_damage * percentageHealPoint);
            if (healPoint > 0)
            {
                playerStats.IncreaseHealthBy(healPoint);
                playerStats.PopHealText(healPoint);

            }
            

        }
    }
}