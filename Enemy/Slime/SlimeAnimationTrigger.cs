using Stats;
using UnityEngine;

namespace Enemy.Slime
{
    public class SlimeAnimationTrigger : MonoBehaviour
    {
        private SlimeEnemy enemy => GetComponentInParent<SlimeEnemy>();

        private void AnimationTrigger()
        {
            enemy.AnimationFinishTrigger();
        }

        private void AttackTrigger()
        {
            var colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackDistance);
            foreach (var hit in colliders)
            {
                var player = hit.GetComponent<Player.Player>();
                if(player != null)
                {
                    PlayerStats target = hit.GetComponent<PlayerStats>();
                    enemy.stats.DoDamage(target);
                }
            }
        }

        private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();

        private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
    }
}