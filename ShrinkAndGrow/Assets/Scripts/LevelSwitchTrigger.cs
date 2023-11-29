using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitchTrigger : MonoBehaviour
{
    [SerializeField] string nextLevelName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<CharacterInventory>() != null)
        {
            MenuManager.Instance.LoadSceneByName(nextLevelName);
        }
    }
}
