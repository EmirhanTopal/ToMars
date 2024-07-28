using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private bool collisionDisable = true;
    
    [SerializeField] int delayValue = 1;
    
    [SerializeField] private int audioSourceIndex;
    [SerializeField] private AudioClip crashClip;
    [SerializeField] private AudioClip successClip;
    
    [SerializeField] private ParticleSystem crashParticle;
    [SerializeField] private ParticleSystem successParticle;
    
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private ParticleManager particleManager;

    private void Update()
    {
        CheatMethod();
    }
    
    void CheatMethod()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextScene();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            //DisableColliders();
            DisableCollisions();
        }
    }

    void DisableCollisions()
    {
        collisionDisable = !collisionDisable;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (collisionDisable)
        {
            switch (other.gameObject.tag) //tag string alır - 
            {
                case "Finish":
                    StartSuccessSequence();
                    Debug.Log("You have arrived the end point ");
                    break;
                case "CubeObstacle":
                    StartCrashSequence();
                    Debug.Log("You crashed the obstacle ");
                    break;
                default:
                    Debug.Log("You are looking good keep pushing");
                    break;
            }
        }
        
    }

    private void StartCrashSequence()
    {
        //todo add SFX upon crash
        AudioManager.TriggerPLayShot(audioSourceIndex,crashClip);
        audioManager.OnStop();
        //todo add particle effect upon crash
        ParticleManager.TriggerPlayParticle(crashParticle);
        particleManager.StopParticle();
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadScene),delayValue);
    }

    private void StartSuccessSequence()
    {
        //todo add SFX upon success
        AudioManager.TriggerPLayShot(audioSourceIndex,successClip);
        audioManager.OnStop();
        //todo add particle effect upon success
        ParticleManager.TriggerPlayParticle(successParticle);
        particleManager.StopParticle();
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextScene),delayValue);
    }
    private void ReloadScene()
    {
        // Scene scene = SceneManager.GetActiveScene();
        // SceneManager.LoadScene(scene.name);
        
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene);
    }

    private void LoadNextScene()
    {
        int scene2 = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = scene2 + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("There is no other scene");
            nextSceneIndex = 0;
            SceneManager.LoadScene(nextSceneIndex);
        }
        
    }
    
    void DisableColliders()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        Debug.Log(allObjects);
        foreach (var obj in allObjects)
        {
            Collider collider = obj.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }
            
            Collider2D collider2D = obj.GetComponent<Collider2D>();
            if (collider2D != null)
            {
                collider2D.enabled = false;
            }
        }
    }
}
