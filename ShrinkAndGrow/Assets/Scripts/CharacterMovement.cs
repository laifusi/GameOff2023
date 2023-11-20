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
    [SerializeField] float secondsToWait;

    private Rigidbody2D rb;
    private float horizontal;
    private bool mustJump;
    private CharacterInventory inventory;
    private Animator animator;
    private bool isGrounded;
    private SpriteRenderer spriteRenderer;
    private int growthValue;

    private IEnumerator Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<CharacterInventory>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        yield return null;

        growthValue = PlayerPrefs.GetInt("GrowthValue", 0);
        Debug.Log(growthValue);

        if (growthValue < 0)
            StartCoroutine(Shrink());
        else if (growthValue > 0)
            StartCoroutine(Grow());
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
            StartCoroutine(Shrink());
            inventory.DrinkPotion();
        }

        if(Input.GetKeyDown(KeyCode.O) && inventory.HasOrange())
        {
            StartCoroutine(Grow());
            inventory.EatOrange();
        }
    }

    private IEnumerator Shrink()
    {
        growthValue -= 1;
        if (growthValue < -1)
            growthValue = -1;

        var currentScale = transform.localScale;
        var currentJumpForce = jumpForce;
        var currentWalkVelocity = walkVelocity;
        var currentGravityScale = rb.gravityScale;
        var currentCamSize = cam.orthographicSize;
        float iterator = 0;
        while (iterator < 2)
        {
            iterator += 0.1f;
            transform.localScale = new Vector3(Mathf.Lerp(currentScale.x, currentScale.x / 2, iterator), Mathf.Lerp(currentScale.y, currentScale.y / 2, iterator), 1);
            jumpForce = Mathf.Lerp(currentJumpForce, currentJumpForce / 2, iterator);
            walkVelocity = Mathf.Lerp(currentWalkVelocity, currentWalkVelocity / 2, iterator);
            rb.gravityScale = Mathf.Lerp(currentGravityScale, currentGravityScale / 2, iterator);
            cam.orthographicSize = Mathf.Lerp(currentCamSize, currentCamSize / 2, iterator);
            yield return new WaitForSeconds(secondsToWait);
        }
    }

    private IEnumerator Grow()
    {
        growthValue += 1;
        if (growthValue > 1)
            growthValue = 1;

        var currentScale = transform.localScale;
        var currentJumpForce = jumpForce;
        var currentWalkVelocity = walkVelocity;
        var currentGravityScale = rb.gravityScale;
        var currentCamSize = cam.orthographicSize;
        float iterator = 0;
        while(iterator < 2)
        {
            iterator += 0.1f;
            transform.localScale = new Vector3(Mathf.Lerp(currentScale.x, currentScale.x*2, iterator), Mathf.Lerp(currentScale.y, currentScale.y * 2, iterator), 1);
            jumpForce = Mathf.Lerp(currentJumpForce, currentJumpForce*2, iterator);
            walkVelocity = Mathf.Lerp(currentWalkVelocity, currentWalkVelocity * 2, iterator);
            rb.gravityScale = Mathf.Lerp(currentGravityScale, currentGravityScale * 2, iterator);
            cam.orthographicSize = Mathf.Lerp(currentCamSize, currentCamSize * 2, iterator);
            yield return new WaitForSeconds(secondsToWait);
        }
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

    private void OnDisable()
    {
        PlayerPrefs.SetInt("GrowthValue", growthValue);
    }
}
