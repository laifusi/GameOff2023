using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] Transform[] destinies;
    [SerializeField] float walkSpeed = 6f;
    [SerializeField] float distanceThreshold = 1;
    [SerializeField] bool shouldStartActive = true;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    Collider2D col2D;

    private int nextDestiny;
    private bool isWalking;
    private Transform destiny;
    private Vector3 currentPosition;
    private Vector3 targetPosition;
    private Vector3 direction;
    private NPCEvent currentWalkEvent;

    public static Action<NPCEvent> OnFinishedWalk;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col2D = GetComponent<Collider2D>();

        ActivateNPC(shouldStartActive);
    }

    private void Update()
    {
        if(isWalking)
        {
            UpdatePosition();
        }
    }

    public void StartWalk(NPCEvent walkEvent)
    {
        if(destinies.Length > nextDestiny)
        {
            currentWalkEvent = walkEvent;
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
            OnFinishedWalk?.Invoke(currentWalkEvent);
        }
    }

    public void ActivateNPC(bool activate)
    {
        spriteRenderer.enabled = activate;
        col2D.enabled = activate;
    }

    public void Appear()
    {
        ActivateNPC(true);
    }

    public void Disappear()
    {
        ActivateNPC(false);
    }
}
