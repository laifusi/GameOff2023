using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] string houseScene;
    [SerializeField] bool needsKey = true;
    [SerializeField] KeyType keyType;
    [SerializeField] GameObject enterDoorUI;
    [SerializeField] GameObject cantEnterDoorUI;
    [SerializeField] int maxGrowthToEnter = 1;

    private bool activeDoor;
    private CharacterMovement character;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(needsKey)
        {
            CharacterInventory inventory = collider.GetComponent<CharacterInventory>();
            if (inventory != null && inventory.HasKey(keyType))
            {
                character = inventory.GetComponent<CharacterMovement>();
                activeDoor = true;
                enterDoorUI.SetActive(true);
            }
        }
        else
        {
            character = collider.GetComponent<CharacterMovement>();
            activeDoor = true;
            enterDoorUI.SetActive(true);
        }
    }

    private void Update()
    {
        if(cantEnterDoorUI != null && !activeDoor)
            cantEnterDoorUI.SetActive(false);

        if (activeDoor && Input.GetKeyDown(KeyCode.E))
        {
            if(character.GetCurrentGrowthLevel() <= maxGrowthToEnter)
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
            else
            {
                if (cantEnterDoorUI != null)
                    cantEnterDoorUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        activeDoor = false;
        enterDoorUI.SetActive(false);
    }
}
