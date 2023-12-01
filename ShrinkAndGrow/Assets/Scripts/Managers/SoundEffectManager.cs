using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    [SerializeField] AudioSource emptyAudioSourcePrefab;

    AudioSource createdAS;

    public static SoundEffectManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayClip(AudioClip sound)
    {
        createdAS = Instantiate(emptyAudioSourcePrefab, transform);
        createdAS.PlayOneShot(sound);
        Destroy(createdAS, sound.length);
    }
}
