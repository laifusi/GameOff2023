using System;
using UnityEngine;

[Serializable]
public class NPCEvent 
{
    [SerializeField] EventTrigger type;
    [SerializeField] DialogueSO dialogue;
    [SerializeField] DependencyType dependencyType;
    [SerializeField] DialogueSO dependencyDialogue;
    [SerializeField] EventActions[] actions;

    private bool doneEvent;

    public DialogueSO Dialogue => dialogue;
    public EventTrigger Type => type;
    public DependencyType DependencyType => dependencyType;
    public DialogueSO DependencyDialogue => dependencyDialogue;
    public EventActions[] DialogueActions => actions;
    public bool DoneEvent => doneEvent;

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

    public ActionType ActionType => actionType;
    public string AnimationTriggerName => animationTriggerName;
}

public enum ActionType
{
    Animation, MoveToWaypoint, Talk
}

/*[Serializable]
public struct Dialogue
{
    [SerializeField] DialogueLine[] lines;

    private int nextLineId;

    public DialogueLine[] Lines => lines;

    public DialogueLine ReadNextLine()
    {
        DialogueLine lineToRead = lines[nextLineId];
        nextLineId++;
        return lineToRead;
    }

    public bool HasMoreLines()
    {
        return nextLineId < lines.Length;
    }
}*/

[Serializable]
public struct DialogueLine
{
    [SerializeField] string line;
    [SerializeField] CharacterSO character;

    public string Line => line;
    public CharacterSO Character => character;
}

/*[Serializable]
public struct Character
{
    [SerializeField] string name;
    [SerializeField] Sprite sprite;

    public string Name => name;
    public Sprite Sprite => sprite;
}*/

public enum DependencyType
{
    None, Dialogue, Diamond
}

public enum EventTrigger
{
    ColliderTrigger, DialogueEnding, WalkEnding
}
