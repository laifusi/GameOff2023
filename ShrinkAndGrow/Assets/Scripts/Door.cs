using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CharacterInventory invetory = collision.collider.GetComponent<CharacterInventory>();
        if (invetory != null && invetory.HasKey())
        {
            Destroy(gameObject);
        }
    }
}
