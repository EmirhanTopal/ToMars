 using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static event Action<int,AudioClip> OnPlayOneShot;

    [SerializeField] private List<AudioSource> audioSources;
    
    void Awake()
    {
        if (audioSources == null || audioSources.Count == 0)
        {
            audioSources = new List<AudioSource>(GetComponents<AudioSource>());
        }
        
        OnPlayOneShot += PlayOneShot;
        OnPlayOneShot += PlayAudio;
    }

    public void OnStop()
    {
        OnPlayOneShot -= PlayOneShot;
        OnPlayOneShot -= PlayAudio;
    }
    
    void PlayOneShot(int sourceIndex, AudioClip clip) //referans index - which the clip?
    {
        if (sourceIndex >= 0 && clip != null && sourceIndex < audioSources.Count)
        {
            audioSources[sourceIndex].PlayOneShot(clip);
        }
    }

    void PlayAudio(int sourceIndex, AudioClip clip)
    {
        if (clip != null)
        {
            audioSources[sourceIndex].clip = clip;
            if (sourceIndex >= 0 && sourceIndex < audioSources.Count)
            {
                audioSources[sourceIndex].Play();
            }
        }
        
        
    }

    public static void TriggerPLayShot(int sourceIndex, AudioClip clip)
    {
        OnPlayOneShot?.Invoke(sourceIndex,clip);
    }
    public static void TriggerPLayAudio(int sourceIndex, AudioClip clip)
    {
        OnPlayOneShot?.Invoke(sourceIndex,clip);
    }
}
