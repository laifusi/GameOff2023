using System.Collections;
using UnityEngine;

public class NPCEventOwner : MonoBehaviour
{
    [SerializeField] NPCEvent[] npcEvents;
    [SerializeField] DialogueUI dialogueUI;

    private Animator animator;
    private NPCController controller;

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<NPCController>();

        DialogueUI.OnDialogueFinished += StartCheckDialogueTriggers;
        NPCController.OnFinishedWalk += StartCheckWalkTriggers;

        foreach(NPCEvent npcEvent in npcEvents)
        {
            if(npcEvent.Dialogue != null)
                npcEvent.Dialogue.Restart();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterInventory inventory = collision.GetComponent<CharacterInventory>();
        if (inventory != null)
        {
            for(int i = 0; i < npcEvents.Length; i++)
            {
                NPCEvent npcEvent = npcEvents[i];
                if (!npcEvent.DoneEvent)
                {
                    if(npcEvent.EventTrigger == EventTrigger.ColliderTrigger)
                    {
                        switch(npcEvent.DependencyType)
                        {
                            case DependencyType.None:
                                PlayDialogueEvent(npcEvent);
                                return;
                            case DependencyType.Dialogue:
                                if (npcEvent.DependencyEvent.DoneEvent)
                                    PlayDialogueEvent(npcEvent);
                                return;
                            case DependencyType.Diamond:
                                if (inventory.HasDiamond())
                                    PlayDialogueEvent(npcEvent);
                                return;
                            case DependencyType.Walk:
                                if (npcEvent.DependencyEvent.DoneEvent)
                                    PlayDialogueEvent(npcEvent);
                                return;
                        }
                    }
                }
            }
        }
    }

    private void StartCheckDialogueTriggers(DialogueSO triggerDialogue)
    {
        StartCoroutine(CheckDialogueTriggers(triggerDialogue));
    }

    private IEnumerator CheckDialogueTriggers(DialogueSO triggerDialogue)
    {
        for (int i = 0; i < npcEvents.Length; i++)
        {
            NPCEvent npcEvent = npcEvents[i];
            if (npcEvent.Dialogue == triggerDialogue)
            {
                npcEvent.FinishEvent();
            }
        }

        yield return null;

        for(int i = 0; i < npcEvents.Length; i++)
        {
            NPCEvent npcEvent = npcEvents[i];
            if (!npcEvent.DoneEvent)
            {
                if (npcEvent.EventTrigger == EventTrigger.DialogueEnding)
                {
                    switch (npcEvent.DependencyType)
                    {
                        /*case DependencyType.None:
                            PlayDialogueEvent(npcEvent);
                            return;*/
                        case DependencyType.Dialogue:
                            if (npcEvent.DependencyEvent.DoneEvent)
                            {
                                PlayDialogueEvent(npcEvent);
                            }
                            break;
                    }
                }
            }
        }
    }

    private void StartCheckWalkTriggers(NPCEvent triggerEvent)
    {
        StartCoroutine(CheckWalkTriggers(triggerEvent));
    }

    private IEnumerator CheckWalkTriggers(NPCEvent triggerEvent)
    {
        for (int i = 0; i < npcEvents.Length; i++)
        {
            NPCEvent npcEvent = npcEvents[i];
            if (npcEvent == triggerEvent)
            {
                npcEvent.FinishEvent();
            }
        }

        yield return null;

        for (int i = 0; i < npcEvents.Length; i++)
        {
            NPCEvent npcEvent = npcEvents[i];
            if (!npcEvent.DoneEvent)
            {
                if (npcEvent.EventTrigger == EventTrigger.WalkEnding)
                {
                    switch (npcEvent.DependencyType)
                    {
                        /*case DependencyType.None:
                            PlayDialogueEvent(npcEvent);
                            return;
                        case DependencyType.Dialogue:
                            if (!npcEvent.DependencyEvent.DoneEvent)
                            {
                                PlayDialogueEvent(npcEvent);
                            }
                            return;*/
                        case DependencyType.Walk:
                            if (npcEvent.DependencyEvent.DoneEvent)
                            {
                                PlayDialogueEvent(npcEvent);
                            }
                            break;
                    }
                }
            }
        }
    }

    private void PlayDialogueEvent(NPCEvent npcEvent)
    {
        foreach(EventActions action in npcEvent.EventActions)
        {
            switch(action.ActionType)
            {
                case ActionType.Animation:
                    TriggerAnimation(action.AnimationTriggerName);
                    break;
                case ActionType.MoveToWaypoint:
                    TriggerWalk(npcEvent);
                    break;
                case ActionType.Talk:
                    TriggerDialogue(npcEvent.Dialogue);
                    break;
                case ActionType.NPCActivation:
                    TriggerActivateNPC(action.ActivateNPC);
                    break;
                case ActionType.Collectible:
                    TriggerCollectibleAction(action.CollectibleType, action.AddCollectible);
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

    private void TriggerActivateNPC(bool activate)
    {
        controller.ActivateNPC(activate);
    }

    private void TriggerCollectibleAction(CollectibleType collectible, bool collect)
    {
        switch(collectible)
        {
            case CollectibleType.Diamond:
                if (collect)
                    CharacterInventory.Instance.AddDiamond();
                else
                    CharacterInventory.Instance.RemoveDiamond();
                break;
            case CollectibleType.GrowingFruit:
                if (collect)
                    CharacterInventory.Instance.AddOrange();
                else
                    CharacterInventory.Instance.EatOrange();
                break;
            case CollectibleType.ShrinkingFlower:
                if (collect)
                    CharacterInventory.Instance.AddPotion();
                else
                    CharacterInventory.Instance.DrinkPotion();
                break;
        }
    }

    private void OnDestroy()
    {
        DialogueUI.OnDialogueFinished -= StartCheckDialogueTriggers;
        NPCController.OnFinishedWalk -= StartCheckWalkTriggers;
    }
}

public enum CollectibleType
{
    None, Diamond, GrowingFruit, ShrinkingFlower
}