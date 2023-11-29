using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsTrigger : MonoBehaviour
{
    [SerializeField] GameObject canvas;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        canvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Thief"))
        {
            canvas.SetActive(true);
            animator.SetTrigger("Credits");
        }
    }
}
