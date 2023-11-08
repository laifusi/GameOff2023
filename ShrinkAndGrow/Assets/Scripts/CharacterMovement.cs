using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float jumpForce = 100;
    [SerializeField] float walkVelocity = 10;
    [SerializeField] Camera cam;
    [SerializeField] Transform feet;
    [SerializeField] LayerMask layerMask;

    private Rigidbody2D rb;
    private float horizontal;
    private bool mustJump;
    private CharacterInventory inventory;
    private Animator animator;
    private bool isGrounded;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<CharacterInventory>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        var hit = Physics2D.OverlapCircle(feet.position, 0.1f, layerMask);
        isGrounded = hit != null;
        animator.SetBool("grounded", isGrounded);

        horizontal = Input.GetAxis("Horizontal");
        animator.SetBool("isWalking", horizontal != 0);
        if(horizontal != 0)
            spriteRenderer.flipX = horizontal < 0;


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            mustJump = true;
            animator.SetTrigger("Jump");
        }

        if(Input.GetKeyDown(KeyCode.P) && inventory.HasPotion())
        {
            Shrink();
            inventory.DrinkPotion();
        }

        if(Input.GetKeyDown(KeyCode.O) && inventory.HasOrange())
        {
            Grow();
            inventory.EatOrange();
        }
    }

    private void Shrink()
    {
        transform.localScale = transform.localScale / 2;
        jumpForce /= 2;
        walkVelocity /= 2;
        rb.gravityScale /= 2;
        cam.orthographicSize /= 2;
    }

    private void Grow()
    {
        transform.localScale = transform.localScale * 2;
        jumpForce *= 2;
        walkVelocity *= 2;
        rb.gravityScale *= 2;
        cam.orthographicSize *= 2;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(walkVelocity*horizontal, rb.velocity.y);

        if (mustJump)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            mustJump = false;
        }
    }
}
