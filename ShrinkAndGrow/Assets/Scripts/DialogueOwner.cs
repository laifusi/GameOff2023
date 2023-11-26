using UnityEngine;

public class DialogueOwner : MonoBehaviour
{
    [SerializeField] DialogueEvent[] dialogueEvents;
    [SerializeField] DialogueUI dialogueUI;

    private Animator animator;
    private NPCController controller;

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<NPCController>();

        foreach(DialogueEvent dialogueEvent in dialogueEvents)
        {
            dialogueEvent.AssignOwner(this);
            dialogueEvent.Dialogue.Restart();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterInventory inventory = collision.GetComponent<CharacterInventory>();
        if (inventory != null)
        {
            for(int i = 0; i < dialogueEvents.Length; i++)
            {
                DialogueEvent dialogueEvent = dialogueEvents[i];
                if (dialogueEvent.Dialogue.HasMoreLines())
                {
                    if(dialogueEvent.Type == EventType.ColliderTrigger)
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

    private void PlayDialogueEvent(DialogueEvent dialogueEvent)
    {
        foreach(DialogueAction action in dialogueEvent.DialogueActions)
        {
            switch(action.ActionType)
            {
                case ActionType.Animation:
                    TriggerAnimation(action.AnimationTriggerName);
                    break;
                case ActionType.MoveToWaypoint:
                    TriggerWalk();
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

    private void TriggerWalk()
    {
        controller.StartWalk();
    }

    private void TriggerDialogue(DialogueSO dialogueToRead)
    {
        dialogueUI.ActivatePanel(dialogueToRead);
        dialogueUI.ReadDialogue();
    }
}