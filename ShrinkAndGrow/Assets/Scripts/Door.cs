using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] string houseScene;
    [SerializeField] bool needsKey = true;
    [SerializeField] KeyType keyType;
    [SerializeField] GameObject doorCanvas;

    private bool activeDoor;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(needsKey)
        {
            CharacterInventory invetory = collider.GetComponent<CharacterInventory>();
            if (invetory != null && invetory.HasKey(keyType))
            {
                activeDoor = true;
                doorCanvas.SetActive(true);
            }
        }
        else
        {
            activeDoor = true;
            doorCanvas.SetActive(true);
        }
    }

    private void Update()
    {
        if (activeDoor && Input.GetKeyDown(KeyCode.E))
        {
            if (needsKey)
            {
                MenuManager.Instance.LoadSceneByName(houseScene);
            }
            else
            {
                MenuManager.Instance.LoadSceneByName("Level1");
            }
            activeDoor = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        activeDoor = false;
        doorCanvas.SetActive(false);
    }
}
