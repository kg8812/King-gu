using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public AudioClip[] chAudios;
    AudioSource source;
    public static SoundEffectManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        source = GetComponent<AudioSource>();
    }
    
    public void PlayChSound(int count)
    {
        if (count < chAudios.Length)
        {
            source.clip = chAudios[count];
            source.Play();
        }
    }
}
