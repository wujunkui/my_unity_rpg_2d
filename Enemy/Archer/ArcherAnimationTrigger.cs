using UnityEngine;

namespace Enemy.Archer
{
    public class ArcherAnimationTrigger: MonoBehaviour
    {
        private EnemyArcher enemy => GetComponentInParent<EnemyArcher>();

        private void AnimationTrigger()
        {
            enemy.AnimationFinishTrigger();
            enemy.EmitArrow();
        }

        private void AttackTrigger()
        {
            
        }
    }
}