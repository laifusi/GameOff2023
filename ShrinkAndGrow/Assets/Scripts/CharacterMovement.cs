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

    private float horizontal;
    private bool mustJump;
    private bool isGrounded;

    private Rigidbody2D rb;
    private CharacterInventory inventory;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector3 currentScale;
    private float currentJumpForce;
    private float currentWalkVelocity;
    private float currentGravityScale;
    private float currentCamSize;

    private int growthValue;
    private int currentMaxGrowth;
    private int currentMinGrowth;

    private IEnumerator Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<CharacterInventory>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        yield return null;

        growthValue = PlayerPrefs.GetInt("GrowthValue", 0);

        ChangeInitialScale(growthValue);
        SaveCurrentScaleValues();
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

        if(Input.GetKeyDown(KeyCode.P) && inventory.HasPotion() && growthValue > currentMinGrowth)
        {
            StartCoroutine(Shrink());
            inventory.DrinkPotion();
        }

        if(Input.GetKeyDown(KeyCode.O) && inventory.HasOrange() && growthValue < currentMaxGrowth)
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

        SaveCurrentScaleValues();
    }

    private IEnumerator Grow()
    {
        growthValue += 1;
        if (growthValue > 1)
            growthValue = 1;

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

        SaveCurrentScaleValues();
    }

    private void ChangeInitialScale(int growthValue)
    {
        if (growthValue >= 1 && growthValue <= currentMaxGrowth)
        {
            transform.localScale *= 2;
            jumpForce *= 2;
            walkVelocity *= 2;
            rb.gravityScale *= 2;
            cam.orthographicSize *= 2;
        }
        else if (growthValue <= -1 && growthValue >= currentMinGrowth)
        {
            transform.localScale /= 2;
            jumpForce /= 2;
            walkVelocity /= 2;
            rb.gravityScale /= 2;
            cam.orthographicSize /= 2;
        }
    }

    private void SaveCurrentScaleValues()
    {
        currentScale = transform.localScale;
        currentJumpForce = jumpForce;
        currentWalkVelocity = walkVelocity;
        currentGravityScale = rb.gravityScale;
        currentCamSize = cam.orthographicSize;
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

    public void SetGrowthLevelLimits(int min, int max)
    {
        currentMinGrowth = min;
        currentMaxGrowth = max;
    }

    public int GetCurrentGrowthLevel()
    {
        return growthValue;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("GrowthValue", growthValue);
    }
}
