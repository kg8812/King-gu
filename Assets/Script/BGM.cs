using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public static BGM instance;

    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
        source.clip = bgm;
        source.Stop();
    }

    AudioSource source;
    [SerializeField] AudioClip bgm;
    [SerializeField] AudioClip die;

    public void PlayBGM()
    {
        source.clip = bgm;
        source.Play();
        source.loop = true;
    }

    public void Stop()
    {
        source.Stop();
    }

    public void Pause()
    {
        source.Pause();
    }

    public void UnPause()
    {
        source.UnPause();
    }
    public void Die()
    {
        source.clip = die;
        source.Play();
        source.loop = false;
    }
}
