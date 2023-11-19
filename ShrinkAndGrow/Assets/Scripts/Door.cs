using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] string houseScene;
    [SerializeField] bool needsKey = true;

    private bool activeDoor;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(needsKey)
        {
            CharacterInventory invetory = collider.GetComponent<CharacterInventory>();
            if (invetory != null && invetory.HasKey(this))
            {
                activeDoor = true;
            }
        }
        else
        {
            activeDoor = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(activeDoor && Input.GetKeyDown(KeyCode.E))
        {
            if(needsKey)
            {
                MenuManager.Instance.LoadSceneByName(houseScene);
            }
            else
            {
                MenuManager.Instance.LoadSceneByName("Level1");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        activeDoor = false;
    }
}
