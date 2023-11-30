using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] DialogueSO[] allDialogueSOs;
    [SerializeField] NPCEvent[] allNPCEventSOs;
    [SerializeField] ObjectiveSO[] allObjectiveSOs;

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("GrowthValue", 0);
        foreach (DialogueSO dialogueSO in allDialogueSOs)
        {
            dialogueSO.Restart();
        }
        foreach (NPCEvent npcEventSO in allNPCEventSOs)
        {
            npcEventSO.Restart();
        }
        foreach(ObjectiveSO objectiveSO in allObjectiveSOs)
        {
            objectiveSO.Restart();
        }
    }
}
