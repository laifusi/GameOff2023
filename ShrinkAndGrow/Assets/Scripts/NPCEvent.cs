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

    public DialogueSO Dialogue => dialogue;
    public EventTrigger EventTrigger => eventTrigger;
    public DependencyType DependencyType => dependencyType;
    public NPCEvent DependencyEvent => dependencyEvent;
    public EventActions[] EventActions => actions;
    public bool DoneEvent => doneEvent;

    public void Restart()
    {
        doneEvent = false;
    }

    public void FinishEvent()
    {
        doneEvent = true;
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
