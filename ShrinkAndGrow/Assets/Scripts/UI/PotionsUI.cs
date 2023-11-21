using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionsUI : InventoryElementUI
{
    private void Start()
    {
        CharacterInventory.OnPotionsUpdated += UpdateText;
    }

    private void OnDestroy()
    {
        CharacterInventory.OnPotionsUpdated -= UpdateText;
    }
}
