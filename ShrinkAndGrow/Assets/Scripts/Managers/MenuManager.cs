using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public static Action OnSceneFadeOut;

    [SerializeField] AudioClip soundToWaitFor;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Menu()
    {
        StartCoroutine(LoadById(0));
        //SceneManager.LoadScene(0);
    }

    public void LoadSceneByID(int buildID)
    {
        StartCoroutine(LoadById(buildID));
    }

    private IEnumerator LoadById(int buildId)
    {
        yield return new WaitForSeconds(soundToWaitFor.length/2);
        SceneManager.LoadScene(buildId);
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
        StartCoroutine(ExitGame());
    }

    private IEnumerator ExitGame()
    {
        yield return new WaitForSeconds(soundToWaitFor.length/2);
        Application.Quit();
    }
}
