using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected SpriteRenderer sr;

    [Header("Health")]
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private int currentHealth;
    [SerializeField] private Material damageMaterial;
    [SerializeField] private float damageFeedbackDuration = .2f;
    private Coroutine damageFeedbackCoroutine;

    // public Collider2D[] colliders; // set size, when you create array. // cannot add or remove items on the go // faster
    // public List<Collider2D> exampleList; // can have dynamic size. // CAN add or remove items on the go // slower


    [Header("Attack details")]
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask whatIsTarget;

    //[Header("Movement details")]
    //[SerializeField] protected float moveSpeed = 3.5f;
    //[SerializeField] private float jumpforce = 8;

    [Header("Collision details")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask WhatIsGround;
    protected bool isGrounded;

    // Facing direction details
    protected int facingDir = 1;
    protected bool canMove = true;
    protected bool facingRight = true;
    //private bool canJump = true;
    //private float xInput;


    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();

        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {
        HandleCollision();
        // HandleInput();
        HandleMovement();
        HandleAnimations();
        HandleFlip();
    }

    public void DamageTargets()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsTarget);

        foreach (Collider2D enemy in enemyColliders)
        {
            Entity entityTarget = enemy.GetComponent<Entity>();
            entityTarget.TakeDamage();



            //string enemyName = enemyScript.GetEnemyName();

            //Debug.Log("I damaged enemy : " + enemyName);
        }
    }

    private void TakeDamage()
    {
        currentHealth = currentHealth - 1;
        PlayDamageFeedback();

        if (currentHealth <= 0)
            Die();

        // throw new NotImplementedException();
    }

    protected virtual void Die()
    {
        anim.enabled = false;
        col.enabled = false;

        rb.gravityScale = 12;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 15);

        Destroy(gameObject, 3);
    }

    private void PlayDamageFeedback()
    {
        if (damageFeedbackCoroutine != null)
            StopCoroutine(damageFeedbackCoroutine);

        StartCoroutine(DamageFeedbackCo());
    }

    private IEnumerator DamageFeedbackCo()
    {
        Material originalMat = sr.material;

        sr.material = damageMaterial;

        yield return new WaitForSeconds(damageFeedbackDuration);

        sr.material = originalMat;
    }


    public virtual void EnableMovement(bool enable)
    {
        canMove = enable;
        //canJump = enable;
    }

    protected void HandleAnimations()
    {
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    //private void HandleInput()
    //{
    //    xInput = Input.GetAxisRaw("Horizontal");

    //    if (Input.GetKeyDown(KeyCode.Space))
    //        TryToJump();

    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //        HandleAttack();
    //}

    // else if (Input.GetKeyDown(KeyCode.UpArrow))
    // {
    //     rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpforce);
    // }
    // else
    // {
    //     Debug.Log("Nobody is pressing Jump");
    // }


    // if ( condition )
    // {
    //     display information
    // }


    protected virtual void HandleAttack()
    {
        if (isGrounded)
            anim.SetTrigger("attack");
    }


    //private void TryToJump()
    //{
    //    if (isGrounded && canJump)
    //        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpforce);
    //}

    protected virtual void HandleMovement()
    {
        //if (canMove == true)
        //    rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        //else
        //    rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }


    protected virtual void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, WhatIsGround);
    }

    protected virtual void HandleFlip()
    {
        if (rb.linearVelocity.x > 0 && facingRight == false)
            Flip();
        else if (rb.linearVelocity.x < 0 && facingRight == true)
            Flip();
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDir = facingDir * -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));

        if (attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);

    }

}
