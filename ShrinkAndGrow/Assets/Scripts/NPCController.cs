using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<CharacterMovement>() != null)
        {
            animator.SetTrigger("Idle");
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
