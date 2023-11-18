using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] string houseScene;
    [SerializeField] bool needsKey = true;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(needsKey)
        {
            CharacterInventory invetory = collider.GetComponent<CharacterInventory>();
            if (invetory != null && invetory.HasKey(this))
            {
                //Destroy(gameObject);
                SceneManager.LoadScene(houseScene);
            }
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
