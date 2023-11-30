using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] LevelEntryPoint[] levelEntryPoints;
    [SerializeField] Transform character;
    [SerializeField] int maxGrowthLevel = 1;
    [SerializeField] int minGrowthLevel = -1;
    [SerializeField] GameObject npc;
    [SerializeField] bool isMainLevel;

    private Animator animator;
    private bool isFirstTime = true;

    public bool IsFirstTime => isFirstTime;

    private void Start()
    {
        animator = GetComponent<Animator>();

        MenuManager.OnSceneFadeOut += ActivateSceneFadeOut;

        character.GetComponent<CharacterMovement>().SetGrowthLevelLimits(minGrowthLevel, maxGrowthLevel);

        string previousLevel = PlayerPrefs.GetString("PreviousScene");
        if(previousLevel == "Menu")
        {
            character.GetComponent<CharacterInventory>().Restart();
        }
        if(npc != null)
            npc.SetActive(previousLevel == "Intro" || previousLevel == "Level1");
        if((isMainLevel && previousLevel == "Intro") || !isMainLevel)
        {
            isFirstTime = true;
        }
        else
        {
            isFirstTime = false;
        }
        foreach (LevelEntryPoint entryPoint in levelEntryPoints)
        {
            if(entryPoint.GetPreviousScene() == previousLevel)
            {
                character.position = entryPoint.GetEntryPoint();
                return;
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
