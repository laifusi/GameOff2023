using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    private int potions;
    private int oranges;
    private int keys;
    private List<Door> openableDoors = new List<Door>();

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

    public void AddKey(Door associatedDoor)
    {
        openableDoors.Add(associatedDoor);
    }

    public bool HasKey(Door doorToOpen)
    {
        return openableDoors.Contains(doorToOpen);
    }
}
