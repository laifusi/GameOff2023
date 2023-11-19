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
        foreach(LevelEntryPoint entryPoint in levelEntryPoints)
        {
            if(entryPoint.GetPreviousScene() == previousLevel)
            {
                character.position = entryPoint.GetEntryPoint();
                break;
            }
        }
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
