using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingFoodUI : InventoryElementUI
{
    private void Start()
    {
        CharacterInventory.OnOrangesUpdated += UpdateText;
    }

    private void OnDestroy()
    {
        CharacterInventory.OnOrangesUpdated -= UpdateText;
    }
}
