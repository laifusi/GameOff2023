using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : Collectible
{
    protected override void PickUp(CharacterInventory inventory)
    {
        inventory.AddDiamond();
    }
}
