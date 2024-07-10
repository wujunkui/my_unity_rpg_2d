using UnityEngine;

namespace Enemy.Skeleton
{
    public class SkeletonAnimationTrigger : MonoBehaviour
    {
        private Enemy_Skeleton enemy => GetComponentInParent<Enemy_Skeleton>();

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
                if(player != null) player.Damage();
            }
        }

        private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();

        private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
    }
}