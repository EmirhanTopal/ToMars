using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private static event Action<ParticleSystem> OnParticle;
    
    private void Awake()
    {
        OnParticle += PlayParticle;
    }
    
    public void StopParticle()
    {
        OnParticle -= PlayParticle;
    }

    void PlayParticle(ParticleSystem particleSyst)
    {
        if (particleSyst != null)
        {
            particleSyst.Play();
        }
    }

    public static void TriggerPlayParticle(ParticleSystem particleSystem)
    {
        OnParticle?.Invoke(particleSystem);
    }

    
}
