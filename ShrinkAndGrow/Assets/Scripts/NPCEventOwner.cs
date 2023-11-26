using UnityEngine;

public class NPCEventOwner : MonoBehaviour
{
    [SerializeField] NPCEvent[] dialogueEvents;
    [SerializeField] DialogueUI dialogueUI;

    private Animator animator;
    private NPCController controller;

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<NPCController>();

        DialogueUI.OnDialogueFinished += CheckDialogueTriggers;
        NPCController.OnFinishedWalk += CheckWalkTriggers;

        foreach(NPCEvent dialogueEvent in dialogueEvents)
        {
            dialogueEvent.Dialogue?.Restart();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterInventory inventory = collision.GetComponent<CharacterInventory>();
        if (inventory != null)
        {
            for(int i = 0; i < dialogueEvents.Length; i++)
            {
                NPCEvent dialogueEvent = dialogueEvents[i];
                if (!dialogueEvent.DoneEvent)
                {
                    if(dialogueEvent.Type == EventTrigger.ColliderTrigger)
                    {
                        switch(dialogueEvent.DependencyType)
                        {
                            case DependencyType.None:
                                PlayDialogueEvent(dialogueEvent);
                                return;
                            case DependencyType.Dialogue:
                                if (!dialogueEvent.DependencyDialogue.HasMoreLines())
                                    PlayDialogueEvent(dialogueEvent);
                                return;
                            case DependencyType.Diamond:
                                if (inventory.HasDiamond())
                                    PlayDialogueEvent(dialogueEvent);
                                return;
                        }
                    }
                }
            }
        }
    }

    private void CheckDialogueTriggers(DialogueSO triggerDialogue)
    {
        for (int i = 0; i < dialogueEvents.Length; i++)
        {
            NPCEvent dialogueEvent = dialogueEvents[i];

            if (dialogueEvent.Dialogue == triggerDialogue)
                dialogueEvent.FinishEvent();

            if (!dialogueEvent.DoneEvent)
            {
                if (dialogueEvent.Type == EventTrigger.DialogueEnding)
                {
                    switch (dialogueEvent.DependencyType)
                    {
                        case DependencyType.None:
                            PlayDialogueEvent(dialogueEvent);
                            return;
                        case DependencyType.Dialogue:
                            if (!dialogueEvent.DependencyDialogue.HasMoreLines())
                            {
                                PlayDialogueEvent(dialogueEvent);
                            }
                            return;
                    }
                }
            }
        }
    }

    private void CheckWalkTriggers(NPCEvent triggerEvent)
    {
        for (int i = 0; i < dialogueEvents.Length; i++)
        {
            NPCEvent dialogueEvent = dialogueEvents[i];

            if (dialogueEvent == triggerEvent)
                dialogueEvent.FinishEvent();

            if (!dialogueEvent.DoneEvent)
            {
                if (dialogueEvent.Type == EventTrigger.WalkEnding)
                {
                    switch (dialogueEvent.DependencyType)
                    {
                        case DependencyType.None:
                            PlayDialogueEvent(dialogueEvent);
                            return;
                        case DependencyType.Dialogue:
                            if (!dialogueEvent.DependencyDialogue.HasMoreLines())
                            {
                                PlayDialogueEvent(dialogueEvent);
                            }
                            return;
                    }
                }
            }
        }
    }

    private void PlayDialogueEvent(NPCEvent dialogueEvent)
    {
        foreach(EventActions action in dialogueEvent.DialogueActions)
        {
            switch(action.ActionType)
            {
                case ActionType.Animation:
                    TriggerAnimation(action.AnimationTriggerName);
                    break;
                case ActionType.MoveToWaypoint:
                    TriggerWalk(dialogueEvent);
                    break;
                case ActionType.Talk:
                    TriggerDialogue(dialogueEvent.Dialogue);
                    break;
            }
        }
    }

    private void TriggerAnimation(string animationTrigger)
    {
        animator.SetTrigger(animationTrigger);
    }

    private void TriggerWalk(NPCEvent walkEvent)
    {
        controller.StartWalk(walkEvent);
    }

    private void TriggerDialogue(DialogueSO dialogueToRead)
    {
        dialogueUI.ActivatePanel(dialogueToRead);
        dialogueUI.ReadDialogue();
    }

    private void OnDestroy()
    {
        DialogueUI.OnDialogueFinished -= CheckDialogueTriggers;
        NPCController.OnFinishedWalk -= CheckWalkTriggers;
    }
}