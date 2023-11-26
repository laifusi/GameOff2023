using System;
using UnityEngine;

[Serializable]
public class DialogueEvent 
{
    [SerializeField] DialogueSO dialogue;
    [SerializeField] EventType type;
    [SerializeField] DependencyType dependencyType;
    [SerializeField] DialogueSO dependencyDialogue;
    [SerializeField] DialogueAction[] actions;

    private DialogueOwner owner;

    public DialogueSO Dialogue => dialogue;
    public EventType Type => type;
    public DependencyType DependencyType => dependencyType;
    public DialogueSO DependencyDialogue => dependencyDialogue;
    public DialogueAction[] DialogueActions => actions;

    public void AssignOwner(DialogueOwner owner)
    {
        this.owner = owner;
    }
}

[Serializable]
public struct DialogueAction
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

public enum EventType
{
    ColliderTrigger, DialogueEnding
}
