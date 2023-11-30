using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevelCanvas : MonoBehaviour
{
    [SerializeField] GameObject canvas;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
                Time.timeScale = 1;
            else
                Time.timeScale = 0;
            canvas.SetActive(!canvas.activeInHierarchy);
        }
    }

    public void ActivateTimeScale()
    {
        Time.timeScale = 1;
    }
}
