using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] DialogueSO[] allDialogueSOs;
    [SerializeField] NPCEvent[] allNPCEventSOs;

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
    }
}
