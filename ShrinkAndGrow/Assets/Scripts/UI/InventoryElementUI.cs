using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryElementUI : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject UIElement;

    protected void UpdateText(int amount)
    {
        text.SetText(amount.ToString());
    }

    protected void ActivateUIElement()
    {
        UIElement.SetActive(true);
    }

    protected void DeactivateUIElement()
    {
        UIElement.SetActive(false);
    }
}
