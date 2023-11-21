using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondUI : InventoryElementUI
{
    private void Start()
    {
        CharacterInventory.OnDiamondUpdated += ActivateUIElement;
        DeactivateUIElement();
    }

    private void OnDestroy()
    {
        CharacterInventory.OnDiamondUpdated -= ActivateUIElement;
    }
}
