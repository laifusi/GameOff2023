using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] Transform[] destinies;
    [SerializeField] float walkSpeed = 6f;
    [SerializeField] float distanceThreshold = 0.1f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private int nextDestiny;
    private bool isWalking;
    private Transform destiny;
    private Vector3 currentPosition;
    private Vector3 targetPosition;
    private Vector3 direction;
    private bool firstContact;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(isWalking)
        {
            UpdatePosition();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterInventory inventory = collision.GetComponent<CharacterInventory>();
        if (inventory != null)
        {
            if(!firstContact)
            {
                animator.SetTrigger("Idle");
                firstContact = true;
            }
            else if(inventory.HasDiamond())
            {
                StartWalk();
            }
        }
    }

    public void StartWalk()
    {
        if(destinies.Length > nextDestiny)
        {
            animator.SetTrigger("Walk");
            isWalking = true;
            destiny = destinies[nextDestiny];
            nextDestiny++;
        }
    }

    private void UpdatePosition()
    {
        currentPosition = transform.position;
        targetPosition = destiny.position;
        if (Vector3.Distance(currentPosition, targetPosition) > distanceThreshold)
        {
            direction = targetPosition - currentPosition;
            direction.Normalize();
            spriteRenderer.flipX = direction.x < 0;
            rb.velocity = new Vector2(walkSpeed * direction.x, rb.velocity.y);
        }
        else
        {
            isWalking = false;
            spriteRenderer.flipX = false;
            animator.SetTrigger("Idle");
            rb.velocity = Vector2.zero;
        }
    }
}
