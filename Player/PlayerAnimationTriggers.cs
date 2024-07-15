using Stats;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationTriggers : MonoBehaviour
    {
        private Player player => GetComponentInParent<Player>();
        private void AnimationTrigger()
        {
            player.AnimationTrigger();
        }

        private void AttackTrigger()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadio);
            foreach (var hit in colliders)
            {
                var enemy = hit.GetComponent<Enemy.Enemy>();
                if(enemy != null)
                {
                    EnemyStats target = hit.GetComponent<EnemyStats>();
                    player.stats.DoDamage(target); 
                    
                }
                
            }
        }

        private void ThrowSword()
        {
            SkillManager.instance.sword.CreateSword();
        }
    }
}