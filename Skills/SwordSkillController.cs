using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Skills
{
    public class SwordSkillController : MonoBehaviour
    {
        private Animator anim;
        private Rigidbody2D rb;
        private CircleCollider2D cd;
        private Player.Player player;

        [SerializeField] private float returnSpeed = 1;
        private bool canRotate = true;
        private bool isReturning = false;
        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
            cd = GetComponent<CircleCollider2D>();
        }

        public void SetupSword(Vector2 _dir, float _gravityScale, Player.Player _player, float _returnSpeed)
        {
            rb.velocity = _dir;
            rb.gravityScale = _gravityScale;
            player = _player;
            returnSpeed = _returnSpeed;
            anim.SetBool("Rotation", true);
        }
        
        public void ReturnSword()
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            anim.SetBool("Rotation", true); // 喜欢回来的时候也转起来，酷
            // anim.CrossFade("SwordFlip", 0);
            // rb.isKinematic = false;
            transform.parent = null;
            isReturning = true;
        }

        private void Update()
        {
            if (canRotate)
                transform.right = rb.velocity;

            if (isReturning)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position,
                    returnSpeed * Time.deltaTime);
                if(Vector2.Distance(transform.position, player.transform.position) < 1)
                    player.CatchTheSword();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isReturning)
                return;
            canRotate = false;
            cd.enabled = false;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            transform.parent = other.transform;
            anim.SetBool("Rotation", false);
        }
    }
}
