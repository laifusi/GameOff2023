using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer glow;

    protected Collider2D col2D;
    protected SpriteRenderer spriteRenderer;
    protected bool taken;

    protected IEnumerator Start()
    {
        col2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        yield return null;

        int objTaken = PlayerPrefs.GetInt(MenuManager.Instance.GetSceneName() + "_" + name, 0);
        if (objTaken != 0)
        {
            //PickUp(CharacterInventory.Instance);
            HideObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterInventory invetory = collision.GetComponent<CharacterInventory>();
        if (invetory != null)
        {
            PickUp(invetory);
            HideObject();
        }
    }

    protected abstract void PickUp(CharacterInventory inventory);

    protected void HideObject()
    {
        col2D.enabled = false;
        spriteRenderer.enabled = false;
        if (glow != null)
            glow.enabled = false;
        taken = true;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt(MenuManager.Instance.GetSceneName() + "_" + name, taken ? 1 : 0);
    }
}
