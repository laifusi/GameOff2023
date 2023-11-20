using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    public static CharacterInventory Instance;

    private static int potions;
    private static int oranges;
    private static List<KeyType> keys = new List<KeyType>();
    private static bool diamond;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void AddPotion()
    {
        potions++;
    }

    public void DrinkPotion()
    {
        potions--;
    }

    public bool HasPotion()
    {
        return potions > 0;
    }

    public void AddOrange()
    {
        oranges++;
    }

    public void EatOrange()
    {
        oranges--;
    }

    public bool HasOrange()
    {
        return oranges > 0;
    }

    public void AddKey(KeyType keyType)
    {
        keys.Add(keyType);
    }

    public bool HasKey(KeyType type)
    {
        return keys.Contains(type);
    }

    public void AddDiamond()
    {
        diamond = true;
    }
}

public enum KeyType
{
    A, B, C
}
