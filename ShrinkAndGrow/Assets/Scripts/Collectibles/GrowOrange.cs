using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowOrange : Collectible
{
    protected override void PickUp(CharacterInventory inventory)
    {
        inventory.AddOrange();
    }
}
