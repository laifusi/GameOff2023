using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] LevelEntryPoint[] levelEntryPoints;
    [SerializeField] Transform character;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        MenuManager.OnSceneFadeOut += ActivateSceneFadeOut;

        string previousLevel = PlayerPrefs.GetString("PreviousScene");
        Debug.Log(previousLevel);
        foreach(LevelEntryPoint entryPoint in levelEntryPoints)
        {
            if(entryPoint.GetPreviousScene() == previousLevel)
            {
                Debug.Log("Found entry point");
                character.position = entryPoint.GetEntryPoint();
                return;
            }
        }

        Debug.Log("no entry point");
        // If no entry point, we are testing through the editor, should be a new game
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("GrowthValue", -1);
    }

    private void ActivateSceneFadeOut()
    {
        animator.SetTrigger("FadeOut");
    }

    private void OnDestroy()
    {
        MenuManager.OnSceneFadeOut -= ActivateSceneFadeOut;

        PlayerPrefs.SetString("PreviousScene", MenuManager.Instance.GetSceneName());
    }
}

[Serializable]
public struct LevelEntryPoint
{
    [SerializeField] Transform entryPoint;
    [SerializeField] string previousScene;

    public string GetPreviousScene()
    {
        return previousScene;
    }

    public Vector3 GetEntryPoint()
    {
        return entryPoint.position;
    }
}
