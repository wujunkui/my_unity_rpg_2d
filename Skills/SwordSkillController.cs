using System.Collections.Generic;
using Stats;
using UnityEngine;

namespace Skills
{
    public class SwordSkillController : MonoBehaviour
    {
        private Animator anim;
        private Rigidbody2D rb;
        private CircleCollider2D cd;
        private Player.Player player;
         
        private int pierceAmount;
        
        // spin sword info
        private float maxTravelDistance;
        private float spinDuration;
        private float spinTimer;
        private bool wasStopped;
        private bool isSpinning;
        private float hitCooldown;
        private float hitTimer;

        [SerializeField] private float returnSpeed = 1;
        private bool canRotate = true;
        private bool isReturning = false;
        [SerializeField] private int swordDamage = 10;

        private ParticleSystem dustFx;
        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
            cd = GetComponent<CircleCollider2D>();
            dustFx = GetComponentInChildren<ParticleSystem>();
        }

        public void SetupSword(Vector2 _dir, float _gravityScale, Player.Player _player, float _returnSpeed)
        {
            rb.velocity = _dir;
            rb.gravityScale = _gravityScale;
            player = _player;
            returnSpeed = _returnSpeed;
            anim.SetBool("Rotation", true);
        }

        public void SetupPierceSword(int _pierceAmount)
        {
            pierceAmount = _pierceAmount;
        }

        public void SetupSpin(bool _isSpinning, float _maxTravelDistance, float _spinDuration, float _hitDuration)
        {
            isSpinning = _isSpinning;
            maxTravelDistance = _maxTravelDistance;
            spinDuration = _spinDuration;
            hitCooldown = _hitDuration;
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

            if (isSpinning)
            {
                SpinLogic();
            }
        }

        private void SpinLogic()
        {
            if (Vector2.Distance(player.transform.position, transform.position) > maxTravelDistance && !wasStopped)
            {
                StopAndSpin();
            }

            if (wasStopped)
            {
                spinTimer -= Time.deltaTime;
                hitTimer -= Time.deltaTime;
                if (hitTimer < 0)
                {
                    foreach (var enemy in GetClosedEnemies(1))
                    {
                        DamageEnemy(enemy);
                    }

                    hitTimer = hitCooldown;
                }
                // transform.position = new Vector3(transform.position.x)
                
                if (spinTimer < 0)
                {
                    isReturning = true;
                    wasStopped = false;
                    spinTimer = spinDuration;
                }
            }
        }

        private void StopAndSpin()
        {
            wasStopped = true;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            spinTimer = spinDuration;
        }

        private Enemy.Enemy[] GetClosedEnemies(float distance)
        {
            List<Enemy.Enemy> enemies = new();
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, distance);
            foreach (var hit in colliders)
            {
                if(hit.TryGetComponent(out Enemy.Enemy enemy))
                    enemies.Add(enemy);
                    
            }

            return enemies.ToArray();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isReturning)
                return;
            
            DamageEnemy(other);
            StuckInto(other);
        }

        private void DamageEnemy(Enemy.Enemy enemy)
        {
            enemy.GetComponent<CharacterStats>()?.TakeDamage(swordDamage);
        }
        
        private void DamageEnemy(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy.Enemy enemy))
            {
                enemy.GetComponent<CharacterStats>()?.TakeDamage(swordDamage);
            }
        }

        private void StuckInto(Collider2D other)
        {
            if(isSpinning)
            {
                StopAndSpin();   
                return;
            }
            
            if (pierceAmount > 0 && other.GetComponent<Enemy.Enemy>() != null)
            {
                pierceAmount--;
                return;
            }
            
            canRotate = false;
            cd.enabled = false;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            
            dustFx.Play();
            
            transform.parent = other.transform;
            anim.SetBool("Rotation", false);
        }
    }
}
