using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveUI : MonoBehaviour
{
    [SerializeField] ObjectiveSO[] levelObjectives;
    [SerializeField] TMP_Text text;
    [SerializeField] ObjectiveSO initialObjective;
    [SerializeField] ObjectiveSO goBackObjective;
    [SerializeField] LevelManager levelManager;

    private ObjectiveSO previousObjective;

    private void Start()
    {
        if (!CharacterInventory.Instance.HasDiamond())
        {
            text.SetText(initialObjective.ObjectiveText);
        }
        else if(goBackObjective != null)
        {
            text.SetText(goBackObjective.ObjectiveText);
        }

        NPCEventOwner.OnNewEvent += UpdateObjectiveText;
        Diamond.OnDiamondPickedUp += UpdateObjectiveText;
    }

    private void UpdateObjectiveText(NPCEvent npcEvent)
    {
        if (previousObjective != null)
            previousObjective.CompleteObjective();

        foreach(ObjectiveSO objective in levelObjectives)
        {
            if(!objective.IsDone)
            {
                switch(objective.Detonator)
                {
                    case ObjectiveDetonator.Diamond:
                        text.SetText(objective.ObjectiveText);
                        previousObjective = objective;
                        return;
                    case ObjectiveDetonator.NPC:
                        if(objective.DetonatorEvent == npcEvent)
                        {
                            text.SetText(objective.ObjectiveText);
                            previousObjective = objective;
                        }
                        return;
                }
            }
        }
    }

    private void OnDestroy()
    {
        NPCEventOwner.OnNewEvent -= UpdateObjectiveText;
        Diamond.OnDiamondPickedUp -= UpdateObjectiveText;
    }
}
