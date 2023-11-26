using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Character", menuName = "Character")]
public class CharacterSO : ScriptableObject
{
    [SerializeField] string charName;
    [SerializeField] Sprite sprite;

    public string Name => charName;
    public Sprite Sprite => sprite;
}
