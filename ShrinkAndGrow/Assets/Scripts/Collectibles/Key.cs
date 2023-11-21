using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Collectible
{
    [SerializeField] KeyType keyType;

    protected override void PickUp(CharacterInventory inventory)
    {
        inventory.AddKey(keyType);
    }
}
