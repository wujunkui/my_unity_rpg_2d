using System.Collections;
using Stats;
using UnityEngine;


public class Entity : MonoBehaviour
{ 
    public Animator anim { get; private set; }
    protected SpriteRenderer sr { get; private set; }
    public CharacterStats stats { get; private set; }
    public EntityFX fx; 
    
    public int facingDirection { get; private set; } = 1;
    protected bool facingRight = true;
    
    public Rigidbody2D rb { get; private set; }
    protected CapsuleCollider2D cd;

    [Header("Knockback")] 
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockbackDuration=.2f;
    protected bool isKnocked;
    
    [Header("Collision info")] 
    public Transform attackCheck;
    public float attackCheckRadio = 0.8f;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    
    public System.Action onFlipped;
    
    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        fx = GetComponent<EntityFX>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CapsuleCollider2D>();
        stats = GetComponent<CharacterStats>();
    }

    protected virtual void Update()
    {
        
    }


    public virtual void DamageEffect()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");
        fx.ScreenShake(fx.hitShakePower);
    }

    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;
        rb.velocity = new Vector2(knockbackDirection.x * -facingDirection, knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
    }
    
    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        SetVelocity(_xVelocity, _yVelocity, true);
    }

    public virtual void SetVelocity(float _xVelocity, float _yVelocity, bool _needFlipControl)
    {
        if (isKnocked) return;
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        if (_needFlipControl)
            FlipController(_xVelocity);
    }

    public virtual void SetZeroVelocity()
    {
        SetVelocity(0, 0);
    }
    
    
    #region Collider check

    public virtual bool IsGroundedDetected() =>
        Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection,
        wallCheckDistance, whatIsGround);

    #endregion
    
    #region Flip

    public virtual void Flip()
    {
        facingDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
        onFlipped?.Invoke();
    }

    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();
    }

    #endregion
    
    
    #region Gizmos
        
    protected virtual void OnDrawGizmos()
    {
        if (groundCheck != null)
            Gizmos.DrawLine(groundCheck.position,
                new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        if (wallCheck != null)
            Gizmos.DrawLine(wallCheck.position,
                new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        if (attackCheck != null)
            Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadio);
    }
        
    #endregion


    public virtual void Die()
    {
        
    }



}
