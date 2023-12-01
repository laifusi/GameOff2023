using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] GameObject doorImage;
    [SerializeField] GameObject tunnelImage;
    [SerializeField] bool needsDiamond;
    [SerializeField] GameObject needDiamondCanvas;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CharacterInventory character = collision.collider.GetComponent<CharacterInventory>();
        if (character != null)
        {
            if(!needsDiamond || character.HasDiamond())
            {
                doorImage.SetActive(false);
                tunnelImage.SetActive(true);
                GetComponent<SoundEffectDetonator>().PlayClip(0);
                Destroy(gameObject);
            }
            else if(needsDiamond)
            {
                needDiamondCanvas.SetActive(true);
            }
        }
    }
}
