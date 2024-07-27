using Stats;
using UnityEngine;

namespace Controllers
{
    public class ArrowController : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private string targetLayerName = "Player";
        [SerializeField] private float xVelocity;
        [SerializeField] private bool worked;
        [SerializeField] private bool flipped;
        private Rigidbody2D rb;
        private CapsuleCollider2D cd;
        private ParticleSystem particle;
        private SpriteRenderer sr;
        private bool toDestroy;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            cd = GetComponent<CapsuleCollider2D>();
            particle = GetComponentInChildren<ParticleSystem>();
            sr = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            // particle.startLifetime = 1 / xVelocity * 0.5f;
        }

        private void Update()
        {
            rb.velocity = new Vector2(xVelocity, rb.velocity.y);
            if (toDestroy)
                FadeDestroy();
        }

        public void SetDamage(int _damage)
        {
            damage = _damage;
        }

        public void SetArrowDirection(bool _toRight)
        {
            if (!_toRight)
                xVelocity = -xVelocity;
        }

        public void FlipArrow()
        {
            if(flipped)
                return;
            xVelocity = xVelocity * -1;
            flipped = true;
            transform.Rotate(0, 180, 0);
            targetLayerName = "Enemy";
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(targetLayerName))
            {
                other.GetComponent<CharacterStats>()?.TakeDamage(damage);
                StuckInto(other);
            } 
            else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                StuckInto(other);
            }
        }

        private void StuckInto(Collider2D other)
        {
            particle.Stop();
            cd.enabled = false;
            transform.Rotate(0,0, Random.Range(-30f, 0));
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            transform.parent = other.transform;
            Invoke("setToDestroy", 2);
        }

        private void setToDestroy() => toDestroy = true;

        private void FadeDestroy()
        {
            float alpha = sr.color.a - 2 * Time.deltaTime;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
            
            if(sr.color.a <= 0)
                Destroy(gameObject);
            
        }
    }
}