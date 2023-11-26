using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] TMP_Text characterName;
    [SerializeField] Image characterImage;
    [SerializeField] TMP_Text line;

    private DialogueSO currentDialogue;

    public void ActivatePanel(DialogueSO dialogue)
    {
        currentDialogue = dialogue;
        panel.SetActive(true);
    }

    public void DeactivatePanel()
    {
        panel.SetActive(false);
    }

    public void ReadDialogue()
    {
        if (!currentDialogue.HasMoreLines())
        {
            DeactivatePanel();
            return;
        }

        DialogueLine lineToRead = currentDialogue.ReadNextLine();
        characterImage.sprite = lineToRead.Character.Sprite;
        characterName.SetText(lineToRead.Character.Name);
        line.SetText(lineToRead.Line);
    }
}
