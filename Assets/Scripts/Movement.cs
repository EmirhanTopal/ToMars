using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{
    private Rigidbody _rb;
    private AudioSource _audioSource;
    
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private ParticleManager particleManager;
    
    [SerializeField] private float mainForce;
    [SerializeField] private float Zforce;

    [SerializeField] private int sourceIndex;
    
    [SerializeField] private AudioClip rocketThrusterAudio;
    [SerializeField] private ParticleSystem rocketThrusterParticle;
    [SerializeField] private ParticleSystem rocketRightThrusterParticle;
    [SerializeField] private ParticleSystem rocketLeftThrusterParticle;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RocketMovement();
        RocketRotation();
    }
    
    void RocketMovement()
    {
        if(Input.GetKey(KeyCode.W))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    
    private void StartThrusting()
    {
        _rb.AddRelativeForce(Vector3.up * mainForce * Time.deltaTime);

        if (!_audioSource.isPlaying)
        {
            AudioManager.TriggerPLayAudio(sourceIndex,rocketThrusterAudio);
            ParticleManager.TriggerPlayParticle(rocketThrusterParticle);
        }
    }
    
    private void StopThrusting()
    {
        _audioSource.Stop();
        rocketThrusterParticle.Stop();
    }

    public void RocketRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            LeftThrusting();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RightThrusting();
        }
        else
        {
            StopRotateThrusting();
        }
    }
    
    void ApplyRotation(int posNegNumber)
    {
        _rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Zforce * posNegNumber * Time.deltaTime);
        _rb.freezeRotation = false;
    }
    
    void RightThrusting()
    {
        Debug.Log("d");
        if (!rocketLeftThrusterParticle.isPlaying)
        {
            ParticleManager.TriggerPlayParticle(rocketLeftThrusterParticle);
        }
        //_rb.AddRelativeForce(5,0,0);
        ApplyRotation(-1);
    }

    void LeftThrusting()
    {
        Debug.Log("a");
        //_rb.AddRelativeForce(-5,0,0);
        if (!rocketRightThrusterParticle.isPlaying)
        {
            ParticleManager.TriggerPlayParticle(rocketRightThrusterParticle);
        }
        ApplyRotation(1);
    }
    
    void StopRotateThrusting()
    {
        rocketRightThrusterParticle.Stop();
        rocketLeftThrusterParticle.Stop();
    }

    
}
