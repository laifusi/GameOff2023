using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIntroController : MonoBehaviour
{
    [SerializeField] GameObject[] initiallyDeactivatedElements;
    [SerializeField] GameObject[] initiallyActiveElements;
    [SerializeField] GameObject[] onTriggerActiveElements;
    [SerializeField] GameObject[] onTriggerDeactivateElements;
    [SerializeField] LevelManager levelManager;

    private void Start()
    {
        if(levelManager.IsFirstTime)
        {
            foreach (GameObject go in initiallyDeactivatedElements)
                go.SetActive(false);
            foreach (GameObject go in initiallyActiveElements)
                go.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (GameObject go in onTriggerDeactivateElements)
            go.SetActive(false);
        foreach (GameObject go in onTriggerActiveElements)
            go.SetActive(true);
    }
}
