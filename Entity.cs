using System;
using System.Collections;
using UnityEngine;


public class Entity : MonoBehaviour
{ 
    public Animator anim { get; private set; }
    protected SpriteRenderer sr { get; private set; }

    public EntityFX fx; 
    
    public int facingDirection { get; private set; } = 1;
    protected bool facingRight = true;
    
    public Rigidbody2D rb { get; private set; }
    protected CapsuleCollider2D cd;
    
    [Header("Detected check")] 
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;

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
  
    }

    protected virtual void Update()
    {
        
    }


    public virtual void OnDamage()
    {
        fx.StartCoroutine("FlashFX");
        Debug.Log(gameObject.name + "was onDamaged!");
    }
    
    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    public virtual void SetZeroVelocity()
    {
        rb.velocity = Vector2.zero;
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
        
    protected void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position,
            new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position,
            new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
        
    #endregion
    
}
