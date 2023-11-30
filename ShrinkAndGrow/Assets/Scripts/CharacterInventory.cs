using System;
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

    public static Action<int> OnPotionsUpdated;
    public static Action<int> OnOrangesUpdated;
    public static Action<KeyType> OnKeysUpdated;
    public static Action OnDiamondUpdated;
    public static Action OnDiamondRemoved;

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

    private IEnumerator Start()
    {
        yield return null;
        InitializeUI();
    }

    private static void InitializeUI()
    {
        OnPotionsUpdated?.Invoke(potions);
        OnOrangesUpdated?.Invoke(oranges);
        foreach (KeyType key in keys)
        {
            OnKeysUpdated?.Invoke(key);
        }
        if (diamond)
        {
            OnDiamondUpdated?.Invoke();
        }
    }

    public void AddPotion()
    {
        potions++;
        OnPotionsUpdated?.Invoke(potions);
    }

    public void DrinkPotion()
    {
        potions--;
        OnPotionsUpdated?.Invoke(potions);
    }

    public bool HasPotion()
    {
        return potions > 0;
    }

    public void AddOrange()
    {
        oranges++;
        OnOrangesUpdated?.Invoke(oranges);
    }

    public void EatOrange()
    {
        oranges--;
        OnOrangesUpdated?.Invoke(oranges);
    }

    public bool HasOrange()
    {
        return oranges > 0;
    }

    public void AddKey(KeyType keyType)
    {
        keys.Add(keyType);
        OnKeysUpdated?.Invoke(keyType);
    }

    public bool HasKey(KeyType type)
    {
        return keys.Contains(type);
    }

    public void AddDiamond()
    {
        diamond = true;
        OnDiamondUpdated?.Invoke();
    }

    public void RemoveDiamond()
    {
        diamond = false;
        OnDiamondRemoved?.Invoke();
    }

    public bool HasDiamond()
    {
        return diamond;
    }

    public void Restart()
    {
        potions = 0;
        oranges = 0;
        keys.Clear();
        diamond = false;
    }
}

public enum KeyType
{
    A, B, C
}
