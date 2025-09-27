using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;


    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpforce = 8;
    private float xInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInput();
        HandleMovement();
    }


        private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

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

    private void HandleMovement()
    {
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpforce);
    }
}
