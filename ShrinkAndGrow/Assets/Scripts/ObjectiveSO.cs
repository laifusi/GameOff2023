using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Objective", menuName = "Objective")]
public class ObjectiveSO : ScriptableObject
{
    [SerializeField] string objectiveText;
    [SerializeField] ObjectiveDetonator detonator;
    [SerializeField] NPCEvent detonatorEvent;

    private bool done;

    public bool IsDone => done;
    public string ObjectiveText => objectiveText;
    public ObjectiveDetonator Detonator => detonator;
    public NPCEvent DetonatorEvent => detonatorEvent;

    public void Restart()
    {
        done = false;
    }

    public void CompleteObjective()
    {
        done = true;
    }
}

public enum ObjectiveDetonator
{
    None, Diamond, NPC
}
