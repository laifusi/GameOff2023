using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowOrange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterInventory invetory = collision.GetComponent<CharacterInventory>();
        if (invetory != null)
        {
            invetory.AddOrange();
            Destroy(gameObject);
        }
    }
}
