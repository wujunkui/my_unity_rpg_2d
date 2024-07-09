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
    }
}