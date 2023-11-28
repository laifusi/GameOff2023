using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Dialogue", menuName = "Dialogue")]
public class DialogueSO : ScriptableObject
{
    [SerializeField] DialogueLine[] lines;

    private int nextLineId;

    public DialogueLine[] Lines => lines;

    public void Restart()
    {
        nextLineId = 0;
    }

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
}

[Serializable]
public struct DialogueLine
{
    [SerializeField] string line;
    [SerializeField] CharacterSO character;

    public string Line => line;
    public CharacterSO Character => character;
}
