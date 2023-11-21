using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUI : InventoryElementUI
{
    [SerializeField] KeyType keyType;

    private void Start()
    {
        CharacterInventory.OnKeysUpdated += ActivateUI;
        DeactivateUIElement();
    }

    private void ActivateUI(KeyType type)
    {
        if (keyType == type)
            ActivateUIElement();
    }

    private void OnDestroy()
    {
        CharacterInventory.OnKeysUpdated -= ActivateUI;
    }
}
