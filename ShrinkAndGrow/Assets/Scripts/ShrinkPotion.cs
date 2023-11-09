using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkPotion : MonoBehaviour
{
    private Animator animator;
    private int drippingRand;

    private void Start()
    {
        animator = GetComponent<Animator>();

        //Every 2 seconds we check if we should play the dripping animation
        InvokeRepeating(nameof(CheckDripping), 2, 2);
    }

    private void CheckDripping()
    {
        drippingRand = Random.Range(0, 2);
        if (drippingRand == 0)
            animator.SetTrigger("Drip");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterInventory invetory = collision.GetComponent<CharacterInventory>();
        if(invetory != null)
        {
            invetory.AddPotion();
            Destroy(gameObject);
        }
    }
}
