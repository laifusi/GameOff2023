using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float jumpForce = 100;
    [SerializeField] float walkVelocity = 10;
    [SerializeField] Camera cam;

    private Rigidbody2D rb;
    private float horizontal;
    private bool mustJump;
    private CharacterInventory inventory;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<CharacterInventory>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        animator.SetBool("isWalking", horizontal != 0);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            mustJump = true;
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
