using System;
using UnityEngine;

namespace Skills
{
    public class Skill : MonoBehaviour
    {
        [SerializeField] protected float cooldown;
        protected float cooldownTimer;

        protected Player.Player player;

        protected virtual void Start()
        {
            player = PlayerManager.instance.player;
        }

        protected virtual void Update()
        {
            cooldownTimer -= Time.deltaTime;
        }

        public virtual bool CanUseSkill()
        {
            if (cooldownTimer < 0)
            {
                UseSkill();
                cooldownTimer = cooldown;
                return true;
            }

            Debug.Log("Skill is cooldown");
            return false;
        }

        public virtual void UseSkill()
        {
        }
    }
}