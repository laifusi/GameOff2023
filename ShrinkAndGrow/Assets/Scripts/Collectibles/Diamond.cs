using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : Collectible
{
    public static Action<NPCEvent> OnDiamondPickedUp;

    protected override void PickUp(CharacterInventory inventory)
    {
        inventory.AddDiamond();
        OnDiamondPickedUp?.Invoke(null);
    }
}
