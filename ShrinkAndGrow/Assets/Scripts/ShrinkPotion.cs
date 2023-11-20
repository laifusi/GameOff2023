using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkPotion : Collectible
{
    private Animator animator;
    private int drippingRand;

    protected new void Start()
    {
        StartCoroutine(base.Start());

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

    protected override void PickUp(CharacterInventory inventory)
    {
        inventory.AddPotion();
    }
}
