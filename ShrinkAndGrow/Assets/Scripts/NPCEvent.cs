using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new Event", menuName = "NPC Event")]
public class NPCEvent : ScriptableObject
{
    [SerializeField] DialogueSO dialogue;
    [SerializeField] EventTrigger eventTrigger;
    [SerializeField] DependencyType dependencyType;
    [SerializeField] NPCEvent dependencyEvent;
    [SerializeField] EventActions[] actions;

    private bool doneEvent;
    private bool started;

    public DialogueSO Dialogue => dialogue;
    public EventTrigger EventTrigger => eventTrigger;
    public DependencyType DependencyType => dependencyType;
    public NPCEvent DependencyEvent => dependencyEvent;
    public EventActions[] EventActions => actions;
    public bool DoneEvent => doneEvent;
    public bool StartedEvent => started;

    public void Restart()
    {
        doneEvent = false;
        started = false;
    }

    public void FinishEvent()
    {
        started = false;
        doneEvent = true;
    }

    public void StartEvent()
    {
        started = true;
    }
}

[Serializable]
public struct EventActions
{
    [SerializeField] ActionType actionType;
    [SerializeField] string animationTriggerName;
    [SerializeField] bool activateNPC;
    [SerializeField] CollectibleType collectibleType;
    [SerializeField] bool addCollectible;

    public ActionType ActionType => actionType;
    public string AnimationTriggerName => animationTriggerName;
    public bool ActivateNPC => activateNPC;
    public CollectibleType CollectibleType => collectibleType;
    public bool AddCollectible => addCollectible;
}

public enum ActionType
{
    Animation, MoveToWaypoint, Talk, NPCActivation, Collectible
}

public enum DependencyType
{
    None, Dialogue, Diamond, Walk
}

public enum EventTrigger
{
    ColliderTrigger, DialogueEnding, WalkEnding
}
