using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public static Action OnSceneFadeOut;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadSceneByID(int buildID)
    {
        SceneManager.LoadScene(buildID);
    }

    public void LoadSceneByName(string sceneName)
    {
        StartCoroutine(FadeOutScene(sceneName));
    }

    private IEnumerator FadeOutScene(string sceneName)
    {
        OnSceneFadeOut?.Invoke();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }

    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
