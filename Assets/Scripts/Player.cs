using UnityEngine;

public class Player : Entity
{

    [Header("Movement details")]
    [SerializeField] protected float moveSpeed = 3.5f;
    [SerializeField] private float jumpforce = 8;
    private float xInput;
    private bool canJump = true;

    protected override void Update()
    {
        base.Update();
        HandleInput();
    }


    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
            TryToJump();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            HandleAttack();
    }


    protected override void HandleMovement()
    {
        if (canMove == true)
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }


    private void TryToJump()
    {
        if (isGrounded && canJump)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpforce);
    }

    public override void EnableMovement(bool enable)
    {
        base.EnableMovement(enable);
        canJump = enable;
    }

    protected override void Die()
    {
        base.Die();
        UI.instance.EnablegameOverUI();
    }

}
