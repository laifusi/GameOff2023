using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectDetonator : MonoBehaviour
{
    [SerializeField] AudioClip[] clip;

    public void PlayClip(int identifier)
    {
        SoundEffectManager.Instance.PlayClip(clip[identifier]);
    }
}
